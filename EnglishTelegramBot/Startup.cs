using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegraf.Net.Extensions;
using Telegraf.Net.Helpers;

namespace EnglishTelegramBot
{
	public class Startup
	{ 
		private IConfiguration _configuration { get; }

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<BotOptions>(_configuration.GetSection("BotOptions"));
			services.AddScoped<HelpCommand>();
			services.AddScoped<StartCommand>();
		}

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

			app.UseTelegramBotLongPolling(ConfigureBot());
		}

		public IBotBuilder ConfigureBot() =>
			new BotBuilder()
				.UseWhen<HelpCommand>(When.NewTextMessage("help"))
				.UseWhen<StartCommand>(When.NewTextMessage("start"));

		public class HelpCommand : BaseCommand
		{
			public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
			{
				await context.Reply("Help text!!!");
			}
		}

		public class StartCommand : BaseCommand
		{
			public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
			{
				await context.Reply("Start text!!! for: " + context.User.FirstName + " " + context.User.LastName);
			}
		}
	}
}
