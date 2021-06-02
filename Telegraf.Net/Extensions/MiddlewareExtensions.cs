﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Threading;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegraf.Net.Extensions
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseTelegramBotLongPolling(
            this IApplicationBuilder app,
            IBotBuilder botBuilder,
            CancellationToken cancellationToken = default)
        {
            var options = app.ApplicationServices.GetRequiredService<IOptions<BotOptions>>();

            var updateDelegate = botBuilder.Build();

            var telegrafBot = new TelegrafClient(options.Value.ApiToken);

            Task.Run(async () =>
            {
                var requestParams = new GetUpdatesRequest
                {
                    Offset = 0,
                    Timeout = 500,
                    AllowedUpdates = new UpdateType[0],
                };

                while (!cancellationToken.IsCancellationRequested)
                {
                    Update[] updates = await telegrafBot.Client.MakeRequestAsync(
                    requestParams,
                    cancellationToken
                    ).ConfigureAwait(false);


                    foreach (var update in updates)
                    {
                        using (var scopeProvider = app.ApplicationServices.CreateScope())
                        {
                            var context = new TelegrafContext(telegrafBot.Client, update, scopeProvider.ServiceProvider);
                            await updateDelegate(context).ConfigureAwait(false);
                        }
                    }

                    if (updates.Length > 0)
                    {
                        requestParams.Offset = updates[updates.Length - 1].Id + 1;
                    }
                }

                cancellationToken.ThrowIfCancellationRequested();
            });
            return app;
        }
    }
}