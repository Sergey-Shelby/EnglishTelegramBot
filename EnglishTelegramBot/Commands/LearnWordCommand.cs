using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands
{
    public class LearnWordCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        public LearnWordCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var message = await context.ReplyAsync("Тренеровка слов запущена 🖋\nОтправьте !stop для завершения 🏁");

            await context.PinMessageAsync(message);
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD);

            var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
            var wordsPartOfSpeech = await _unitOfWork.WordPartOfSpeechRepository.FetchWordsByCount(5);
            await GenerateWordTrainingTable(_unitOfWork, wordsPartOfSpeech, user, TrainingType.Training);

            await next(context);
        }

        //TODO: use CQRS
        public static async Task GenerateWordTrainingTable(IUnitOfWork unitOfWork, IEnumerable<WordPartOfSpeech> wordsPartOfSpeech, User user, TrainingType trainingType)
        {
            var wordTrainingSet = new WordTrainingSet
            {
                Name = trainingType.ToString(),
                CreatedDate = DateTime.Now,
                UserId = user.Id
            };
            await unitOfWork.WordTrainingSetRepository.CreateAsync(wordTrainingSet);
            await unitOfWork.SaveChangesAsync();

            foreach (var wordPartOfSpeech in wordsPartOfSpeech)
            {
                var wordTraining = new WordTraining
                {
                    WordTrainingSetId = wordTrainingSet.Id,
                    WordId = wordPartOfSpeech.WordId,
                    IsFinished = false
                };
                await unitOfWork.WordTrainingRepository.CreateAsync(wordTraining);
            }
            await unitOfWork.SaveChangesAsync();
        }
    }

    public enum TrainingType
    {
        Test10,
        Training
    }
}
