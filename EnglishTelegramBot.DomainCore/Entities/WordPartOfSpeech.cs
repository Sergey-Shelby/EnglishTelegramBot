using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class WordPartOfSpeech
	{
		public int Id { get; set; }
		public int WordId { get; set; }
		public int PartOfSpeechId { get; set; }
		public virtual Word Word { get; set; }
		public virtual PartOfSpeech PartOfSpeech { get; set; }
		public virtual List<WordPartOfSpeechData> WordPartOfSpeechDatas { get; set; }
	}
}
