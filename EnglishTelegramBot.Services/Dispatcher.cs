using EnglishTelegramBot.DomainCore.Framework;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace EnglishTelegramBot.Services
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _container;

        public Dispatcher(IServiceProvider container)
        {
            _container = container;
        }

        [DebuggerStepThrough]
        public Task<TResult> Dispatch<TResult>(IQuery query)
        {
            if (query == null) throw new ArgumentException(nameof(query));
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            return Handle(handlerType, query);
        }

        [DebuggerStepThrough]
        public Task<TResult> Dispatch<TResult>(ICommand command)
        {
            if (command == null) throw new ArgumentException(nameof(command));
            var handlerType = typeof(ICommandResultHandler<,>).MakeGenericType(command.GetType(), typeof(TResult));
            return Handle(handlerType, command);
        }

        [DebuggerStepThrough]
        public Task Dispatch(ICommand command)
        {
            if (command == null) throw new ArgumentException(nameof(command));
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            return Handle(handlerType, command);
        }

        [DebuggerStepThrough]
        private dynamic Handle(Type handlerType, dynamic o)
        {
            dynamic handler = _container.GetService(handlerType);
            return handler.Handle(o);
        }
    }
}
