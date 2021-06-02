using Telegraf.Net.Abstractions;

namespace Telegraf.Net
{
    public class BotOptions: IBotOptions
    {
        public string ApiToken { get; set; }
    }
}
