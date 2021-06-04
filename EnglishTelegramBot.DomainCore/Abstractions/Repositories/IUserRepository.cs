using EnglishTelegramBot.DomainCore.Entities;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
	public interface IUserRepository : IBaseRepository<User>
	{
		Task<User> FetchByTelegramId(int telegramId); 
	}
}
