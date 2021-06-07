using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions
{
	public interface IUnitOfWork
	{
		IWordRepository WordRepository { get; }
		IThemeRepository ThemeRepository { get; }
		IPartOfSpeechRepository PartOfSpeechRepository { get; } 
		IUserRepository UserRepository { get; }
		IWordTrainingRepository WordTrainingRepository { get; } 
		IWordPartOfSpeechRepository WordPartOfSpeechRepository { get; }
		IWordPartOfSpeechDataRepository WordPartOfSpeechDataRepository { get; }
		IThemeWordsRepository ThemeWordsRepository { get; }
		IWordTrainingSetRepository WordTrainingSetRepository { get; }
		Task SaveChangesAsync();
	}
}
