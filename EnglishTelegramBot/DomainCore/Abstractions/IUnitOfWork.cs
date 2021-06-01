using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions
{
	interface IUnitOfWork
	{
		IWordRepository WordRepository { get; }
		IThemeRepository ThemeRepository { get; }
		IPartOfSpeechRepository PartOfSpeechRepository { get; } 
		Task SaveChangesAsync();
	}
}
