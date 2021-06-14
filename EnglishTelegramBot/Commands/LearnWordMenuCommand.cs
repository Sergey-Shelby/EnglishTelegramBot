using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishTelegramBot.Commands
{
    public class LearnWordMenuCommand : BaseCommand
    {
        private readonly IUnitOfWork _unitOfWork;
		private ITelegrafContext _telegrafContext;
        public LearnWordMenuCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
		{
			_telegrafContext = context;
			var replyKeyboardMarkup = await CreateLearnMenuKeyboard();
			await context.ReplyAsync("Пожалуйста, выберите тип изучения", replyKeyboardMarkup);
        }

		private async Task<ReplyKeyboardMarkup> CreateLearnMenuKeyboard()
		{
			var learnWords = await FetchCountRepeatWords();
			var rkm = new ReplyKeyboardMarkup
			{
				Keyboard = new KeyboardButton[][]
				{
					new KeyboardButton[]
					{
						Message.LEARN_NEW_WORDS
					},
					new KeyboardButton[]
					{
						$"{Message.REPEAT_LEARN} ({learnWords.Count})"
					},
					new KeyboardButton[]
					{
						Message.MAIN_MENU
					}
				}
			};
			return rkm;
		}

		private async Task<List<LearnWord>> FetchCountRepeatWords()
		{
			var user = await _unitOfWork.UserRepository.FetchByTelegramId(_telegrafContext.User.Id);
			return await _unitOfWork.LearnWordRepository.FetchWordPartOfSpeechForRepeat(20, user.Id);
        }
    }

}
