using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.DomainCore.Entities;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Providers
{
    public class UserManager : IUserManager
    {
        private IUnitOfWork _unitOfWork;
        private IContextPrincipal _contextPrincipal;
        public UserManager(IUnitOfWork unitOfWork, IContextPrincipal contextPrincipal)
        {
            _unitOfWork = unitOfWork;
            _contextPrincipal = contextPrincipal;
        }

        public Task<User> FetchCurrentUserAsync()
        {
            return _unitOfWork.UserRepository.FetchByTelegramId(_contextPrincipal.TelegramUserId);
        }
    }
}
