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
    public class LearnWordCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        IDispatcher _dispatcher;
        public LearnWordCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var message = await context.ReplyAsync("Тренеровка слов запущена 🖋\nОтправьте !stop для завершения 🏁");

            await context.PinMessageAsync(message);
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD);

            var wordsPartOfSpeech = await _unitOfWork.WordPartOfSpeechRepository.FetchFullByCount(5);

            var createWordTrainingSetCommand = new CreateWordTrainingSetCommand { WordsPartOfSpeech = wordsPartOfSpeech, TrainingType = TrainingTypeSet.Training };
            await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            await next(context);
        }
    }

}
