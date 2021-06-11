using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class WordTrainingStatus
    {
        public Word Word { get; set; }
        public TrainingType TrainingType { get; set; }
    }

    public class CheckSelectWordCommand : BaseCommand
    {
        private IStatusProvider _statusProvider;
        private IUnitOfWork _unitOfWork;
        private IDispatcher _dispatcher;
        public CheckSelectWordCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var status =_statusProvider.GetStatus<WordTrainingState>(context.User.Id);
            if (status?.Details.TrainingType != null)
            {
                //var currentWordTrainings = await _dispatcher.Dispatch<List<WordTraining>>(new FetchCurrentWordTrainingsQuery());
                //var nextWordTraining = currentWordTrainings.OrderBy(x => x.Id).FirstOrDefault(x => x.IsFinished == false);

                //var isWrongAnswer = status.Details.EnglishWord.Trim() != context.Update.Message.Text;
                //if (isWrongAnswer)
                //{
                //    await UpdateWordTraining(nextWordTraining, false, false);
                //    await context.ReplyAsync("🤯 Не правильно!\nПопробуй ещё!");
                //    return;
                //}

                //if (nextWordTraining.RussianSelect == null)
                //    await UpdateWordTraining(nextWordTraining, true, true);
                //else
                //    await UpdateWordTraining(nextWordTraining, false, true);

                //await context.ReplyAsync("🎊 Правильно!");
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
