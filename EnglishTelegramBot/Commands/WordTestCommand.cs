using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands
{
    public class WordTestCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        public WordTestCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Вы вошли в режим тестирования слов 🖋");
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD);

            var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
            var wordsPartOfSpeech = await _unitOfWork.WordPartOfSpeechRepository.FetchWordsByCount(10);
            await LearnWordCommand.GenerateWordTrainingTable(_unitOfWork, wordsPartOfSpeech, user, TrainingType.Test10);

            await next(context);
        }
    }
}
