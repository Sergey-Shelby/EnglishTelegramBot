using Telegraf.Net.Abstractions;

namespace Telegraf.Net
{
    public class BotConfiguration : IBotConfiguration
    {
        public string BotToken { get; set; }
        public string HostAddress { get; set; }
    }
}
