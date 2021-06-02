using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions
{
	public interface IUnitOfWork
	{
		IWordRepository WordRepository { get; }
		IThemeRepository ThemeRepository { get; }
		IPartOfSpeechRepository PartOfSpeechRepository { get; } 
		Task SaveChangesAsync();
	}
}
