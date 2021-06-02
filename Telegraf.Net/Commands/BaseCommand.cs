using System.Threading.Tasks;
using Telegraf.Net.Abstractions;

namespace Telegraf.Net.Commands
{
    public abstract class BaseCommand : IAnswerCommand
    {
        public abstract Task ExecuteAsync(TelegrafContext context, UpdateDelegate next);
    }
}
