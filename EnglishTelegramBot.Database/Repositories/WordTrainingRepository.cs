using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Database.Repositories
{
	class WordTrainingRepository : BaseRepository<WordTrainig>, IWordTrainigRepository
	{
		public WordTrainingRepository(EnglishContext englishContext) : base(englishContext) 
		{

		}
	}
}
