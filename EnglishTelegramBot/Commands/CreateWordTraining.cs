using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;

namespace EnglishTelegramBot.Commands
{
	public class CreateWordTraining
	{
        private readonly IDispatcher _dispatcher;
        private readonly IStatusProvider _statusProvider;
        private readonly IEnumerable<WordPartOfSpeech> _wordPartOfSpeeches;
        private TelegrafContext _context;
        public CreateWordTraining(TelegrafContext context, IEnumerable<WordPartOfSpeech> wordPartOfSpeeches, IDispatcher dispatcher, IStatusProvider statusProvider)
		{
            _context = context;
            _dispatcher = dispatcher;
            _statusProvider = statusProvider;
            _wordPartOfSpeeches = wordPartOfSpeeches;

        }
		public async Task Execute(TrainingSetType trainingSetType)
		{
            var createWordTrainingSetCommand = new CreateWordTrainingCommand
            {
                WordsPartOfSpeech = _wordPartOfSpeeches,
                TrainingType = trainingSetType
            };
            var setId = await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            var wordTrainingState = new WordTrainingState
            {
                WordTrainings = _wordPartOfSpeeches.Select(x => new WordTraining
                {
                    WordPartOfSpeech = x,
                    WordTrainingSetId = setId
                }).ToList(),
                TrainingSetType = trainingSetType
            };
			_statusProvider.SetStatus(_context.User.Id, Status.LEARN_WORD, wordTrainingState);
		}
	}
}
