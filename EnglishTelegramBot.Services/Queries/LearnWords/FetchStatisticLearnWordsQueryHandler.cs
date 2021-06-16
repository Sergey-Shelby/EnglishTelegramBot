using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.LearnWords;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Queries.LearnWords
{
    public class FetchStatisticLearnWordsQueryHandler : IQueryHandler<FetchStatisticLearnWordsQuery, StatisticLearnWord>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;
        public FetchStatisticLearnWordsQueryHandler(IUnitOfWork unitOfWork, IUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<StatisticLearnWord> Handle(FetchStatisticLearnWordsQuery query)
        {
            var user = await _userManager.FetchCurrentUserAsync();

            var allWordPartOfSpeeches = await _unitOfWork.WordPartOfSpeechRepository.FetchAllAsync();
            var learnWords = await _unitOfWork.LearnWordRepository.FetchByUserId(user.Id);

            var statistic = new StatisticLearnWord
            {
                LearnedWordsCount = learnWords.Where(x => x.Level == 3).Count(),
                WordsOnRepeatCount = learnWords.Where(x => x.Level < 3 && x.Level > 0).Count()
            };

            statistic.NewWordsCount = allWordPartOfSpeeches.Count() - statistic.LearnedWordsCount - statistic.WordsOnRepeatCount;
            return statistic;
        }
    }
}
