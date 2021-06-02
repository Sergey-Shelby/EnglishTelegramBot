using System;
using Telegraf.Net.Abstractions;

namespace Telegraf.Net.Helpers
{
    public static class When
    {
        public static bool NewTextMessage(ITelegrafContext context) =>
            context.Update.Message?.Text != null;

        public static Predicate<ITelegrafContext> NewTextMessage(string text) =>
            (ITelegrafContext context) => context.Update.Message?.Text == text;

    }
}
