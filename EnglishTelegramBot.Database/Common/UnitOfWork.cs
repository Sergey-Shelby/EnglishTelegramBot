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
		private IWordTrainingRepository _lazyWordTrainingRepository;
		private IWordPartOfSpeechRepository _lazyIWordPartOfSpeechRepository;
		private IWordPartOfSpeechDataRepository _lazyIWordPartOfSpeechDataRepository;
		private IThemeWordsRepository _lazyIThemeWordsRepository;
		private IWordTrainingSetRepository _lazyWordTrainingSetRepository;
		private ILearnWordRepository _lazyLearnWordRepository; 

		public IWordRepository WordRepository => _lazyWorkRepository ??= new WordRepository(_englishContext);
		public IThemeRepository ThemeRepository => _lazyThemeRepository ??= new ThemeRepository(_englishContext);
		public IPartOfSpeechRepository PartOfSpeechRepository => _lazyPartOfSpeechRepository ??= new PartOfSpeechRepository(_englishContext);
		public IUserRepository UserRepository => _lazyUserRepository ??= new UserRepository(_englishContext);
		public IWordTrainingRepository WordTrainingRepository => _lazyWordTrainingRepository ??= new WordTrainingRepository(_englishContext);
		public IWordPartOfSpeechRepository WordPartOfSpeechRepository => _lazyIWordPartOfSpeechRepository ??= new WordPartOfSpeechRepository(_englishContext);
		public IWordPartOfSpeechDataRepository WordPartOfSpeechDataRepository => _lazyIWordPartOfSpeechDataRepository ??= new WordPartOfSpeechDataRepository(_englishContext);
		public IThemeWordsRepository ThemeWordsRepository => _lazyIThemeWordsRepository ??= new ThemeWordsRepository(_englishContext);
		public IWordTrainingSetRepository WordTrainingSetRepository => _lazyWordTrainingSetRepository ??= new WordTrainingSetRepository(_englishContext);
		public ILearnWordRepository LearnWordRepository => _lazyLearnWordRepository ??= new LearnWordRepository(_englishContext);

		public async Task SaveChangesAsync()
		{
			await _englishContext.SaveChangesAsync();
		}
	}
}
