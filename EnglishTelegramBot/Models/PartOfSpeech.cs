using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Models
{
	public class PartOfSpeech
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public List<Theme> Themes { get; set; }
	}
}
