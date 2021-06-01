using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Common.Abstractions.Repositories
{
    interface IWordRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetWordList(); 
        T GetWord(int id);
        void Create(T item); 
        void Update(T item);
        void Delete(int id); 
        void Save(); 
    }
}
