using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordPartOfSpeeches;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;


namespace EnglishTelegramBot.Commands
{
	public class LearnWordRepeatCommand : BaseCommand
    {
        private readonly IUnitOfWork _unitOfWork;
        IStatusProvider _statusProvider;
        IDispatcher _dispatcher;
        public LearnWordRepeatCommand(IUnitOfWork unitOfWork, IStatusProvider statusProvider, IDispatcher dispatcher)
        {
            _unitOfWork = unitOfWork;
            _statusProvider = statusProvider;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var message = await context.ReplyAsync("Тренеровка повтора слов запущена 🖋\nОтправьте !stop для завершения 🏁");
            await context.PinMessageAsync(message);

            //var wordPartOfSpeeches = await _dispatcher.Dispatch<List<WordPartOfSpeech>>(new FetchWordPartOfSpeechForTrainingQuery());

			var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
			var repeatWordsPartOfSpeechIds = await _unitOfWork.LearnWordRepository.FetchWordPartOfSpeechRepeat(user.Id);
            var wordsOfSpeech = await _unitOfWork.WordPartOfSpeechRepository.FetchFullAsync(600);
            var wordPartOfSpeeches = wordsOfSpeech.Where(p => repeatWordsPartOfSpeechIds.Contains(p.Id)).ToList();  

            //TODO: Next part of method repeat in each type training.
            //Bad practice
            //Replace In SQRC ? but we change status -_-
            var createWordTrainingSetCommand = new CreateWordTrainingCommand
            {
                WordsPartOfSpeech = wordPartOfSpeeches,
                TrainingType = TrainingSetType.Training
            };
            var setId = await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            var wordTrainingState = new WordTrainingState
            {
                WordTrainings = wordPartOfSpeeches.Select(x => new WordTraining
                {
                    WordPartOfSpeech = x,
                    WordTrainingSetId = setId
                }).ToList(),
                TrainingSetType = TrainingSetType.Training
            };
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, wordTrainingState);

            await next(context);
        }
    }
}
