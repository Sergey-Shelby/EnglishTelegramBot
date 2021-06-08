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

		public async Task<List<WordPartOfSpeech>> FetchWordsByCount(int count)
		{
			return await _dbset.Include(x => x.Word).Include(y => y.PartOfSpeech).OrderBy(x => Guid.NewGuid()).Take(count).ToListAsync();
		}
		public async Task<WordPartOfSpeech> FetchByWordId(int wordId)
		{
			return await _dbset.Include(x => x.Word).Include(y => y.PartOfSpeech).Where(x => x.WordId == wordId).FirstOrDefaultAsync();
		}
	}
}
