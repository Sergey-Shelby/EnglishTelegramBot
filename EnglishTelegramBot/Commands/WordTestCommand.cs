using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands
{
    public class WordTestCommand : BaseCommand
    {
        private IStatusProvider _statusProvider;
        private IUnitOfWork _unitOfWork;
        private IDispatcher _dispatcher;
        public WordTestCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Вы вошли в режим тестирования слов 🖋");
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD);

            var wordsPartOfSpeech = await _unitOfWork.WordPartOfSpeechRepository.FetchFullByCount(10);

            var createWordTrainingSetCommand = new CreateWordTrainingSetCommand { WordsPartOfSpeech = wordsPartOfSpeech, TrainingType = TrainingTypeSet.Test10 };
            await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            await next(context);
        }
    }
}
