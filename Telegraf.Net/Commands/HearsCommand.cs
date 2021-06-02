using System;
using Telegram.Bot.Types;

namespace Telegraf.Net.Commands
{
    public class HearsCommand
    {
        public string Text { get; set; }
        public Type CommandType { get; set; }
        public bool IsFullEqual { get; set; }

        public bool IsMatch(Message message) => IsFullEqual ? message.Text.Equals(Text) : message.Text.Contains(Text);
    }
}
