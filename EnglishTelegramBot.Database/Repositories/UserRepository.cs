using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions.Repositories;
using EnglishTelegramBot.DomainCore.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Database.Repositories
{
	public class UserRepository : BaseRepository<User>, IUserRepository
	{
		private DbSet<User> _dbset;
		public UserRepository(EnglishContext englishContext) : base(englishContext)
		{
			_dbset = englishContext.Set<User>();
		}

		public async Task<User> FetchByTelegramId(int telegramId)
		{
			return await _dbset.FirstOrDefaultAsync(x => x.TelegramId == telegramId);
		}
	}
}
