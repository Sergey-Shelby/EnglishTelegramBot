using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using System.Text;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.LearnWords;
using Telegram.Bot.Types.Enums;
using EnglishTelegramBot.Services.Queries.LearnWords;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Linq;
using EnglishTelegramBot.DomainCore.Enums;

namespace EnglishTelegramBot.Commands
{
	public class StatisticsCommand : BaseCommand
    {
        private readonly IDispatcher _dispatcher;
        public StatisticsCommand(IDispatcher dispatcher)
        {
			_dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var statistic = await _dispatcher.Dispatch<StatisticLearnWord>(new FetchStatisticLearnWordsQuery());

			var fullCount = statistic.NewWordsCount + statistic.WordsOnRepeatCount + statistic.LearnedWordsCount;
			var learnedWordsPercent = (double)statistic.LearnedWordsCount / fullCount * 100;
			var wordsOnRepeatPercent = (double)statistic.WordsOnRepeatCount / fullCount * 100;
			var newWordsCountPercent = (double)statistic.NewWordsCount / fullCount * 100;

			Serilog.Log.Information("Stat.");

			var messageText = new StringBuilder();
			messageText.AppendLine("*Статистика изучения слов:*");
			messageText.AppendLine($"Изученных слов: {statistic.LearnedWordsCount} ({learnedWordsPercent.ToString("0")}%)");
			messageText.AppendLine($"Слов на повторе: {statistic.WordsOnRepeatCount} ({wordsOnRepeatPercent.ToString("0")}%)");
			messageText.AppendLine($"Неизученных слов: {statistic.NewWordsCount} ({newWordsCountPercent.ToString("0")}%)");

			var testHistories = await _dispatcher.Dispatch<IEnumerable<WordTestsHistory>>(new FetchWordTestsHistoryQuery());

			if (testHistories.Any())
			{
				messageText.AppendLine();
				messageText.AppendLine("*Статистика проверки знаний:*");
				testHistories.ToList().ForEach(x => messageText.AppendLine($"{x.DateTime.Date.ToString("d")} - {x.Result.ToString("0")}% ({(x.TrainingType == TrainingSetType.FullTest ? "все слова" : "выученные слова")})"));
			}
			await context.ReplyAsync(messageText.ToString(), parseMode: ParseMode.Markdown);
        }
	}
}
