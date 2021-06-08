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
            var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
            var words = await FetchNextWords(user);
            if (words == null)
            {
                await next(context);
                return;
            }

            var rightWord = words[0];
			words = words.OrderBy(x => Guid.NewGuid()).ToList();

            var rkm = new ReplyKeyboardMarkup(new KeyboardButton[][]
                {
                    new KeyboardButton[] { words[0].EnglishWord, words[1].EnglishWord },
                    new KeyboardButton[] { words[2].EnglishWord, words[3].EnglishWord },
                });

            await context.ReplyAsync($"Текущее слово: {rightWord.RussianWord}", rkm);

            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, rightWord);
        }

        /// <summary>
        /// Fetch next word from WordTraining according TrainingSet and 3 wrong words (order accordingly)
        /// If next word not exist return null.
        /// </summary>
        private async Task<IList<Word>> FetchNextWords(User user)
        {
            var currentWord = await GetCurrentWordAsync(_unitOfWork, user);
            if (currentWord == null)
                return null;

            var wrongWords = await _unitOfWork.WordRepository.FetchWordsByCount(3);
            wrongWords.Insert(0, currentWord);
            return wrongWords;
        }

        //TODO: Split to SQRS Query
        public static async Task<Word> GetCurrentWordAsync(IUnitOfWork unitOfWork, User user)
        {
            var currentWord = await GetCurrentWordTrainingAsync(unitOfWork, user);
            return currentWord?.Word;
        }

        //TODO: Split to SQRS Query
        public static async Task<WordTraining> GetCurrentWordTrainingAsync(IUnitOfWork unitOfWork, User user)
        {
            var trainingSets = await unitOfWork.WordTrainingSetRepository.FetchAllAsync();
            var currentSet = trainingSets.OrderByDescending(x => x.CreatedDate).Where(x => x.UserId == user.Id).FirstOrDefault();
            var currentWordTrainings = await unitOfWork.WordTrainingRepository.FetchBySetAsync(currentSet.Id);
            return currentWordTrainings.OrderByDescending(x => x.Id).FirstOrDefault(x => x.Result == null);
        }
    }
}
