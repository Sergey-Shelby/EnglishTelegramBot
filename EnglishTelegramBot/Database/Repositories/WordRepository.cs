using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.Models;

namespace EnglishTelegramBot.Database.Repositories
{
	public class WordRepository : BaseRepository<Word>, IWordRepository
	{
		public WordRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
