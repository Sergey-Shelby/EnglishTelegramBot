using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
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
    public class NextWordSelectTypeCommand : BaseCommand
    {
        private IStatusProvider _statusProvider;
        private IUnitOfWork _unitOfWork;
        private IDispatcher _dispatcher;
        public NextWordSelectTypeCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
            var words = await FetchNextWords();
            if (words == null)
            {
                await next(context);
                return;
            }

            var rightWordPartOfSpeech = words[0];
            var rightThemeWords = await _unitOfWork.ThemeWordsRepository.FetchByPartOfSpeechId(rightWordPartOfSpeech.Id);

			words = words.OrderBy(x => Guid.NewGuid()).ToList();

            var rkm = new ReplyKeyboardMarkup(new KeyboardButton[][]
                {
                    new KeyboardButton[] { words[0].Word.EnglishWord, words[1].Word.EnglishWord },
                    new KeyboardButton[] { words[2].Word.EnglishWord, words[3].Word.EnglishWord },
                });

            await context.ReplyAsync($"Текущее слово: {rightWordPartOfSpeech.Word.RussianWord} ({rightWordPartOfSpeech.PartOfSpeech.Name.ToLower()}, {rightThemeWords.Theme.Name.ToLower()})", rkm);

            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, rightWordPartOfSpeech.Word);
        }

        /// <summary>
        /// Fetch next word from WordTraining according TrainingSet and 3 wrong words (order accordingly)
        /// If next word not exist return null.
        /// </summary>
        private async Task<IList<WordPartOfSpeech>> FetchNextWords()
        {
            var currentWord = await _dispatcher.Dispatch<WordPartOfSpeech>(new FetchNextWordPartOfSpeechForTrainingQuery());
            if (currentWord == null)
                return null;

            var wrongWords = await _unitOfWork.WordPartOfSpeechRepository.FetchFullAsync(3);
            wrongWords.Insert(0, currentWord);
            return wrongWords;
        }
    }
}
