using Telegram.Bot;

namespace Telegraf.Net
{
    public class TelegrafClient
    {
        public ITelegramBotClient Client { get; }

        public TelegrafClient(string token)
        {
            Client = new TelegramBotClient(token);
        }
    }
}
