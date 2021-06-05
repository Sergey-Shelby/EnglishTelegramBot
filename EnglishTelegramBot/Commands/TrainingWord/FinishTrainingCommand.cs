using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class FinishTrainingCommand : BaseCommand
    {
        StatusProvider _statusProvider;
        public FinishTrainingCommand(StatusProvider statusProvider)
        {
            _statusProvider = statusProvider;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Тренеровка завершена!");
            _statusProvider.ClearStatus(context.User.Id);
            await next(context);
        }
    }
}
