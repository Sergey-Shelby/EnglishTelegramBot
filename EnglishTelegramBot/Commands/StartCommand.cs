﻿using EnglishTelegramBot.Constants;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishTelegramBot.Commands
{
    public class StartCommand : BaseCommand
    {
        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Good evening", CreateMainMenuKeyboard());
        }

        private ReplyKeyboardMarkup CreateMainMenuKeyboard()
        {
            var rkm = new ReplyKeyboardMarkup();
            rkm.Keyboard =
                new KeyboardButton[][]
                {
                    new KeyboardButton[]
                    {
                        Message.LEARN_WORD
                    },
                    new KeyboardButton[]
                    {
                        "Line 2-1", "Line 2-2"
                    }
                };
            return rkm;
        }
    }
}
