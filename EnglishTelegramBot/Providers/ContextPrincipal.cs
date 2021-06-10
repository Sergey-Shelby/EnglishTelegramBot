using EnglishTelegramBot.DomainCore.Abstractions;

namespace EnglishTelegramBot.Providers
{
    public class ContextPrincipal : IContextPrincipal
    {
        public int TelegramUserId { get; set; }
    }
}
