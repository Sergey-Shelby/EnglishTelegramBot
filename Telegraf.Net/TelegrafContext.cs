using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegraf.Net
{
    public class TelegrafContext
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly User _user;
        public TelegrafContext(ITelegramBotClient telegramBot, User user)
        {
            _telegramBotClient = telegramBot;
            _user = user;
        }

        public Task<Message> Reply(string text) => _telegramBotClient.SendTextMessageAsync(_user.Id, text);
    }
}
