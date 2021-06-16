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
	public class DictionaryTestCommand : BaseCommand
    {
        private readonly IStatusProvider _statusProvider;
        private readonly IDispatcher _dispatcher;
        public DictionaryTestCommand(IStatusProvider statusProvider, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var message = await context.ReplyAsync("Тренеровка слов запущена 🖋\nОтправьте !stop для завершения 🏁");
            await context.PinMessageAsync(message);

            var wordPartOfSpeeches = await _dispatcher.Dispatch<IEnumerable<WordPartOfSpeech>>(new FetchWordPartOfSpeechForFullTestQuery());
            if (wordPartOfSpeeches.Count() < 5)
            {
                await context.ReplyAsync("Недостаточно слов для проходения теста.");
                return;
            }

            //TODO: Next part of method repeat in each type training.
            //Bad practice
            //Replace In SQRC ? but we change status -_-
            var createWordTrainingSetCommand = new CreateWordTrainingCommand
            {
                WordsPartOfSpeech = wordPartOfSpeeches,
                TrainingType = TrainingSetType.DictionaryTest
            };
            var setId = await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            var wordTrainingState = new WordTrainingState
            {
                WordTrainings = wordPartOfSpeeches.Select(x => new WordTraining
                {
                    WordPartOfSpeech = x,
                    WordTrainingSetId = setId
                }).ToList(),
                TrainingSetType = TrainingSetType.DictionaryTest
            };
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, wordTrainingState);

            await next(context);
        }
    }
}
