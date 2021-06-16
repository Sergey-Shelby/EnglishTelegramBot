using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordPartOfSpeeches;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishTelegramBot.Commands
{
    public class WordTestMenuCommand : BaseCommand
    {
        private readonly IStatusProvider _statusProvider;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDispatcher _dispatcher;
        public WordTestMenuCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
			var replyKeyboardMarkup = await CreateTestMenuKeyboard();
			await context.ReplyAsync("Вы вошли в режим тестирования слов 🖋", replyKeyboardMarkup);
            //_statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD);

            //var wordsPartOfSpeech = await _unitOfWork.WordPartOfSpeechRepository.FetchFullAsync(10);

            //var createWordTrainingSetCommand = new CreateWordTrainingCommand { WordsPartOfSpeech = wordsPartOfSpeech, TrainingType = TrainingSetType.Test10 };
            //await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            //_statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, wordTrainingState);

            //await next(context);
        }

        private async Task<ReplyKeyboardMarkup> CreateTestMenuKeyboard()
        {
            var rkm = new ReplyKeyboardMarkup
            {
                Keyboard = new KeyboardButton[][]
                {
                new KeyboardButton[]
                {
                    Message.MAIN_TEST_WORD
                },
                new KeyboardButton[]
                {
                    Message.LEARN_TEST_WORD
                },
                new KeyboardButton[]
                {
                    Message.MAIN_MENU
                }
                }
            };
            return rkm;
        }
    }
}
