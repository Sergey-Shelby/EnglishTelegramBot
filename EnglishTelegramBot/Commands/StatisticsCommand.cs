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
            var wordTrainings = await _unitOfWork.WordTrainigRepository.FetchAllByUserIdAsync(user.Id);
            var listWordTraining = wordTrainings.OrderBy(x => x.Result).ToList();
            var wordList = new StringBuilder();
            listWordTraining.ForEach(x => wordList.AppendLine($"{x.Word.English} - {x.Word.Russian} | {x.Result}"));

			await context.ReplyAsync($"Cтатистика:\n{wordList}");
        }
	}
}
