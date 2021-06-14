using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface ILearnWordRepository : IBaseRepository<LearnWord>
	{
		Task<List<LearnWord>> FetchWordPartOfSpeechForRepeat(int take, int userId);
		Task<LearnWord> FetchByWordPartOfSpeechIdAsync(int id);
	}
}
