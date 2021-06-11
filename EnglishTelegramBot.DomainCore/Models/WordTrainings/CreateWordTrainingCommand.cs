using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Models.WordTrainings
{
    public class CreateWordTrainingCommand: ICommand
    {
        public IEnumerable<WordPartOfSpeech> WordsPartOfSpeech { get; set; }
        public TrainingSetType TrainingType { get; set; }
    }
}
