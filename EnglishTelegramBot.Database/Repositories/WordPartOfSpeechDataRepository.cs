using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Database.Repositories
{
	public class WordPartOfSpeechDataRepository : BaseRepository<WordPartOfSpeechData>, IWordPartOfSpeechDataRepository
	{
		public WordPartOfSpeechDataRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
