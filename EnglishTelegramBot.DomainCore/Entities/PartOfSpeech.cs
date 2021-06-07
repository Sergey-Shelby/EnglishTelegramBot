using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class PartOfSpeech
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<WordPartOfSpeech> Themes { get; set; }
	}
}
