using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Database.Repositories
{
	public class WordTrainingSetRepository : BaseRepository<WordTrainingSet>, IWordTrainingSetRepository
	{
		public WordTrainingSetRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
