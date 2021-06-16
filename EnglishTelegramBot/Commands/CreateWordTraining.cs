using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Commands
{
	public class CreateWordTraining
	{
        private readonly IDispatcher _dispatcher;
        private readonly IStatusProvider _statusProvider;
        private readonly TrainingSetType _trainingSetType;
        private readonly IEnumerable<WordPartOfSpeech> _wordPartOfSpeeches;
        public CreateWordTraining(IEnumerable<WordPartOfSpeech> wordPartOfSpeeches, IDispatcher dispatcher, IStatusProvider statusProvider, TrainingSetType trainingSetType)
		{
            _dispatcher = dispatcher;
            _statusProvider = statusProvider;
            _trainingSetType = trainingSetType;
            _wordPartOfSpeeches = wordPartOfSpeeches;

        }
		public async Task Execute()
		{
            var createWordTrainingSetCommand = new CreateWordTrainingCommand
            {
                WordsPartOfSpeech = _wordPartOfSpeeches,
                TrainingType = _trainingSetType
            };
            var setId = await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            var wordTrainingState = new WordTrainingState
            {
                WordTrainings = _wordPartOfSpeeches.Select(x => new WordTraining
                {
                    WordPartOfSpeech = x,
                    WordTrainingSetId = setId
                }).ToList(),
                TrainingSetType = _trainingSetType
            };
            //_statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, wordTrainingState);
        }
	}
}
