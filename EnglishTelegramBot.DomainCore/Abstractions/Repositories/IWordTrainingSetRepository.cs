using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IWordTrainingSetRepository : IBaseRepository<WordTrainingSet>
	{
		Task<List<WordTrainingSet>> FetchAllByUserIdAsync(int userId);
		Task<WordTrainingSet> FetchLastByUserId(int userId);
	}
}
