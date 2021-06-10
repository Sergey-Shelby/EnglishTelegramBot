using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Queries.WordTrainings
{
    public class FetchNextWordPartOfSpeechForTrainingQueryHandler : IQueryHandler<FetchNextWordPartOfSpeechForTrainingQuery, WordPartOfSpeech>
    {
        private IUnitOfWork _unitOfWork;
        private IUserManager _userManager;
        private IDispatcher _dispatcher;
        public FetchNextWordPartOfSpeechForTrainingQueryHandler(IUnitOfWork unitOfWork, IUserManager userManager, IDispatcher dispatcher)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _dispatcher = dispatcher;
        }

        public async Task<WordPartOfSpeech> Handle(FetchNextWordPartOfSpeechForTrainingQuery query)
        {
            var user = await _userManager.FetchCurrentUserAsync();

            var wordTrainings = await _dispatcher.Dispatch<List<WordTraining>>(new FetchCurrentWordTrainingsQuery());
            var nextWordTraining = wordTrainings.OrderBy(x => x.Id).FirstOrDefault(x => x.IsFinished == false);
            var nextWordPartOfSpeech = await _unitOfWork.WordPartOfSpeechRepository.FetchFullByWordId(nextWordTraining.WordPartOfSpeechId);

            return nextWordPartOfSpeech;
        }
    }
}
