using System;
using Telegraf.Net.Abstractions;

namespace Telegraf.Net.Helpers
{
    public class BaseWhen
    {
        public static bool NewTextMessage(ITelegrafContext context) =>
            context.Update.Message?.Text != null;

        public static Predicate<ITelegrafContext> TextMessageEquals(string text) =>
            (ITelegrafContext context) => context.Update.Message?.Text == text;

		public static Predicate<ITelegrafContext> TextMessageContains(string text)
		{
			return (ITelegrafContext context) => context.Update?.Message != null && context.Update.Message?.Text != null && context.Update.Message.Text.Contains(text);
		}
	}
}
