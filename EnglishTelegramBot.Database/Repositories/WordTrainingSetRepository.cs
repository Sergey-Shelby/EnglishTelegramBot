using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Repositories
{
	public class WordTrainingSetRepository : BaseRepository<WordTrainingSet>, IWordTrainingSetRepository
	{
		private readonly DbSet<WordTrainingSet> _dbset;
		public WordTrainingSetRepository(EnglishContext englishContext) : base(englishContext)
		{
			_dbset = englishContext.Set<WordTrainingSet>();
		}

		public async Task<List<WordTrainingSet>> FetchAllByUserIdAsync(int userId)
		{
			return await _dbset.Where(x => x.UserId == userId).Include(y => y.WordTraining).ToListAsync();

			//return await _dbset.Where(x => x.UserId == userId).Include(x => x.Word).ToListAsync();
			//throw new System.Exception();
		}
		public async Task<WordTrainingSet> FetchLastByUserId(int userId)
		{
			return await _dbset.Where(x => x.UserId == userId).OrderByDescending(p => p.CreatedDate).FirstOrDefaultAsync();
		}
	}
}
