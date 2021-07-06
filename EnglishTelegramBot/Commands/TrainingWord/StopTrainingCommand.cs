using EnglishTelegramBot.DomainCore.Enums;
using EnglishTelegramBot.DomainCore.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands.TrainingWord
{
	public class StopTrainingCommand : BaseCommand
	{
		private readonly IStatusProvider _statusProvider;
		private readonly IDispatcher _dispatcher;
		public StopTrainingCommand(IStatusProvider statusProvider, IDispatcher dispatcher)
		{
			_statusProvider = statusProvider;
			_dispatcher = dispatcher;
		}
		public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
		{
			var state = _statusProvider.GetStatus<WordTrainingState>(context.User.Id);
			var message = state.Details.TrainingSetType == TrainingSetType.Training ? $"Тренировка завершена!" : "Тест завершен!";
			await context.ReplyAsyncWithHtml($"{message}");
			await context.UnpinMessageAsync();
			_statusProvider.ClearStatus(context.User.Id);
			await next(context);
		}
	}
}
