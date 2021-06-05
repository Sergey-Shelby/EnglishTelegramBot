using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;

namespace Telegraf.Net
{
    public class BotBuilder : IBotBuilder
    {
        private readonly ICollection<Func<UpdateDelegate, UpdateDelegate>> _components;
        internal UpdateDelegate UpdateDelegate { get; private set; }

        public BotBuilder()
        {
            _components = new List<Func<UpdateDelegate, UpdateDelegate>>();
        }

        public IBotBuilder Use<TCommand>() where TCommand : IAnswerCommand
        {
            _components.Add(next =>
                context =>
                    ((IAnswerCommand)context.Services.GetService(typeof(TCommand))).ExecuteAsync(context, next)
            );
            return this;
        }

        public IBotBuilder Use<TCommand>(TCommand handler) where TCommand : IAnswerCommand
        {
            _components.Add(next => context => handler.ExecuteAsync(context, next));
            return this;
        }

        public UpdateDelegate Build(UpdateDelegate handle = null)
        {
            handle ??= ctx => { return Task.FromResult(0); };

            foreach (var component in _components.Reverse())
            {
                handle = component(handle);
            }

            return UpdateDelegate = handle;
        }
    }
}
