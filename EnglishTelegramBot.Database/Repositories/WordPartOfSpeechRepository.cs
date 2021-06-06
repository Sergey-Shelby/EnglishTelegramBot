using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Database.Repositories
{
	public class WordPartOfSpeechRepository : BaseRepository<WordPartOfSpeech>, IWordPartOfSpeechRepository
	{
		public WordPartOfSpeechRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
