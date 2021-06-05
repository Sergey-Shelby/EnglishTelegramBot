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
        IStatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        public NextWordCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var words = await FetchNextWords();

            var rightWord = words[0];

            var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
            await _unitOfWork.WordTrainigRepository.CreateAsync(new WordTraining()
            {
                WordId = rightWord.ID,
                UserId = user.Id,
                CreationTime = DateTime.Now
            });
			await _unitOfWork.WordTrainigRepository.SaveAsync();

			words = words.OrderBy(x => Guid.NewGuid()).ToList();

            var rkm = new ReplyKeyboardMarkup();
            rkm.Keyboard =
                new KeyboardButton[][]
                {
                    new KeyboardButton[] { words[0].English, words[1].English },
                    new KeyboardButton[] { words[2].English, words[3].English },
                };

            await context.ReplyAsync($"Текущее слово: {rightWord.Russian} ({rightWord.Theme.Name})", rkm);

            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, rightWord);
        }

        private async Task<IList<Word>> FetchNextWords()
        {
            return await _unitOfWork.WordRepository.FetchFourWords();
        }
    }
}
