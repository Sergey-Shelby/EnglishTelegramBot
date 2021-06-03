using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands
{
    public class CheckWordCommand : BaseCommand
    {
        StatusProvider _statusProvider;
        public CheckWordCommand(StatusProvider statusProvider)
        {
            _statusProvider = statusProvider;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("status cleared");
            _statusProvider.ClearStatus(context.User.Id);
        }
    }
}
