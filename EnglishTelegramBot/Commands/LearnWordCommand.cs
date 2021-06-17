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

namespace EnglishTelegramBot.Commands
{
    public class LearnWordCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IDispatcher _dispatcher;
        public LearnWordCommand(IStatusProvider statusProvider, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var message = await context.ReplyAsync("Тренеровка слов запущена 🖋\nОтправьте !stop для завершения 🏁");
            await context.PinMessageAsync(message);

            var wordPartOfSpeeches = await _dispatcher.Dispatch<IEnumerable<WordPartOfSpeech>>(new FetchWordPartOfSpeechForTrainingQuery());

            //TODO: Next part of method repeat in each type training.
            //Bad practice
            //Replace In SQRC ? but we change status -_-
            var createWordTraining = new CreateWordTraining(context, wordPartOfSpeeches, _dispatcher, _statusProvider);
            await createWordTraining.Execute(TrainingSetType.Training);

            await next(context);
        }
    }

    public class WordTrainingState
    {
        public IEnumerable<WordTraining> WordTrainings { get; set; }
        public TrainingSetType TrainingSetType { get; set; }
        /// <summary>
        /// Flag for Check word command
        /// </summary>
        public bool IsStarted { get; set; }
        public TrainingType? TrainingType => WordTrainings switch
         {
             IEnumerable<WordTraining> trainings when trainings.Any(x => x.RussianSelect == null) => DomainCore.Enums.TrainingType.SelectRus,
             IEnumerable<WordTraining> trainings when trainings.Any(x => x.EnglishSelect == null) => DomainCore.Enums.TrainingType.SelectEng,
             IEnumerable<WordTraining> trainings when trainings.Any(x => x.InputEnglish == null) => DomainCore.Enums.TrainingType.Input,
             _ => null
         };

        public WordTraining CurrentWordTraining => TrainingType switch
          {
              DomainCore.Enums.TrainingType.SelectRus => WordTrainings.FirstOrDefault(x => x.RussianSelect == null),
              DomainCore.Enums.TrainingType.SelectEng => WordTrainings.FirstOrDefault(x => x.EnglishSelect == null),
              DomainCore.Enums.TrainingType.Input => WordTrainings.FirstOrDefault(x => x.InputEnglish == null),
              _ => null
          };
    }

}
