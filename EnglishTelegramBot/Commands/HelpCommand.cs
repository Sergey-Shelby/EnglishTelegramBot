using EnglishTelegramBot.DomainCore.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;

namespace EnglishTelegramBot.Commands
{
	public class HelpCommand : BaseCommand
	{
		private readonly IUnitOfWork _unitOfWork;

		public HelpCommand(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
		{
			var words = await _unitOfWork.WordRepository.FetchAllAsync();
			var word = words.OrderBy(x => Guid.NewGuid()).FirstOrDefault();
			await context.ReplyAsync(word.EnglishWord);
		}
	}
}
