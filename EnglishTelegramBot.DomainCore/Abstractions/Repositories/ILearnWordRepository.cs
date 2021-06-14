using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface ILearnWordRepository : IBaseRepository<LearnWord>
	{
		Task<List<LearnWord>> FetchWordPartOfSpeechForRepeat(int userId);
		Task<List<int>> FetchWordPartOfSpeechNotInRepeat(int userId);
		Task<LearnWord> FetchByWordPartOfSpeechIdAsync(int id);
		Task<List<int>> FetchWordPartOfSpeechRepeat(int userId);
	}
}
