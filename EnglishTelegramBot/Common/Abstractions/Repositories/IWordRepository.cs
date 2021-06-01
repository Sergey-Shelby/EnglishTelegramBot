using System;
using System.Collections.Generic;

namespace EnglishTelegramBot.Common.Abstractions.Repositories
{
    interface IWordRepository<T> : IBaseRepository<T> where T : class
    {

    }
}
