using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordPartOfSpeeches;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishTelegramBot.Commands
{
    public class LearnWordMenuCommand : BaseCommand
    {
		private readonly IDispatcher _dispatcher;

		public LearnWordMenuCommand(IDispatcher dispatcher)
        {
			_dispatcher = dispatcher;

		}
        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
		{
			var replyKeyboardMarkup = await CreateLearnMenuKeyboard();
			await context.ReplyAsync("Пожалуйста, выберите тип изучения", replyKeyboardMarkup);
        }

        private async Task<ReplyKeyboardMarkup> CreateLearnMenuKeyboard()
        {
            var countWordsForRepeat = await CountWordsForRepeat();
            var messageLearnWord = countWordsForRepeat != 0 ? $"{Message.REPEAT_LEARN} ({countWordsForRepeat})" : Message.REPEAT_LEARN;
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
            messageLearnWord
          },
          new KeyboardButton[]
          {
            Message.MAIN_MENU
          }
              }
            };
            return rkm;
        }

        private async Task<int> CountWordsForRepeat()
		{
			var wordsForRepeat = await _dispatcher.Dispatch<IEnumerable<WordPartOfSpeech>>(new FetchWordPartOfSpeechForRepeatQuery());
			return wordsForRepeat.Count();
		}
    }

}
