using System;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Pipeline;

namespace Telegraf.Net
{
    public static class BotBuilderExtensions
    {
        public static IBotBuilder UseWhen<TCommand>(
              this IBotBuilder builder,
              Predicate<ITelegrafContext> predicate
          )
              where TCommand : IAnswerCommand
        {
            var branchDelegate = new BotBuilder().Use<TCommand>().Build();
            builder.Use(new UseWhenMiddleware(predicate, branchDelegate));
            return builder;
        }
    }
}
