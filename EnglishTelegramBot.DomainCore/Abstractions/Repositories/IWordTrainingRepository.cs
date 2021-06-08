using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IWordTrainingRepository : IBaseRepository<WordTraining> 
	{
		//Task<WordTraining> FetchByWordIdAndUserId(int wordId, int userId);
		Task<List<WordTraining>> FetchAllByUserIdAsync(int userId);
		Task<List<WordTraining>> FetchBySetAsync(int setId);
	}
}
