namespace Telegraf.Net.Abstractions
{
    public interface IBotConfiguration
    {
        public string BotToken { get; set; }
        public string HostAddress { get; set; }
    }
}
