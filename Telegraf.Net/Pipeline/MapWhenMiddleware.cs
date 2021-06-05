using System;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;

namespace Telegraf.Net.Pipeline
{
    internal class MapWhenMiddleware : IAnswerCommand
    {
        private readonly Predicate<TelegrafContext> _predicate;

        private readonly Action<IBotBuilder> _configure;

        public MapWhenMiddleware(Predicate<TelegrafContext> predicate, Action<IBotBuilder> configure)
        {
            _predicate = predicate;
            _configure = configure;
        }

        public Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            var mapBuilder = new BotBuilder();
            _configure(mapBuilder);
            UpdateDelegate mapDelegate = mapBuilder.Build(next);

            return _predicate(context) ? mapDelegate(context) : next(context);
        }
    }
}
