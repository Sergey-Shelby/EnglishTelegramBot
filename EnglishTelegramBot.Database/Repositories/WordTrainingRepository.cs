using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	class WordTrainingRepository : BaseRepository<WordTraining>, IWordTrainingRepository
	{
		private readonly DbSet<WordTraining> _dbset;
		public WordTrainingRepository(EnglishContext englishContext) : base(englishContext) 
		{
			_dbset = englishContext.Set<WordTraining>();
		}

		public async Task<List<WordTraining>> FetchAllByUserIdAsync(int userId)
		{
			return await _dbset
				.Include(x => x.WordPartOfSpeech)
				.Include(x=>x.WordTrainingSet)
				.Where(x => x.WordTrainingSet.UserId == userId)
				.ToListAsync();
		}

		public Task<List<WordTraining>> FetchBySetAsync(int setId)
		{
			return _dbset
				.Where(x => x.WordTrainingSetId == setId)
				.Include(x => x.WordPartOfSpeech)
				.ThenInclude(x=>x.Word)
				.ToListAsync();
		}

		public Task<WordTraining> FetchBySetAsync(int setId, int wordPartOfSpeechId)
		{
			return _dbset
				.Where(x => x.WordTrainingSetId == setId && x.WordPartOfSpeechId == wordPartOfSpeechId)
				.FirstOrDefaultAsync();
		}
	}
}
