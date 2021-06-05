using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class FinishTrainingCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        public FinishTrainingCommand(IStatusProvider statusProvider)
        {
            _statusProvider = statusProvider;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Тренеровка завершена!");
            await context.UnpinMessageAsync();
            _statusProvider.ClearStatus(context.User.Id);
            await next(context);
        }
    }
}
