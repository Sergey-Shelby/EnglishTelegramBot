using System;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Telegraf.Net
{
    public class TelegrafContext : ITelegrafContext
    {
        public ITelegramBotClient Bot { get; set; }
        public User User { get; set; }
        public IServiceProvider Services { get; set; }
        public Update Update { get; set; }
        public Task<Message> ReplyAsync(string text, IReplyMarkup replyMarkup = null) => 
            Bot.SendTextMessageAsync(User.Id, text, replyMarkup: replyMarkup);

        public Task PinMessageAsync(Message message) =>
            Bot.PinChatMessageAsync(message.Chat.Id, message.MessageId);

        public Task UnpinMessageAsync() =>
            Bot.UnpinChatMessageAsync(Update.Message.Chat.Id);

        public TelegrafContext(ITelegramBotClient bot, Update u, IServiceProvider services)
        {
            Bot = bot;
            Update = u;
            User = u?.Message?.From;
            Services = services;
        }
    }
}
