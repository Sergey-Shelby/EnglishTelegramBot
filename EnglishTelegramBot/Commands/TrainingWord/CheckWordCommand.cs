using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using System;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class CheckWordCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        public CheckWordCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var status =_statusProvider.GetStatus<Word>(context.User.Id);
            if (status?.Details != null)
            {
                var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
               
                var wordTraining = await NextWordCommand.GetCurrentWordTrainingAsync(_unitOfWork, user);
                var isWrongAnswer = status.Details.EnglishWord.Trim() != context.Update.Message.Text;
                if (isWrongAnswer)
                {
                    await UpdateWordTraining(wordTraining, false, false);
                    await context.ReplyAsync("🤯 Не правильно!\nПопробуй ещё!");
                    return;
                }

                if (wordTraining.RussianSelect == null)
                    await UpdateWordTraining(wordTraining, true, true);
                else
                    await UpdateWordTraining(wordTraining, false, true);

                await context.ReplyAsync("🎊 Правильно!");
            }
            await next(context);
        }

        public async Task UpdateWordTraining(WordTraining wordTraining, bool result, bool finished) 
		{
            wordTraining.RussianSelect = result;
            wordTraining.IsFinished = finished;
            _unitOfWork.WordTrainingRepository.Update(wordTraining);
            await _unitOfWork.WordTrainingRepository.SaveAsync();
        }
    }
}
