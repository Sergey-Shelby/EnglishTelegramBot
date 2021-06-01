using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Common.Abstractions.Repositories
{
    interface IThemeRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetThemeList(); 
        T GetTheme(int id); 
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save(); 
	}
}
