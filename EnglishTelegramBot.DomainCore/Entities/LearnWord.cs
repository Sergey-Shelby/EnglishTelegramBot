using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class LearnWord
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int WordPartOfSpeechId { get; set; }
		public int Level { get; set; }
		public DateTime? NextLevelDate { get; set; }
		public double SelectRus { get; set; }
		public double SelectEng { get; set; }
		public double Input { get; set; }
		public virtual User User { get; set; }
		public virtual WordPartOfSpeech WordPartOfSpeech { get; set; }
	}
}
