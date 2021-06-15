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
	public class FetchWordPartOfSpeechForRepeatQueryFullHandler : IQueryHandler<FetchWordPartOfSpeechForRepeatQueryFull, IEnumerable<WordPartOfSpeech>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserManager _userManager;
        public FetchWordPartOfSpeechForRepeatQueryFullHandler(IUnitOfWork unitOfWork, IUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IEnumerable<WordPartOfSpeech>> Handle(FetchWordPartOfSpeechForRepeatQueryFull query)
        {
            var user = await _userManager.FetchCurrentUserAsync();

            var learnWords = await _unitOfWork.LearnWordRepository.FetchByUserId(user.Id);
            var wodrsPartOfSpeechForRepeatIds = learnWords
				.Where(x => x.Level >= 1 && x.Level <3 && x.NextLevelDate != null)
				.OrderBy(x => x.NextLevelDate)
				.Take(2)
                .Select(x => x.WordPartOfSpeechId);

            var allWordPartOfSpeeches = await _unitOfWork.WordPartOfSpeechRepository.FetchAllFullAsync();
            var wodrsPartOfSpeechForRepeat = allWordPartOfSpeeches.Where(x => wodrsPartOfSpeechForRepeatIds.Contains(x.Id));

            return wodrsPartOfSpeechForRepeat;
        }
    }
}
