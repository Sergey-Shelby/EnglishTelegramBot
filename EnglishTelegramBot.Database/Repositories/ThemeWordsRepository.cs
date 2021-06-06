using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Database.Repositories
{
	public class ThemeWordsRepository : BaseRepository<ThemeWords>, IThemeWordsRepository 
	{
		public ThemeWordsRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
