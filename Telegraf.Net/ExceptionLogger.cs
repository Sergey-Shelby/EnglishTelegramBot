using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.Enums;

namespace Telegraf.Net
{
	public static class ExceptionLogger
	{
		private static TelegramBotClient _client;

		static ExceptionLogger()
		{
			_client = new TelegramBotClient("1874538705:AAEBcO4PJL_MPUHOS1tkF9XPDee7WbMC7nc");
		}

		public static async Task PrintAsync(string line)
		{
			await _client.SendTextMessageAsync(-503576715, line, ParseMode.Default);
		}

		public static Task PrintSticker() =>
			_client.SendStickerAsync(-503576715, sticker: "CAACAgIAAxkBAAIRhmC7eMlMzpiZ4S8nhGtgs37ck7yoAAKDAAPBnGAMjEXaG6vxAgYfBA");
	}
}
