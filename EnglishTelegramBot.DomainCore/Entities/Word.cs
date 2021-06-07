using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class Word
	{
		public int Id { get; set; }
		public string EnglishWord { get; set; }
		public string RussianWord { get; set; }
		public List<WordPartOfSpeech> WordPartOfSpeech { get; set; }
	}
}
