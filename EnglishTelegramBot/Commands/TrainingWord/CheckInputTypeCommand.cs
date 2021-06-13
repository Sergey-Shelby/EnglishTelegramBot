using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands.TrainingWord
{
	public class CheckInputTypeCommand : BaseCommand
    {
        private IStatusProvider _statusProvider;
        private IUnitOfWork _unitOfWork;
        private IDispatcher _dispatcher;
        public CheckInputTypeCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var state = _statusProvider.GetStatus<WordTrainingState>(context.User.Id); if (state.Details.IsStarted == true)
                if (state.Details.IsStarted == true)
                {
                    var correctAnswerEng = state.Details.CurrentWordTraining.WordPartOfSpeech.Word.EnglishWord.Trim();
                    var isWrongAnswerEng = correctAnswerEng != context.Update.Message.Text.ToLower();
                    var message = string.Empty;
                    if (state.Details.TrainingType == TrainingType.Input)
                    {
                        state.Details.CurrentWordTraining.InputEnglish = isWrongAnswerEng ? false : true;
                        message = isWrongAnswerEng ? $"Неправильно! Правильный ответ: <b>{correctAnswerEng}</b>" : "Правильно!";
                    }
                    await context.ReplyAsyncWithHtml(message);
                }
            await next(context);
        }
    }
}
