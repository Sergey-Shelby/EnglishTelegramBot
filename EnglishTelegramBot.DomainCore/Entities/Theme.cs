using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class Theme
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public List<ThemeWords> ThemeWords { get; set; }
	}
}
