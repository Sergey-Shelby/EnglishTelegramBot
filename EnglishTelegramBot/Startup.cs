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
using EnglishTelegramBot.DomainCore.Enums;
using System.Threading.Tasks;
using System;
using System.Net;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegraf.Net.ASP.NET_Core;
using Microsoft.Extensions.Hosting;

namespace EnglishTelegramBot
{
	public class Startup
	{
		private readonly BotConfiguration _botConfig;
		private readonly IConfiguration _configuration;
		public static Assembly DomainAssembly => typeof(Dispatcher).Assembly;

		public Startup(IConfiguration configuration)
		{
			_configuration = configuration;
			_botConfig = configuration.GetSection("BotConfiguration").Get<BotConfiguration>();
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddHostedService<ConfigureWebhook>();

			services.AddHttpClient("tgwebhook")
					.AddTypedClient<ITelegramBotClient>(httpClient => new TelegramBotClient(_botConfig.BotToken, httpClient));

			services.AddScopedHandlers(DomainAssembly);
			services.AddScoped<IDispatcher, Dispatcher>();
			services.AddScoped<IUserManager, UserManager>();
			services.AddScoped<IContextPrincipal, ContextPrincipal>();
			services.Configure<BotConfiguration>(_configuration.GetSection("BotConfiguration"));
			services.AddSingleton<IStatusProvider>(x => new StatusProvider());
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			services.AddDbContext<EnglishContext>(options => options.UseSqlServer(_configuration.GetConnectionString("MainDb")));
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

			if (env.IsDevelopment())
			{
				app.UseTelegramBotLongPolling(ConfigureBot());
			}
            else
			{
				app.UseTelegramBotWebhook(ConfigureBot());
				new SiteWaiter().Run();
			}
		}

		public IBotBuilder ConfigureBot() =>
			new BotBuilder()
				.UseWhen<LearnWordMenuCommand>(When.TextMessageEquals(Message.LEARN_WORD))
				.UseWhen<LearnWordCommand>(When.TextMessageEquals(Message.LEARN_NEW_WORDS))
				.UseWhen<LearnWordRepeatCommand>(When.TextMessageContains(Message.REPEAT_LEARN))
				.UseWhen<WordTestMenuCommand>(When.TextMessageEquals(Message.TEST_WORD))
				.UseWhen<FullTestMainCommand>(When.TextMessageEquals(Message.MAIN_TEST_WORD))
				.UseWhen<DictionaryTestCommand>(When.TextMessageEquals(Message.LEARN_TEST_WORD))
				.UseWhen<HelpCommand>(When.TextMessageEquals("help"))
				.UseWhen<UsersCommand>(When.TextMessageEquals(Message.USERS))
				.UseWhen<StatisticsCommand>(When.TextMessageEquals(Message.STATISTICS))

				.UseWhen<MainMenuCommand>(When.TextMessageEquals(Message.MAIN_MENU))

				.MapWhen(When.TextMessageContains("start"), x => x
					.Use<StartCommand>()
					.Use<MainMenuCommand>())

				.MapWhen(When.HasStatus(Status.LEARN_WORD), x => x
					.MapWhen(When.TextMessageEquals("!stop"), x => x//.Or(When.CheckCountWordTraining()), x => x
						.Use<StopTrainingCommand>()
						.Use<MainMenuCommand>())
					.UseWhen<CheckInputTypeCommand>(When.HasTrainingType(x => x == TrainingType.Input))
					.UseWhen<CheckSelectTypeCommand>(When.HasTrainingType(x => x == TrainingType.SelectRus || x == TrainingType.SelectEng))
					.UseWhen<TrainingSelectTypeCommand>(When.HasTrainingType(x => x == TrainingType.SelectRus || x == TrainingType.SelectEng))
					.UseWhen<TrainingInputTypeCommand>(When.HasTrainingType(x => x == TrainingType.Input))
					.Use<FinishTrainingCommand>()
						.Use<MainMenuCommand>());
	}

	public class SiteWaiter
	{
		public void Run()
		{
			Task.Run(async () =>
			{
				while (true)
				{
					try
					{
						await Task.Delay(TimeSpan.FromMinutes(1));
						string html = string.Empty;
						string url = @"https://tg-bots.site";

						HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
						request.AutomaticDecompression = DecompressionMethods.GZip;

						using (HttpWebResponse response = (HttpWebResponse)request.GetResponse());
					}
                    catch {}
				}
			});
		}
	}
}

