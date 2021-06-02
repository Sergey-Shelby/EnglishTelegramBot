using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.Database.Repositories
{
	public class ThemeRepository : BaseRepository<Theme>, IThemeRepository
    {
		public ThemeRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
