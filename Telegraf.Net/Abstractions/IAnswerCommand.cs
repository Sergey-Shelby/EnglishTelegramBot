using System.Threading.Tasks;

namespace Telegraf.Net.Abstractions
{
    public interface IAnswerCommand
    {
        Task ExecuteAsync(TelegrafContext context, UpdateDelegate next);
    }
}
