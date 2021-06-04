using EnglishTelegramBot.Constants;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;
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
                }) ;
			}
            await _unitOfWork.UserRepository.SaveAsync();
            await context.ReplyAsync("Good evening", CreateMainMenuKeyboard());
        }

        private ReplyKeyboardMarkup CreateMainMenuKeyboard()
        {
            var rkm = new ReplyKeyboardMarkup();
            rkm.Keyboard =
                new KeyboardButton[][]
                {
                    new KeyboardButton[]
                    {
                        Message.LEARN_WORD
                    },
                    new KeyboardButton[]
                    {
                        "Line 2-1", "Line 2-2"
                    }
                };
            return rkm;
        }
    }
}
