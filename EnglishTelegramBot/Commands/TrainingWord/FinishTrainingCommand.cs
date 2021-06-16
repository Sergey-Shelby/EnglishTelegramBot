using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using EnglishTelegramBot.DomainCore.Models.LearnWords;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using EnglishTelegramBot.DomainCore.Enums;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class FinishTrainingCommand : BaseCommand
    {
        private readonly IStatusProvider _statusProvider;
        private readonly IDispatcher _dispatcher;

        public FinishTrainingCommand(IStatusProvider statusProvider, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var state = _statusProvider.GetStatus<WordTrainingState>(context.User.Id);

            await _dispatcher.Dispatch(new UpdateWordTrainingCommand { WordTrainings = state.Details.WordTrainings });

            if (state.Details.TrainingSetType == TrainingSetType.Training)
            {
                await _dispatcher.Dispatch(new CreateLearnWordCommand { WordTrainings = state.Details.WordTrainings });
            }

            var listWrongWords = state.Details.WordTrainings
                .Where(x => x.InputEnglish == false || x.EnglishSelect == false || x.RussianSelect == false)
                .ToList();

            var countRusSelectTrue = state.Details.WordTrainings.Where(x => x.RussianSelect == true).Count();
            var countEngSelectTrue = state.Details.WordTrainings.Where(x => x.EnglishSelect == true).Count();
            var countEngInputTrue = state.Details.WordTrainings.Where(x => x.InputEnglish == true).Count();


            double sumCount= (countRusSelectTrue + countEngSelectTrue + countEngInputTrue);
            var procent = Math.Truncate(sumCount / (3 * state.Details.WordTrainings.Count()) * 100);

            var messageList = new StringBuilder();
            var message = state.Details.TrainingSetType == TrainingSetType.Training ? $"Тренировка завершена!" : "Тест завершен!";
            messageList.Append($"{message}\nРезультат — {procent}%\n");

            if (procent < 100)
            {
                messageList.Append($"Повторите следующие слова:\n");
                listWrongWords.ForEach(x => messageList.AppendLine($"<i>{x.WordPartOfSpeech.Word.EnglishWord} — {x.WordPartOfSpeech.WordPartOfSpeechDatas[0].Word}</i>"));
            }

            await context.ReplyAsyncWithHtml($"{messageList}");
            await context.UnpinMessageAsync();
            _statusProvider.ClearStatus(context.User.Id);
            await next(context);
        }
    }
}
