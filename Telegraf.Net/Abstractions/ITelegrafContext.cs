using System;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegraf.Net.Abstractions
{
    public interface ITelegrafContext
    {
        ITelegramBotClient Bot { get; }
        User User { get; }
        Update Update { get; }
        IServiceProvider Services { get; }
        Task<Message> ReplyAsync(string text, IReplyMarkup replyMarkup = null);
        Task<Message> ReplyAsyncWithHtml(string text, IReplyMarkup replyMarkup = null);
    }
}
