using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EnglishTelegramBot.Commands;
using EnglishTelegramBot.Constants;
using EnglishTelegramBot.Database.Common;
using EnglishTelegramBot.DomainCore.Abstractions;
using EnglishTelegramBot.Extensions;
using EnglishTelegramBot.Commands.TrainingWord;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Extensions;
using System.Reflection;
using EnglishTelegramBot.Services;
using EnglishTelegramBot.DomainCore.Framework;
using EnglishTelegramBot.Providers;

namespace EnglishTelegramBot
{
	public class Startup
	{ 
		private IConfiguration _configuration { get; }
		public static Assembly DomainAssembly => typeof(Dispatcher).Assembly;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScopedHandlers(DomainAssembly);
			services.AddScoped<IDispatcher, Dispatcher>();
			services.AddScoped<IUserManager, UserManager>();
			services.AddScoped<IContextPrincipal, ContextPrincipal>();
			services.Configure<BotOptions>(_configuration.GetSection("BotOptions"));
			services.AddSingleton<IStatusProvider>(x => new StatusProvider());
			services.AddScoped<IUnitOfWork>(x => new UnitOfWork(new EnglishContext()));
			services.AddCommands();
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
				.UseWhen<LearnWordMenuCommand>(When.TextMessageEquals(Message.LEARN_WORD))
				.UseWhen<LearnWordCommand>(When.TextMessageEquals(Message.LEARN_NEW_WORDS))
				.UseWhen<WordTestCommand>(When.TextMessageEquals(Message.TEST_WORD))

				.UseWhen<HelpCommand>(When.TextMessageEquals("help"))
				.UseWhen<UsersCommand>(When.TextMessageEquals(Message.USERS))
				.UseWhen<StatisticsCommand>(When.TextMessageEquals(Message.STATISTICS))

				.UseWhen<MainMenuCommand>(When.TextMessageEquals(Message.MAIN_MENU))

				.MapWhen(When.TextMessageContains("start"), x => x
					.Use<StartCommand>()
					.Use<MainMenuCommand>())

				.MapWhen(When.HasStatus(Status.LEARN_WORD), x => x
					.MapWhen(When.TextMessageEquals("!stop"), x => x//.Or(When.CheckCountWordTraining()), x => x
						.Use<FinishTrainingCommand>()
						.Use<MainMenuCommand>())
					.Use<CheckSelectWordCommand>()
					.Use<NextWordSelectTypeCommand>()
					.Use<FinishTrainingCommand>()
						.Use<MainMenuCommand>());
	}
}

