using EnglishTelegramBot.DomainCore.Enums;
using System;

namespace EnglishTelegramBot.DomainCore.Models.WordTrainings
{
    public class WordTestsHistory
    {
        public DateTime DateTime { get; set; }
        public TrainingSetType TrainingType { get; set; }
        public double Result { get; set; }
    }
}
