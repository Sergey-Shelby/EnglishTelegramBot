using EnglishTelegramBot.Constants;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.DomainCore.Models.WordPartOfSpeeches;
using EnglishTelegramBot.DomainCore.Models.WordTrainings;
using System.Collections.Generic;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands
{
    public class LearnWordCommand : BaseCommand
    {
        IStatusProvider _statusProvider;
        IUnitOfWork _unitOfWork;
        IDispatcher _dispatcher;
        public LearnWordCommand(IStatusProvider statusProvider, IUnitOfWork unitOfWork, IDispatcher dispatcher)
        {
            _statusProvider = statusProvider;
            _unitOfWork = unitOfWork;
            _dispatcher = dispatcher;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Тренеровка слов запущена 🖋\nОтправьте !stop для завершения 🏁");

            var wordPartOfSpeeches = await _dispatcher.Dispatch<List<WordPartOfSpeech>>(new FetchWordPartOfSpeechForTrainingQuery());

            //await context.PinMessageAsync(message);
            //_statusProvider.SetStatus(context.User.Id, Status.LEARN_WORD);

           

            //var createWordTrainingSetCommand = new CreateWordTrainingSetCommand { WordsPartOfSpeech = wordsPartOfSpeech, TrainingType = TrainingTypeSet.Training };
            //await _dispatcher.Dispatch<int>(createWordTrainingSetCommand);

            //await next(context);
        }
    }

}
