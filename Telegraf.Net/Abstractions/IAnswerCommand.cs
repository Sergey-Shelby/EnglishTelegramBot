using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Telegraf.Net.Abstractions
{
    public interface IAnswerCommand
    {
        Task Execute(TelegrafContext context, Message message);
        User User { get; set; }
    }
}
