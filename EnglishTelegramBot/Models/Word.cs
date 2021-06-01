using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Models
{
	public class Word
	{
		public int ID { get; set; }
		public int ThemeID { get; set; }
		public string English { get; set; }
		public string Russian { get; set; } 
		public Theme Theme { get; set; }
	}
}
