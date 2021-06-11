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
	public class WordPartOfSpeechRepository : BaseRepository<WordPartOfSpeech>, IWordPartOfSpeechRepository
	{
		private DbSet<WordPartOfSpeech> _dbset;
		public WordPartOfSpeechRepository(EnglishContext englishContext) : base(englishContext)
		{
			_dbset = englishContext.Set<WordPartOfSpeech>();
		}

		public async Task<List<WordPartOfSpeech>> FetchFullAsync(int take)
		{
			return await _dbset.Include(x => x.Word)
							   .Include(x => x.PartOfSpeech)
							   .Include(x => x.WordPartOfSpeechDatas)
							   .OrderBy(x => Guid.NewGuid())
							   .Take(take)
							   .ToListAsync();
		}

		public async Task<WordPartOfSpeech> FetchFullByWordIdAsync(int wordId)
		{
			return await _dbset.Include(x => x.Word).Include(y => y.PartOfSpeech).Where(x => x.WordId == wordId).FirstOrDefaultAsync();
		}
	}
}
