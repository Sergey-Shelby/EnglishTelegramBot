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
        private readonly IStatusProvider _statusProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDispatcher _dispatcher;
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

            var wordsPartOfSpeech = await _unitOfWork.WordPartOfSpeechRepository.FetchFullAsync(10);

            var createWordTrainingSetCommand = new CreateWordTrainingCommand { WordsPartOfSpeech = wordsPartOfSpeech, TrainingType = TrainingSetType.Test10 };
            await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            await next(context);
        }
    }
}
