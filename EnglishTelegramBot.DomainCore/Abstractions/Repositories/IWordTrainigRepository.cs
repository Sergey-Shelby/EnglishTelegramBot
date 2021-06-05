using EnglishTelegramBot.DomainCore.Entities;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IWordTrainigRepository : IBaseRepository<WordTrainig>
	{
		Task<WordTrainig> FetchByWordIdAndUserId(int wordId, int userId);
	}
}
