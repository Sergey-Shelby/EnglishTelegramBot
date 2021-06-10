using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Queries.WordTrainings
{
    public class FetchCurrentWordTrainingsQueryHandler : IQueryHandler<FetchCurrentWordTrainingsQuery, List<WordTraining>>
    {
        private IUnitOfWork _unitOfWork;
        private IUserManager _userManager;
        public FetchCurrentWordTrainingsQueryHandler(IUnitOfWork unitOfWork, IUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<List<WordTraining>> Handle(FetchCurrentWordTrainingsQuery query)
        {
            var user = await _userManager.FetchCurrentUserAsync();

            var trainingSets = await _unitOfWork.WordTrainingSetRepository.FetchAllAsync();
            var currentSet = trainingSets.OrderByDescending(x => x.CreatedDate).Where(x => x.UserId == user.Id).FirstOrDefault();
            return await _unitOfWork.WordTrainingRepository.FetchBySetAsync(currentSet.Id);
        }
    }
}
