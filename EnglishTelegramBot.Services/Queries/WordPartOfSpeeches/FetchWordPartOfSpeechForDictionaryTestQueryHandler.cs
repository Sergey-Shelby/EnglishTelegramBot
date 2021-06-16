using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordPartOfSpeeches;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Queries.WordPartOfSpeeches
{
	public class FetchWordPartOfSpeechForDictionaryTestQueryHandler : IQueryHandler<FetchWordPartOfSpeechForDictionaryTestQuery, IEnumerable<WordPartOfSpeech>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;
        public FetchWordPartOfSpeechForDictionaryTestQueryHandler(IUnitOfWork unitOfWork, IUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<WordPartOfSpeech>> Handle(FetchWordPartOfSpeechForDictionaryTestQuery query)
        {
            var user = await _userManager.FetchCurrentUserAsync();

            var allWordPartOfSpeeches = await _unitOfWork.WordPartOfSpeechRepository.FetchAllFullAsync();
            var learnWords = await _unitOfWork.LearnWordRepository.FetchByUserId(user.Id);

            var wordPartOfSpeeches = allWordPartOfSpeeches
                 .Where(x => learnWords.Any(y => y.WordPartOfSpeechId == x.Id))
				 .OrderBy(x => Guid.NewGuid())
				 .Take(10);

            return wordPartOfSpeeches;
        }
    }
}
