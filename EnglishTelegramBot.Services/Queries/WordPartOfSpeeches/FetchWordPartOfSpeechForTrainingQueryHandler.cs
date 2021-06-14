using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordPartOfSpeeches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Queries.WordPartOfSpeeches
{
    public class FetchWordPartOfSpeechForTrainingQueryHandler : IQueryHandler<FetchWordPartOfSpeechForTrainingQuery, List<WordPartOfSpeech>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public FetchWordPartOfSpeechForTrainingQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WordPartOfSpeech>> Handle(FetchWordPartOfSpeechForTrainingQuery query)
        {
            var wordPartOfSpeeches = await _unitOfWork.WordPartOfSpeechRepository.FetchFullAsync(2);
            return wordPartOfSpeeches;
        }
    }
}
