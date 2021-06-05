using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	class WordTrainingRepository : BaseRepository<WordTraining>, IWordTrainigRepository
	{
		private DbSet<WordTraining> _dbset;
		public WordTrainingRepository(EnglishContext englishContext) : base(englishContext) 
		{
			_dbset = englishContext.Set<WordTraining>();
		}

		public async Task<List<WordTraining>> FetchAllByUserIdAsync(int userId)
		{
			return await _dbset.Where(x => x.UserId == userId).Include(x => x.Word).ToListAsync();
		}
		public async Task<WordTraining> FetchByWordIdAndUserId(int wordId, int userId)
		{
			return await _dbset.FirstOrDefaultAsync(x => x.WordId == wordId && x.UserId == userId);
		}
	}
}
