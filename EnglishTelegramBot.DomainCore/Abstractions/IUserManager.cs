using EnglishTelegramBot.DomainCore.Entities;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions
{
    public interface IUserManager
    {
        Task<User> FetchCurrentUserAsync();
    }


}
