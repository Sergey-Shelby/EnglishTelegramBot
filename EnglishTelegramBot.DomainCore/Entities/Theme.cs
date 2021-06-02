using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class Theme
	{
		public int ID { get; set; }
		public int SpeechID { get; set; }
		public string Name { get; set; }
		public List<Word> Words { get; set; }
		public PartOfSpeech PartsOfSpeech { get; set; }
	}
}
