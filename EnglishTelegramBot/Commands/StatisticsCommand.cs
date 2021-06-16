using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using System.Text;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.LearnWords;
using Telegram.Bot.Types.Enums;

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

			var learnedWordsPercent = (double)statistic.LearnedWordsCount / (statistic.NewWordsCount + statistic.WordsOnRepeatCount) * 100;
			var wordsOnRepeatPercent = (double)statistic.WordsOnRepeatCount / (statistic.NewWordsCount + statistic.LearnedWordsCount) * 100;
			var newWordsCountPercent = (double)statistic.NewWordsCount / (statistic.WordsOnRepeatCount + statistic.LearnedWordsCount) * 100;

			var messageText = new StringBuilder();
			messageText.AppendLine("*Статистика изучения слов:*");
			messageText.AppendLine($"Изученных слов: {statistic.LearnedWordsCount} ({learnedWordsPercent.ToString("0")}%)");
			messageText.AppendLine($"Слов на повторе: {statistic.WordsOnRepeatCount} ({wordsOnRepeatPercent.ToString("0")}%)");
			messageText.AppendLine($"Неизученных слов: {statistic.NewWordsCount} ({newWordsCountPercent.ToString("0")}%)");

			await context.ReplyAsync(messageText.ToString(), parseMode: ParseMode.Markdown);
        }
	}
}
