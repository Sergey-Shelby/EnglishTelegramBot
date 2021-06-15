using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Repositories
{
	public class LearnWordRepository : BaseRepository<LearnWord>, ILearnWordRepository
	{
		private readonly DbSet<LearnWord> _dbSet;

		public LearnWordRepository(EnglishContext englishContext) : base(englishContext)
		{
			_dbSet = englishContext.Set<LearnWord>();
		}

		public async Task<IEnumerable<LearnWord>> FetchByUserId(int userId)
		{
			return await _dbSet.Include(x => x.WordPartOfSpeech)
					   .Where(x => x.UserId == userId)
					   .OrderBy(x => Guid.NewGuid())
					   .ToListAsync();
		}

		public async Task<LearnWord> FetchByWordPartOfSpeechIdAsync(int id)
		{
			return await _dbSet.Where(x => x.WordPartOfSpeechId == id).FirstOrDefaultAsync();
		}
	}
}
