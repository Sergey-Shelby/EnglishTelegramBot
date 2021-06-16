using EnglishTelegramBot.DomainCore.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class WordTrainingSet
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public TrainingSetType TrainingType { get; set; } 
		public DateTime CreatedDate { get; set; }
		public virtual User User { get; set; }
		public List<WordTraining> WordTraining { get; set; } 

	}
}
