﻿using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
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
        private readonly IStatusProvider _statusProvider;
        private readonly IDispatcher _disatcher;

        public FinishTrainingCommand(IStatusProvider statusProvider, IDispatcher disatcher)
        {
            _statusProvider = statusProvider;
            _disatcher = disatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var state = _statusProvider.GetStatus<WordTrainingState>(context.User.Id);

            await _disatcher.Dispatch(new UpdateWordTrainingCommand { WordTrainings = state.Details.WordTrainings });

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
                listWrongWords.ForEach(x => messageList.AppendLine($"<i>{x.WordPartOfSpeech.WordPartOfSpeechDatas[0].Word} — {x.WordPartOfSpeech.Word.RussianWord}</i>"));
            }
            await context.ReplyAsyncWithHtml($"{messageList}");
            await context.UnpinMessageAsync();
            _statusProvider.ClearStatus(context.User.Id);
            await next(context);
        }
    }
}
