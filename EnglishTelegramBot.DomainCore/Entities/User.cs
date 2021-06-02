using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class User
	{
		public int Id { get; set; } 
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Username { get; set; }
		public string LanguageCode { get; set; }
	}
}
