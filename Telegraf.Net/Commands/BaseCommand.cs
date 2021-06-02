using System.Threading.Tasks;
using Telegraf.Net.Abstractions;
using Telegram.Bot.Types;

namespace Telegraf.Net.Commands
{
    public abstract class BaseCommand : IAnswerCommand
    {
        public User User { get; set; }
        public abstract Task Execute(TelegrafContext context, Message message);
    }
}
