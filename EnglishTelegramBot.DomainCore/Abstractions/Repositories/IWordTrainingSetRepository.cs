using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IWordTrainingSetRepository : IBaseRepository<WordTrainingSet>
	{
		Task<List<WordTrainingSet>> FetchFullByUserIdAsync(int userId);
	}
}
