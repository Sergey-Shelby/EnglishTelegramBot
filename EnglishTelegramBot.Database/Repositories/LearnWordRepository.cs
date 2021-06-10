using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Database.Repositories
{
	public class LearnWordRepository : BaseRepository<LearnWord>, ILearnWordRepository
	{
		public LearnWordRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
