using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class WordPartOfSpeechData
	{
		public int Id { get; set; }
		public int WordPartOfSpeechId { get; set; }
		public string Word { get; set; }
		public bool IsMain { get; set; }
		public virtual WordPartOfSpeech WordPartOfSpeech { get; set; }
	}
}
