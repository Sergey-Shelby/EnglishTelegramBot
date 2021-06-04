using EnglishTelegramBot.Commands;
using EnglishTelegramBot.Constants;
using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Extensions;
using Telegraf.Net.Helpers;
using EnglishTelegramBot.Extensions;

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
			services.AddSingleton<StatusProvider>(x => new StatusProvider());
			services.AddScoped<IUnitOfWork>(x => new UnitOfWork(new EnglishContext()));
			services.AddScoped<HelpCommand>();
			services.AddScoped<StartCommand>();
			services.AddScoped<LearnWordCommand>();
			services.AddScoped<CheckWordCommand>();
			services.AddScoped<UsersCommand>();
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
				.UseWhen<HelpCommand>(When.TextMessageEquals("help"))
				.UseWhen<StartCommand>(When.TextMessageContains("start"))
				.UseWhen<LearnWordCommand>(When.TextMessageEquals(Message.LEARN_WORD))
				.UseWhen<UsersCommand>(When.TextMessageEquals(Message.USERS))
				.UseWhenStatus<CheckWordCommand>(Status.LEARN_WORD);
	}
}
