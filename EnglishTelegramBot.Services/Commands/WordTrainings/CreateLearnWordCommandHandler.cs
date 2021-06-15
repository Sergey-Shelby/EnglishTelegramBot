using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.LearnWords;
using System;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services.Commands.WordTrainings
{
	public class CreateLearnWordCommandHandler : ICommandHandler<CreateLearnWordCommand>
    {
        private IUnitOfWork _unitOfWork;
        private IUserManager _userManager;
        public CreateLearnWordCommandHandler(IUnitOfWork unitOfWork, IUserManager userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task Handle(CreateLearnWordCommand command)
        {
            var user = await _userManager.FetchCurrentUserAsync();

			foreach (var word in command.WordTrainings)
			{
                var learnWord = await _unitOfWork.LearnWordRepository.FetchByWordPartOfSpeechIdAsync(word.WordPartOfSpeech.Id);
                if (learnWord == null)
				{
                    var learnWordNew = new LearnWord()
                    {
                        UserId = user.Id,
                        WordPartOfSpeechId = word.WordPartOfSpeech.Id,
                        Level = 0,
                        NextLevelDate = null,
						SelectRus = (bool)word.RussianSelect ? 1 : 0,
                        SelectEng = (bool)word.EnglishSelect ? 1: 0,
                        Input = (bool)word.InputEnglish ? 1: 0,
                    };
                    CheckIncreaseLevel(learnWordNew);
                    await _unitOfWork.LearnWordRepository.CreateAsync(learnWordNew);
				}
                else
				{
                    learnWord.SelectEng = ChangeProgress(learnWord, word.EnglishSelect, learnWord.SelectEng);
                    learnWord.SelectRus = ChangeProgress(learnWord, word.RussianSelect, learnWord.SelectRus);
                    learnWord.Input = ChangeProgress(learnWord, word.InputEnglish, learnWord.Input);

                    CheckIncreaseLevel(learnWord);
                    await _unitOfWork.LearnWordRepository.UpdateAsync(learnWord);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }

        private double ChangeProgress(LearnWord learnWord, bool? result, double currentValue)
        {
            if (result == true)
            {
                var progressCoef = learnWord.Level == 0 ? 0.6 : 1;
                return currentValue + progressCoef;
            }
            else
            {
                var progressCoef = -0.3d;
                var newValue = currentValue + progressCoef;
                return newValue > 0 ? newValue : 0;
            }
        }

        public void CheckIncreaseLevel(LearnWord learnWord)
        {
            if (learnWord.Level == 3)
                return;

            var necessaryProgress = learnWord.Level + 1;
            if (learnWord.SelectRus >= necessaryProgress && learnWord.SelectRus >= necessaryProgress && learnWord.Input >= necessaryProgress)
            {
                learnWord.NextLevelDate = DateTime.Now.AddMinutes(3);
                learnWord.Level++;
            }
        }
    }
}
