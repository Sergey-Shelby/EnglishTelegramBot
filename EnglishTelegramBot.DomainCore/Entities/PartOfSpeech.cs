using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class PartOfSpeech
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public List<Theme> Themes { get; set; }
	}
}
