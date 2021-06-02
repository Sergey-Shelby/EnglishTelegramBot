using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegraf.Net.Abstractions
{
    public interface ITelegrafContext
    {
        ITelegramBotClient Bot { get; }
        User User { get; }
        Update Update { get; }
        IServiceProvider Services { get; }
        Task<Message> Reply(string text);
    }
}
