using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Repositories
{
	public class ThemeRepository : BaseRepository<Theme>, IThemeRepository
    {
		private DbSet<Theme> _dbset;
		public ThemeRepository(EnglishContext englishContext) : base(englishContext)
		{
			_dbset = englishContext.Set<Theme>();
		}
	}
}
