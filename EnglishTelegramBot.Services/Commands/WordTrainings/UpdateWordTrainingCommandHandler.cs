using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Commands.WordTrainings
{
    public class UpdateWordTrainingCommandHandler : ICommandHandler<UpdateWordTrainingCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateWordTrainingCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateWordTrainingCommand command)
        {
            foreach (var wordTrainingResult in command.WordTrainings)
            {
                var wordTraining = await _unitOfWork.WordTrainingRepository.FetchBySetAsync(wordTrainingResult.WordTrainingSetId, wordTrainingResult.WordPartOfSpeech.Id);
                wordTraining.InputEnglish = wordTrainingResult.InputEnglish;
                wordTraining.RussianSelect = wordTrainingResult.RussianSelect;
                wordTraining.EnglishSelect = wordTrainingResult.EnglishSelect;

                await _unitOfWork.WordTrainingRepository.UpdateAsync(wordTraining);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
