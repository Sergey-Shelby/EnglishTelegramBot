using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Common.Abstractions.Repositories
{
	interface IPartOfSpeechRepository<T> : IDisposable where T : class
    {
        IEnumerable<T> GetPartOfSpeechList();
        T GetPartOfSpeech(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save(); 
	}
}
