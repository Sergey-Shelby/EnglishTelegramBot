using EnglishTelegramBot.DomainCore.Entities;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IWordTrainigRepository : IBaseRepository<WordTraining>
	{
		Task<WordTraining> FetchByWordIdAndUserId(int wordId, int userId);
	}
}
