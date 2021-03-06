using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : class
	{
        Task<IEnumerable<T>> FetchAllAsync();
        Task<T> FetchByIdAsync(int id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
