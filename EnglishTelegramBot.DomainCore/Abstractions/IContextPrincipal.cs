using EnglishTelegramBot.DomainCore.Entities;

namespace EnglishTelegramBot.DomainCore.Abstractions
{
    public interface IContextPrincipal
    {
        public int TelegramUserId { get; set; }
    }


}
