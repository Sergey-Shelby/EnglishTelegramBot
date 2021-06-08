using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Repositories
{
	public class ThemeWordsRepository : BaseRepository<ThemeWords>, IThemeWordsRepository 
	{
		private DbSet<ThemeWords> _dbset;
		public ThemeWordsRepository(EnglishContext englishContext) : base(englishContext)
		{
			_dbset = englishContext.Set<ThemeWords>();
		}

		public async Task<ThemeWords> FetchByPartOfSpeechId(int partOfSpeechId)
		{
			return await _dbset.Include(x => x.Theme).Where(y => y.WordPartOfSpeechId == partOfSpeechId).FirstOrDefaultAsync();
		}
	}
}
