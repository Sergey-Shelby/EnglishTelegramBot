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
                        NextLevelDate = DateTime.Today.AddMinutes(3),
						SelectRus = (bool)word.RussianSelect ? 1 : 0,
                        SelectEng = (bool)word.EnglishSelect ? 1: 0,
                        Input = (bool)word.InputEnglish ? 1: 0,
                    };
                    Update(learnWordNew);
                    await _unitOfWork.LearnWordRepository.CreateAsync(learnWordNew);
				}
                else
				{
                    learnWord.NextLevelDate = DateTime.Now.AddDays(1);
                    learnWord.SelectEng = (bool)word.EnglishSelect ? learnWord.SelectEng + 0.6 : learnWord.SelectEng - 0.3;
                    learnWord.SelectRus = (bool)word.RussianSelect ? learnWord.SelectRus + 0.6 : learnWord.SelectRus - 0.3;
                    learnWord.Input = (bool)word.InputEnglish ? learnWord.Input + 0.6 : learnWord.Input - 0.3;
                    Update(learnWord);
                    await _unitOfWork.LearnWordRepository.UpdateAsync(learnWord);
                }
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public void Update(LearnWord learnWord)
        {
            if (learnWord.Level == 3)
                return;

            var necessaryProgress = learnWord.Level + 1;
            if (learnWord.SelectRus >= necessaryProgress && learnWord.SelectRus >= necessaryProgress && learnWord.Input >= necessaryProgress)
            {
                learnWord.NextLevelDate = DateTime.Today.AddDays(5);
                learnWord.Level++;
            }
        }
    }
}
