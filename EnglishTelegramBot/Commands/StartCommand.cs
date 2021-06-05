using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Commands
{
    public class StartCommand : BaseCommand
    {
        private IUnitOfWork _unitOfWork;
		public StartCommand(IUnitOfWork unitOfWork)
		{
            this._unitOfWork = unitOfWork;
        }
        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            User user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
            if (user == null)
			{
                await _unitOfWork.UserRepository.CreateAsync(new User()
                {
                    TelegramId = context.User.Id,
                    FirstName = context.User.FirstName,
                    LastName = context.User.LastName,
                    UserName = context.User.Username,
                    LanguageCode = context.User.LanguageCode
                });
                await _unitOfWork.UserRepository.SaveAsync();
                await context.ReplyAsync("Спасибо за регистрацию!");
            }
            await next(context);
        }
    }
}
