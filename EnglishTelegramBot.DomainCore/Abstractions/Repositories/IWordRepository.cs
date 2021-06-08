using EnglishTelegramBot.DomainCore.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
    public interface IWordRepository : IBaseRepository<Word>
    {
        Task<List<Word>> FetchWordsByCount(int count);
    }
}
