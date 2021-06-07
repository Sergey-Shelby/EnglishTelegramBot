using EnglishTelegramBot.Database.Repositories;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Common
{
	public class UnitOfWork: IUnitOfWork
	{
		private readonly EnglishContext _englishContext;
		public UnitOfWork(EnglishContext englishContext)
		{
			_englishContext = englishContext;
		}

		private IWordRepository _lazyWorkRepository;
		private IThemeRepository _lazyThemeRepository;
		private IPartOfSpeechRepository _lazyPartOfSpeechRepository;
		private IUserRepository _lazyUserRepository;
		private IWordTrainigRepository _lazyWordTrainingRepository;
		private IWordPartOfSpeechRepository _lazyIWordPartOfSpeechRepository;
		private IWordPartOfSpeechDataRepository _lazyIWordPartOfSpeechDataRepository;
		private IThemeWordsRepository _lazyIThemeWordsRepository;

		public IWordRepository WordRepository => _lazyWorkRepository?? new WordRepository(_englishContext);
		public IThemeRepository ThemeRepository => _lazyThemeRepository ?? new ThemeRepository(_englishContext);
		public IPartOfSpeechRepository PartOfSpeechRepository => _lazyPartOfSpeechRepository ?? new PartOfSpeechRepository(_englishContext);
		public IUserRepository UserRepository => _lazyUserRepository ?? new UserRepository(_englishContext);
		public IWordTrainigRepository WordTrainigRepository => _lazyWordTrainingRepository ?? new WordTrainingRepository(_englishContext);

		public IWordPartOfSpeechRepository WordPartOfSpeechRepository => _lazyIWordPartOfSpeechRepository ?? new WordPartOfSpeechRepository(_englishContext);
		public IWordPartOfSpeechDataRepository WordPartOfSpeechDataRepository => _lazyIWordPartOfSpeechDataRepository ?? new WordPartOfSpeechDataRepository(_englishContext);
		public IThemeWordsRepository ThemeWordsRepository => _lazyIThemeWordsRepository ?? new ThemeWordsRepository(_englishContext);

		public async Task SaveChangesAsync()
		{
			await _englishContext.SaveChangesAsync();
		}
	}
}
