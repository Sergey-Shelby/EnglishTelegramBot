using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Models.WordTrainings
{
    public class CreateWordTrainingSetCommand: ICommand
    {
        public IEnumerable<WordPartOfSpeech> WordsPartOfSpeech { get; set; }
        public TrainingTypeSet TrainingType { get; set; }
    }
}
