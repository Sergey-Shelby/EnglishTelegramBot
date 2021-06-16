using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using EnglishTelegramBot.DomainCore.Abstractions;
using System.Linq;
using System.Text;

namespace EnglishTelegramBot.Commands
{
	public class StatisticsCommand : BaseCommand
    {
        private IUnitOfWork _unitOfWork;
        public StatisticsCommand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);

            var wordTrainingSets = await _unitOfWork.WordTrainingSetRepository.FetchAllByUserIdAsync(user.Id);

            var statList = new StringBuilder();

			//TODO: edit

			wordTrainingSets.ForEach(x => statList.AppendLine($"{x.TrainingType} [{x.CreatedDate}] — {x.WordTraining.Where(x => x.RussianSelect == true).Count() / x.WordTraining.Count() * 100}%"));

			//var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
			//var wordTrainings = await _unitOfWork.WordTrainingRepository.FetchAllByUserIdAsync(user.Id);
			//var listWordTrainings = wordTrainings
			//    .GroupBy(x => x.Word.EnglishWord)
			//    .Select(x => new 
			//    { 
			//        Word = x.Key, 
			//        CountTrue = x.Count(y => y.Result == true), 
			//        CountFalse = x.Count(z => z.Result == false)
			//    })
			//    .OrderByDescending(z => z.CountTrue + z.CountFalse)
			//    .ThenByDescending(o => o.CountTrue).ToList();

			//var wordList = new StringBuilder();
			//listWordTrainings.ForEach(x => wordList.AppendLine($"{x.Word}: ✅ — { x.CountTrue}, ❌ {x.CountFalse}."));

			await context.ReplyAsync($"Cтатистика:\n{statList}");
        }
	}
}
