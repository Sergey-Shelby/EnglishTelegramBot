using System;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegraf.Net
{
    public class TelegrafContext : ITelegrafContext
    {
        public ITelegramBotClient Bot { get; set; }
        public User User { get; set; }
        public IServiceProvider Services { get; set; }
        public Update Update { get; set; }
        public Task<Message> Reply(string text) => Bot.SendTextMessageAsync(User.Id, text);

        public TelegrafContext(ITelegramBotClient bot, Update u, IServiceProvider services)
        {
            Bot = bot;
            Update = u;
            User = u?.Message?.From;
            Services = services;
        }
    }
}
