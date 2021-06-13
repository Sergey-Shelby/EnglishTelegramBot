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

            var isTrainRus = state.TrainingType == TrainingType.SelectRus;
            var cuurentWord = isTrainRus ? currentWordTraining.WordPartOfSpeech.Word.EnglishWord : currentWordTraining.WordPartOfSpeech.Word.RussianWord;
            var rkm = new ReplyKeyboardMarkup(new KeyboardButton[][]
            {
                new KeyboardButton[] { isTrainRus ? selectWords[0].WordPartOfSpeechDatas[0].Word : selectWords[0].Word.EnglishWord, isTrainRus ? selectWords[1].WordPartOfSpeechDatas[0].Word : selectWords[1].Word.EnglishWord },
                new KeyboardButton[] { isTrainRus ? selectWords[2].WordPartOfSpeechDatas[0].Word : selectWords[2].Word.EnglishWord, isTrainRus ? selectWords[3].WordPartOfSpeechDatas[0].Word : selectWords[3].Word.EnglishWord }
            });
            await context.ReplyAsyncWithHtml($"Текущее слово: <i>{cuurentWord}</i> ({currentWordTraining.WordPartOfSpeech.PartOfSpeech.Name.ToLower()})", rkm);
        }
    }
}
