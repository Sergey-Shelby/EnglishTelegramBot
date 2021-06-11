using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class WordTraining
	{
		public int Id { get; set; }
		public int WordPartOfSpeechId { get; set; }
		public int WordTrainingSetId { get; set; }
		public bool? RussianSelect { get; set; }
		public bool? EnglishSelect { get; set; }
		public bool? InputEnglish { get; set; }
		public bool? InputRussian { get; set; }
		public bool IsFinished { get; set; }
		public virtual WordPartOfSpeech WordPartOfSpeech { get; set; }
		public virtual WordTrainingSet WordTrainingSet { get; set; }
	}
}
