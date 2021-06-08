using EnglishTelegramBot.DomainCore.Entities;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IThemeWordsRepository : IBaseRepository<ThemeWords>
	{
		Task<ThemeWords> FetchByPartOfSpeechId(int partOfSpeechId);
	}
}
