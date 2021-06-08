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
            if (status.Details != null)
            {
                var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
                var wordTraining = await _unitOfWork.WordTrainingRepository.FetchByWordIdAndUserId(status.Details.Id, user.Id);

                if (status.Details.EnglishWord.Trim() != context.Update.Message.Text)
                {
                    await UpdateWordTraining(wordTraining, context.User.Id, false);

                    await context.ReplyAsync("🤯 Не правильно!\nПопробуй ещё!");
                    return;
                }
                if (wordTraining.Result == null)
                {
                    await UpdateWordTraining(wordTraining, context.User.Id, true);
                }
                await context.ReplyAsync("🎊 Правильно!");
            }
            await next(context);
        }
        public async Task UpdateWordTraining(WordTraining wordTraining, int userTelegramId, bool result) 
		{
            wordTraining.Result = result;
            //wordTraining.FinishedTime = DateTime.Now;
            //var user = await _unitOfWork.UserRepository.FetchByTelegramId(userTelegramId);
            _unitOfWork.WordTrainingRepository.Update(wordTraining);
            await _unitOfWork.WordTrainingRepository.SaveAsync();
        }
    }
}
