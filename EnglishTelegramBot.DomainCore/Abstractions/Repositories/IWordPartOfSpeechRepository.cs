using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IWordPartOfSpeechRepository : IBaseRepository<WordPartOfSpeech>
	{
		Task<List<WordPartOfSpeech>> FetchFullByCount(int count);
		Task<WordPartOfSpeech> FetchFullByWordId(int wordId);
	}
}
