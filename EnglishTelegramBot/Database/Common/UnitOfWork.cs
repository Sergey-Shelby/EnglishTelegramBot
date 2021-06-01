using EnglishTelegramBot.Database.Repositories;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Common
{
	public class UnitOfWork: IUnitOfWork
	{
		private readonly EnglishContext _englishContext;
		public UnitOfWork(EnglishContext englishContext)
		{
			this._englishContext = englishContext;
		}
		private IWordRepository _lazyWorkRepository;
		private IThemeRepository _lazyThemeRepository;
		private IPartOfSpeechRepository _lazyPartOfSpeechRepository;

		public IWordRepository WordRepository => _lazyWorkRepository?? new WordRepository(_englishContext);

		public IThemeRepository ThemeRepository => _lazyThemeRepository ?? new ThemeRepository(_englishContext);

		public IPartOfSpeechRepository PartOfSpeechRepository => _lazyPartOfSpeechRepository ?? new PartOfSpeechRepository(_englishContext);

		private 
		public async Task SaveChangesAsync()
		{
			await _englishContext.SaveChangesAsync();
		}
	}
}
