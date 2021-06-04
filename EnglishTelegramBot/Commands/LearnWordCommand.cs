using EnglishTelegramBot.Constants;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands
{
    public class LearnWordCommand : BaseCommand
    {
        StatusProvider _statusProvider;
        public LearnWordCommand(StatusProvider statusProvider)
        {
            _statusProvider = statusProvider;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Тренеровка слов запущена 🖋\nОтправьте !stop для завершения 🏁");
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD);
            await next(context);
        }
    }
}
