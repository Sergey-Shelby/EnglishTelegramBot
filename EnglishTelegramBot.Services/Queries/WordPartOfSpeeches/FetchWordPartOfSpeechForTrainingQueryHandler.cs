using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Consts;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordPartOfSpeeches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Queries.WordPartOfSpeeches
{
    public class FetchWordPartOfSpeechForTrainingQueryHandler : IQueryHandler<FetchWordPartOfSpeechForTrainingQuery, IEnumerable<WordPartOfSpeech>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;
        public FetchWordPartOfSpeechForTrainingQueryHandler(IUnitOfWork unitOfWork, IUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<WordPartOfSpeech>> Handle(FetchWordPartOfSpeechForTrainingQuery query)
        {
            var user = await _userManager.FetchCurrentUserAsync();

            var allWordPartOfSpeeches = await _unitOfWork.WordPartOfSpeechRepository.FetchAllFullAsync();
            var learnWords = await _unitOfWork.LearnWordRepository.FetchByUserId(user.Id);

           var wordPartOfSpeeches = allWordPartOfSpeeches
                .Where(x => !learnWords.Any(y => y.WordPartOfSpeechId == x.Id && y.Level >= 1))
                .OrderBy(x=> Guid.NewGuid())
                .Take(Parameters.COUNT_WORD_FOR_TRAINING)
                .ToList();

            return wordPartOfSpeeches;
        }
    }
}
