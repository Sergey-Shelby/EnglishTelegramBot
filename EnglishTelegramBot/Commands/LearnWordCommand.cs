﻿using EnglishTelegramBot.Constants;
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
            var words = await _unitOfWork.WordRepository.FetchWordsByCount(5);
            await GenerateWordTrainingTable(_unitOfWork, words, user, TrainingType.Training);

            await next(context);
        }

        //TODO: use CQRS
        public static async Task GenerateWordTrainingTable(IUnitOfWork unitOfWork, IEnumerable<Word> words, User user, TrainingType trainingType)
        {
            var wordTrainingSet = new WordTrainingSet
            {
                Name = trainingType.ToString(),
                CreatedDate = DateTime.Now,
                UserId = user.Id
            };
            await unitOfWork.WordTrainingSetRepository.CreateAsync(wordTrainingSet);
            await unitOfWork.SaveChangesAsync();

            foreach (var word in words)
            {
                var wordTraining = new WordTraining
                {
                    SetId = wordTrainingSet.Id,
                    WordId = word.Id
                };
                await unitOfWork.WordTrainingRepository.CreateAsync(wordTraining);
            }
        }
    }

    public enum TrainingType
    {
        Test10,
        Training
    }
}
