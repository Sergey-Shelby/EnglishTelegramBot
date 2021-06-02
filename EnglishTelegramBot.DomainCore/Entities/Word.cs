namespace EnglishTelegramBot.DomainCore.Entities
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
