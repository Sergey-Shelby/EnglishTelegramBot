using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Enums;
using System;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishTelegramBot.Commands.TrainingWord
{
	public class TrainingInputTypeCommand : BaseCommand
    {
        private readonly IStatusProvider _statusProvider;
        private readonly IUserManager _manager;
        public TrainingInputTypeCommand(IStatusProvider statusProvider, IUserManager manager)
        {
            _statusProvider = statusProvider;
            _manager = manager;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var user = await _manager.FetchCurrentUserAsync();
            var state = _statusProvider.GetStatus<WordTrainingState>(user.TelegramId).Details;
            state.IsStarted = true;

            var currentWordTraining = state.CurrentWordTraining;
            if (currentWordTraining == null)
            {
                await next(context);
                return;
            }
            await context.ReplyAsyncWithHtml($"Текущее слово: <i>{currentWordTraining.WordPartOfSpeech.Word.RussianWord}</i> ({currentWordTraining.WordPartOfSpeech.PartOfSpeech.Name.ToLower()})", replyMarkup: new ReplyKeyboardRemove());  
        }
    }
}
