using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Telegraf.Net
{
	public static class tempclass
	{
		private static TelegramBotClient client;
		public static void Print(string line)
		{
			client = new TelegramBotClient("1874538705:AAEBcO4PJL_MPUHOS1tkF9XPDee7WbMC7nc");
			client.SendTextMessageAsync(-503576715, $"Exception: {line}");
		}
	}
}
