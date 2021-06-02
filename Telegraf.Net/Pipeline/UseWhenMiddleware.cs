using System;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;

namespace Telegraf.Net.Pipeline
{
    public class UseWhenMiddleware: IAnswerCommand
    {
        private readonly Predicate<ITelegrafContext> _predicate;

        private readonly UpdateDelegate _branch;

        public UseWhenMiddleware(Predicate<ITelegrafContext> predicate, UpdateDelegate branch)
        {
            _predicate = predicate;
            _branch = branch;
        }

        public async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            if (_predicate(context))
            {
                await _branch(context).ConfigureAwait(false);
            }

            await next(context).ConfigureAwait(false);
        }
    }
}
