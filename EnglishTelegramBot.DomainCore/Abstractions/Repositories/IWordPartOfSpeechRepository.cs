using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IWordPartOfSpeechRepository : IBaseRepository<WordPartOfSpeech>
	{
		Task<List<WordPartOfSpeech>> FetchWordsByCount(int count);
		Task<WordPartOfSpeech> FetchByWordId(int wordId);
	}
}
