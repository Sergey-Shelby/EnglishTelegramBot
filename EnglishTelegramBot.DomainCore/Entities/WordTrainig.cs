using System;
using System.Collections.Generic;
using System.Text;

namespace EnglishTelegramBot.DomainCore.Entities
{
	public class WordTrainig
	{
		public int Id { get; set; }
		public int WordId { get; set; }
		public int UserId { get; set; }
		public bool Result { get; set; }
		public DateTime CreationTime { get; set; }
		public DateTime FinishedTime { get; set; }
		public virtual Word Word {get; set;}
		public virtual User User { get; set; }
	}
}
