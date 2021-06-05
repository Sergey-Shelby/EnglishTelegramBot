using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class NextWordCommand : BaseCommand
    {
        StatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        public NextWordCommand(StatusProvider statusProvider, IUnitOfWork unitOfWork)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var words = await FetchNextWords();

            var rightWord = words[0];
            words = words.OrderBy(x => Guid.NewGuid()).ToList();

            var rkm = new ReplyKeyboardMarkup();
            rkm.Keyboard =
                new KeyboardButton[][]
                {
                    new KeyboardButton[] { words[0].English, words[1].English },
                    new KeyboardButton[] { words[2].English, words[3].English },
                };

            await context.ReplyAsync($"Текущее слово: {rightWord.Russian}", rkm);

            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, rightWord);
        }

        private async Task<IList<Word>> FetchNextWords()
        {
            var words = await _unitOfWork.WordRepository.FetchAllAsync();
            return words.OrderBy(x => Guid.NewGuid()).Take(4).ToList();
        }
    }
}
