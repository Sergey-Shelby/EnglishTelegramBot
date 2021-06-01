using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.DomainCore.Abstractions.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : class
	{
        IEnumerable<T> FetchAll();
        T FetchById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
