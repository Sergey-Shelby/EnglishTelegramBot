using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class CheckWordCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        public CheckWordCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var status =_statusProvider.GetStatus<Word>(context.User.Id);
            if (status.Details != null)
            {
                if (status.Details.English.Trim() != context.Update.Message.Text)
                {
                    await context.ReplyAsync("🤯 Не правильно!\nПопробуй ещё!");
                    return;
                }
                await context.ReplyAsync("🎊 Правильно!");
            }
            await next(context);
        }
    }
}
