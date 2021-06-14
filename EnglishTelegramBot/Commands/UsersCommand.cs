using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using EnglishTelegramBot.DomainCore.Abstractions;
using System.Linq;
using System.Text;

namespace EnglishTelegramBot.Commands
{
    public class UsersCommand : BaseCommand
    {
        private readonly IUnitOfWork _unitOfWork;

		public UsersCommand(IUnitOfWork unitOfWork)
		{
            _unitOfWork = unitOfWork;
        }

        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var users = await _unitOfWork.UserRepository.FetchAllAsync();
            var usersList = new StringBuilder();
            users.ToList().ForEach(x => usersList.AppendLine($"{x.Id} | {x.FirstName} {x.LastName} | {x.UserName}"));

            await context.ReplyAsync(usersList.ToString());
        }
    }
}
