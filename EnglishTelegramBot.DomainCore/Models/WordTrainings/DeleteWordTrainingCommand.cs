using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Models.WordTrainings
{
    public class DeleteWordTrainingCommand : ICommand
    {
        public int WordTrainingSetId { get; set; }
    }
}
