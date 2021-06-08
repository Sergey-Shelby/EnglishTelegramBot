using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class WordTraining
	{
		public int Id { get; set; }
		public int WordId { get; set; }
		public int WordTrainingSetId { get; set; }
		public bool? Result { get; set; }
		public bool IsFinished { get; set; }
		public virtual Word Word {get; set; }
		public virtual WordTrainingSet WordTrainingSet { get; set; }
	}
}
