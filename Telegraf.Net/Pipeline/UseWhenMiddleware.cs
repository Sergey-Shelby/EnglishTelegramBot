using System;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;

namespace Telegraf.Net.Pipeline
{
    public class UseWhenMiddleware<TCommand> : IAnswerCommand where TCommand : IAnswerCommand
    {
        private readonly Predicate<ITelegrafContext> _predicate;


        public UseWhenMiddleware(Predicate<ITelegrafContext> predicate)
        {
            _predicate = predicate;
        }

        public async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            if (_predicate(context))
            {
                UpdateDelegate updateDelegate = context =>
                    ((IAnswerCommand)context.Services.GetService(typeof(TCommand))).ExecuteAsync(context, next);
                await updateDelegate(context).ConfigureAwait(false);
            }
            else
            {
                await next(context).ConfigureAwait(false);
            }
        }
    }
}
