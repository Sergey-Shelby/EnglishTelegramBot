using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class ThemeWords
	{
		public int Id { get; set; }
		public int WordPartOfSpeechId { get; set; }
		public int ThemeId { get; set; }
		public virtual WordPartOfSpeech WordPartOfSpeech { get; set; }
		public virtual Theme Theme { get; set; }
	}
}
