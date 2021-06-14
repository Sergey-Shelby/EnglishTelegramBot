using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Models.LearnWords
{
	public class CreateLearnWordCommand : ICommand
    { 
        public IEnumerable<WordTraining> WordTrainings { get; set; }
    }
}
