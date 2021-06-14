using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.LearnWords;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class FinishTrainingCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        IDispatcher _dispatcher;
        public FinishTrainingCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var state = _statusProvider.GetStatus<WordTrainingState>(context.User.Id);
			//-----------
			//foreach (var item in state.Details.WordTrainings)
			//{
   //             item.WordPartOfSpeechId = item.WordPartOfSpeech.Id;
			//	await _unitOfWork.WordTrainingRepository.CreateAsync(item);
   //             await _unitOfWork.WordTrainingRepository.SaveAsync();
   //         }
            //------------

            var listWrongWords = state.Details.WordTrainings
                .Where(x => x.InputEnglish == false || x.EnglishSelect == false || x.RussianSelect == false)
                .ToList();

            double wordTrainingTrueAnswerCount = 5 - listWrongWords.Count();
            var procent = Math.Truncate(wordTrainingTrueAnswerCount / 5 * 100);

            var messageList = new StringBuilder();

            messageList.Append($"Тренировка завершена! Результат — {procent}%\n");

            if (procent < 100)
            {
                messageList.Append($"Повторите следующие слова:\n");
                //TODO: WordPartOfSpeech.Word.EnglishWord - invalid, need see WordPartOfSpeechData
                listWrongWords.ForEach(x => messageList.AppendLine($"<i>{x.WordPartOfSpeech.WordPartOfSpeechDatas[0].Word} — {x.WordPartOfSpeech.Word.RussianWord}</i>"));
            }

            await _dispatcher.Dispatch(new CreateLearnWordCommand { WordTrainings = state.Details.WordTrainings });

            await context.ReplyAsyncWithHtml($"{messageList}");
            await context.UnpinMessageAsync();
            _statusProvider.ClearStatus(context.User.Id);
            await next(context);
        }
    }
}
