using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EnglishTelegramBot.DomainCore.Framework
{
    public interface IDispatcher
    {
        Task<TResult> Dispatch<TResult>(IQuery query);
        Task<TResult> Dispatch<TResult>(ICommand command);
        Task Dispatch(ICommand command);
    }
}
