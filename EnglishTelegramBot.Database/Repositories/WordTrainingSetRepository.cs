using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace EnglishTelegramBot.Database.Repositories
{
	public class WordTrainingSetRepository : BaseRepository<WordTrainingSet>, IWordTrainingSetRepository
	{
		public WordTrainingSetRepository(EnglishContext englishContext) : base(englishContext)
		{

		}
	}
}
