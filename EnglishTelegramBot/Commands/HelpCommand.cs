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
		private IUnitOfWork _unitOfWork;
		public HelpCommand(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
		{
			var word = _unitOfWork.WordRepository.FetchAll().OrderBy(x => Guid.NewGuid()).FirstOrDefault();
			await context.ReplyAsync(word.English);
		}
	}
}
