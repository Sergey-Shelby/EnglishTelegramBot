using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace EnglishTelegramBot
{
	public class Startup
	{
		private TelegramBotClient _client;
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapGet("/", async context =>
				{
					await context.Response.WriteAsync("Hello Telgram Bot!");
				});
			});

			_client = new TelegramBotClient("1874538705:AAEBcO4PJL_MPUHOS1tkF9XPDee7WbMC7nc");
			_client.StartReceiving();
			_client.OnMessage += OnMessageHandler;
		}
		private async void OnMessageHandler(object sender, MessageEventArgs e)
		{
			var msg = e.Message;
			await _client.SendTextMessageAsync(msg.Chat.Id, $"¬аше сообщение Ч {msg.Text}");
		}
	}
}
