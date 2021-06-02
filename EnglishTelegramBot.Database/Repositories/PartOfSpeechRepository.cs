using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Database.Repositories
{
	public class PartOfSpeechRepository : BaseRepository<PartOfSpeech>, IPartOfSpeechRepository
    {
		public PartOfSpeechRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
