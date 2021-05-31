using EnglishTelegramBot.TelegrafBot;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace EnglishTelegramBot
{
	public class Startup
	{
		private Telegraf _client;

		public void ConfigureServices(IServiceCollection services) {}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello Telgram Bot!");
				});
			});

			_client = new Telegraf("1874538705:AAEBcO4PJL_MPUHOS1tkF9XPDee7WbMC7nc");

			_client.Hears<HelpCommand>("help only full");
			_client.Hears<HelpCommand>("help part", isFullEqual: false);
			_client.Hears<StartCommand>("start");

			_client.StartReceining((TelegrafContext context, Message message) => 
			{
				context.Reply($"Your message: {message.Text}");
				return Task.CompletedTask; 
			});
		}

		public class HelpCommand : BaseCommand
		{
			public override async Task Execute(TelegrafContext context, Message message)
			{
				await context.Reply("Help text!!!");
			}
		}

		public class StartCommand : BaseCommand
		{
			public override async Task Execute(TelegrafContext context, Message message)
			{
				await context.Reply("Start text!!! for: " + User.FirstName + " " + User.LastName);
			}
		}
	}
}
