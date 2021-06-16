using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Queries.LearnWords
{
    public class FetchWordTestsHistoryQueryHandler : IQueryHandler<FetchWordTestsHistoryQuery, IEnumerable<WordTestsHistory>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;
        public FetchWordTestsHistoryQueryHandler(IUnitOfWork unitOfWork, IUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<WordTestsHistory>> Handle(FetchWordTestsHistoryQuery query)
        {
            var user = await _userManager.FetchCurrentUserAsync();

            var list = new List<WordTestsHistory>();
            var wordTrainingSets = await _unitOfWork.WordTrainingSetRepository.FetchFullByUserIdAsync(user.Id);
            foreach (var wordTrainingSet in wordTrainingSets.Where(x => x.Name == TrainingSetType.FullTest.ToString() || x.Name == TrainingSetType.DictionaryTest.ToString()))
            {
                var testHistory = new WordTestsHistory
                {
                    DateTime = wordTrainingSet.CreatedDate,
                    TrainingType = Enum.Parse<TrainingSetType>(wordTrainingSet.Name)
                };

                var countRightInput = wordTrainingSet.WordTraining.Count(x => x.InputEnglish.Value);
                var countRightSelectRus = wordTrainingSet.WordTraining.Count(x => x.InputEnglish.Value);
                var countRightSelectEng = wordTrainingSet.WordTraining.Count(x => x.InputEnglish.Value);

                testHistory.Result = (double)(countRightInput + countRightSelectRus + countRightSelectEng) / (wordTrainingSet.WordTraining.Count * 3) * 100;

                list.Add(testHistory);
            }

            return list;
        }
    }
}
