using EnglishTelegramBot.DomainCore.Abstractions;
using System;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishTelegramBot.Commands.TrainingWord
{
    public class TrainingSelectTypeCommand : BaseCommand
    {
        private IStatusProvider _statusProvider;
        private IUnitOfWork _unitOfWork;
        private IUserManager _manager;
        public TrainingSelectTypeCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IUserManager manager)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
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

            var selectWords = await _unitOfWork.WordPartOfSpeechRepository.FetchFullAsync(3);
            selectWords.Insert(new Random().Next(0, 3), currentWordTraining.WordPartOfSpeech);

            var rkm = new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new KeyboardButton[] { selectWords[0].WordPartOfSpeechDatas[0].Word, selectWords[1].WordPartOfSpeechDatas[0].Word },
                new KeyboardButton[] { selectWords[2].WordPartOfSpeechDatas[0].Word, selectWords[3].WordPartOfSpeechDatas[0].Word }
            });
            await context.ReplyAsync($"Текущее слово: {currentWordTraining.WordPartOfSpeech.Word.EnglishWord} ({currentWordTraining.WordPartOfSpeech.PartOfSpeech.Name.ToLower()})", rkm);
        }
    }
}
