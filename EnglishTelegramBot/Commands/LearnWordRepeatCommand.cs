using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordPartOfSpeeches;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;


namespace EnglishTelegramBot.Commands
{
	public class LearnWordRepeatCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IDispatcher _dispatcher;
        public LearnWordRepeatCommand(IStatusProvider statusProvider, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var wordPartOfSpeeches = await _dispatcher.Dispatch<IEnumerable<WordPartOfSpeech>>(new FetchWordPartOfSpeechForRepeatQuery());
            if (!wordPartOfSpeeches.Any())
            {
                await CheckRepeatWords(context);
                return;
            }
            var message = await context.ReplyAsync("Тренеровка повтора слов запущена 🖋\nОтправьте !stop для завершения 🏁");
            await context.PinMessageAsync(message);


            //TODO: Next part of method repeat in each type training.
            //Bad practice
            //Replace In SQRC ? but we change status -_-
            var createWordTrainingSetCommand = new CreateWordTrainingCommand
            {
                WordsPartOfSpeech = wordPartOfSpeeches,
                TrainingType = TrainingSetType.Training
            };
            var setId = await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            var wordTrainingState = new WordTrainingState
            {
                WordTrainings = wordPartOfSpeeches.Select(x => new WordTraining
                {
                    WordPartOfSpeech = x,
                    WordTrainingSetId = setId
                }).ToList(),
                TrainingSetType = TrainingSetType.Training
            };
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, wordTrainingState);

            await next(context);
        }
        private async Task CheckRepeatWords(TelegrafContext context)
        {
            var wordPartOfSpeeches = await _dispatcher.Dispatch<IEnumerable<WordPartOfSpeech>>(new FetchWordPartOfSpeechForRepeatQueryFull());
            if (!wordPartOfSpeeches.Any())
            {
                await context.ReplyAsync("Нет слов для повторения.");
            }
            else
            {
                var message = string.Empty;
                foreach (var wordPart in wordPartOfSpeeches)
                {
                    message += $"<i>{wordPart.Word.EnglishWord}</i> ({wordPart.LearnWords.Where(x => x.WordPartOfSpeechId == wordPart.Id).FirstOrDefault().NextLevelDate?.ToString("dd.MM.yyyy")}) \n";
                }
                await context.ReplyAsyncWithHtml($"Cлова для повторения (дата повторения):\n{message}");
            }
        }
    }
}
