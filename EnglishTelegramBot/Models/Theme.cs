using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Models
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
