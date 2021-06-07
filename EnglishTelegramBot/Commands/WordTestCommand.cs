using EnglishTelegramBot.Constants;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands
{
    public class WordTestCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        public WordTestCommand(IStatusProvider statusProvider)
        {
            _statusProvider = statusProvider;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Вы вошли в режим тестирования слов 🖋");
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, TrainingType.Test10);
            await next(context);
        }
    }
}
