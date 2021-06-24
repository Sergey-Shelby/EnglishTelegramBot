using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using Telegraf.Net.Abstractions;

namespace Telegraf.Net.ASP.NET_Core
{
    public static class TelegramBotMiddlewareExtensions
    {   
        /// <summary>
        /// Add Telegram bot webhook handling functionality to the pipeline
        /// </summary>
        /// <typeparam name="TBot">Type of bot</typeparam>
        /// <param name="app">Instance of IApplicationBuilder</param>
        /// <param name="ensureWebhookEnabled">Whether to set the webhook immediately by making a request to Telegram bot API</param>
        /// <returns>Instance of IApplicationBuilder</returns>
        public static IApplicationBuilder UseTelegramBotWebhook(
            this IApplicationBuilder app,
            IBotBuilder botBuilder)
        {
            var updateDelegate = botBuilder.Build();

            var options = app.ApplicationServices.GetRequiredService<IOptions<BotConfiguration>>();
            app.Map(
                @$"/bot/{options.Value.BotToken}",
                builder => builder.UseMiddleware<TelegramBotMiddleware>(updateDelegate)
            );

            return app;
        }
    }
}
