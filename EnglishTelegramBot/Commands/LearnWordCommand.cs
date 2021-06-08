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
            _statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD, TrainingType.Training);

            var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
            var words = await _unitOfWork.WordRepository.FetchWordsByCount(5);
            await GenerateWordTrainingTable(words, user);

            await next(context);
        }

        private async Task GenerateWordTrainingTable(IEnumerable<Word> words, User user)
        {
            var wordTrainingSet = new WordTrainingSet
            {
                Name = TrainingType.Training.ToString(),
                CreatedDate = DateTime.Now,
                UserId = user.Id
            };
            await _unitOfWork.WordTrainingSetRepository.CreateAsync(wordTrainingSet);
            await _unitOfWork.SaveChangesAsync();

            foreach (var word in words)
            {
                var wordTraining = new WordTraining
                {
                    SetId = wordTrainingSet.Id,
                    WordId = word.Id
                };
                await _unitOfWork.WordTrainingRepository.CreateAsync(wordTraining);
            }
        }
    }

    public enum TrainingType
    {
        Test10,
        Training
    }
}
