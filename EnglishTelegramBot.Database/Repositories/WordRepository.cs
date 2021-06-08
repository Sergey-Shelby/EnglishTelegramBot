using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	public class WordRepository : BaseRepository<Word>, IWordRepository
	{
		private DbSet<Word> _dbset;
		public WordRepository(EnglishContext englishContext) : base(englishContext)
		{
			_dbset = englishContext.Set<Word>();
		}

		public async Task<List<Word>> FetchWordsByCount(int count)
		{
			return await _dbset.OrderBy(x => Guid.NewGuid()).Take(count).ToListAsync();
		}
	}
}
