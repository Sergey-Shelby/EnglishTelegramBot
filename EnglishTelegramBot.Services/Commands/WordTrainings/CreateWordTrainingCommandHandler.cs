using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Commands.WordTrainings
{
    public class CreateWordTrainingCommandHandler : ICommandResultHandler<CreateWordTrainingCommand, int>
    {
        private IUnitOfWork _unitOfWork;
        private IUserManager _userManager;
        public CreateWordTrainingCommandHandler(IUnitOfWork unitOfWork, IUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<int> Handle(CreateWordTrainingCommand command)
        {
            var user = await _userManager.FetchCurrentUserAsync();

            var wordTrainingSet = new WordTrainingSet
            {
                Name = command.TrainingType.ToString(),
                CreatedDate = DateTime.Now,
                UserId = user.Id
            };
            await _unitOfWork.WordTrainingSetRepository.CreateAsync(wordTrainingSet);
            await _unitOfWork.SaveChangesAsync();

            foreach (var wordPartOfSpeech in command.WordsPartOfSpeech)
            {
                var wordTraining = new WordTraining
                {
                    WordTrainingSetId = wordTrainingSet.Id,
                    WordPartOfSpeechId = wordPartOfSpeech.Id,
                    IsFinished = false
                };
                await _unitOfWork.WordTrainingRepository.CreateAsync(wordTraining);
            }
            await _unitOfWork.SaveChangesAsync();

            return wordTrainingSet.Id;
        }
    }
}
