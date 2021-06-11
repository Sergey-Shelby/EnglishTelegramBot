using EnglishTelegramBot.DomainCore.Abstractions;
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
        public FinishTrainingCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var user = await _unitOfWork.UserRepository.FetchByTelegramId(context.User.Id);
            var wordTrainingSet = await _unitOfWork.WordTrainingSetRepository.FetchLastByUserId(user.Id);
            var wordTraining = await _unitOfWork.WordTrainingRepository.FetchBySetAsync(wordTrainingSet.Id);

            double wordTrainingTrueAnswerCount = wordTraining.Where(x => x.RussianSelect == true).Count();
            var procent = Math.Truncate(wordTrainingTrueAnswerCount / wordTraining.Count() * 100);

            var messageList = new StringBuilder();

            messageList.Append($"Тренировка завершена! Результат — {procent}%\n");

            if (procent < 100)
            {
                messageList.Append($"Повторите следующие слова:\n");
                //TODO: WordPartOfSpeech.Word.EnglishWord - invalid, need see WordPartOfSpeechData
                wordTraining.Where(y => y.RussianSelect == false).ToList().ForEach(x => messageList.AppendLine($"{x.WordPartOfSpeech.Word.EnglishWord} — {x.WordPartOfSpeech.Word.RussianWord}"));
            }

            await context.ReplyAsync($"{messageList}");
            await context.UnpinMessageAsync();
            _statusProvider.ClearStatus(context.User.Id);
            await next(context);
        }
    }
}
