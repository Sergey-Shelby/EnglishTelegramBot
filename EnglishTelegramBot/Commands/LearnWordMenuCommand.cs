using EnglishTelegramBot.Constants;
using System.Threading.Tasks;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace EnglishTelegramBot.Commands
{
    public class LearnWordMenuCommand : BaseCommand
    {
        public override async Task ExecuteAsync(TelegrafContext context, UpdateDelegate next)
        {
            await context.ReplyAsync("Пожалуйста, выберите тип изучения", CreateLearnMenuKeyboard());
        }

        private static ReplyKeyboardMarkup CreateLearnMenuKeyboard()
        {
            var rkm = new ReplyKeyboardMarkup();
            rkm.Keyboard =
                new KeyboardButton[][]
                {
                    new KeyboardButton[]
                    {
                        Message.LEARN_NEW_WORDS
                    },
                    new KeyboardButton[]
                    {
                        Message.REPEAT_LEARN
                    },
                    new KeyboardButton[]
                    {
                        Message.MAIN_MENU
                    }
                };
            return rkm;
        }
    }

}
