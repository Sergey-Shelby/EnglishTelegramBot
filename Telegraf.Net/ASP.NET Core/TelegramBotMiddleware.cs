using EnglishTelegramBot.DomainCore.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;
using Telegram.Bot;
using Telegram.Bot.Types;
using JsonException = Newtonsoft.Json.JsonException;

namespace Telegraf.Net.ASP.NET_Core
{
    internal class TelegramBotMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly UpdateDelegate _updateDelegate;

        /// <summary>
        /// Initializes an instance of middleware
        /// </summary>
        /// <param name="next">Instance of request delegate</param>
        /// <param name="logger">Logger for this middleware</param>
        public TelegramBotMiddleware(
            RequestDelegate next,
            UpdateDelegate updateDelegate
        )
        {
            _next = next;
            _updateDelegate = updateDelegate;
        }

        /// <summary>
        /// Gets invoked to handle the incoming request
        /// </summary>
        /// <param name="context"></param>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method != HttpMethods.Post)
            {
                await _next.Invoke(context).ConfigureAwait(false);
                return;
            }

            string payload;
            using (var reader = new StreamReader(context.Request.Body))
            {
                payload = await reader.ReadToEndAsync().ConfigureAwait(false);
            }

            Update update = null;
            try
            {
                update = JsonConvert.DeserializeObject<Update>(payload);
            }
            catch (JsonException e)
            {
            }

            if (update == null)
            {
                context.Response.StatusCode = 404;
                return;
            }

            using (var scope = context.RequestServices.CreateScope())
            {
                try
                {
                    var bot = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();
                    var contextPrincipal = (IContextPrincipal)scope.ServiceProvider.GetService(typeof(IContextPrincipal));
                    contextPrincipal.TelegramUserId = update?.Message?.From?.Id ?? default;
                    var updateContext = new TelegrafContext(bot, update, scope.ServiceProvider);

                    await _updateDelegate(updateContext).ConfigureAwait(false);
                }
                catch (Exception e)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await ExceptionLogger.PrintAsync($"⌛️ {DateTime.Now}");
                    await ExceptionLogger.PrintAsync($"📍 Base exception:\n*{e.Message}*\n{e.StackTrace}");
                    if (e.InnerException != null)
                    {
                        await ExceptionLogger.PrintAsync($"📍 Inner exception:\n*{e.InnerException.Message}*\n{e.InnerException.StackTrace}");
                    }

                    await ExceptionLogger.PrintSticker();
                }
            }

            if (!context.Response.HasStarted)
            {
                context.Response.StatusCode = 201;
            }
        }
    }
}
