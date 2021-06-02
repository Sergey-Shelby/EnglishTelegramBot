using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Commands;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace Telegraf.Net
{
    public class TelegrafClient
    {
        private readonly TelegramBotClient _tgClient;
        private readonly List<HearsCommand> _hearsCommand;

        public TelegrafClient(string token)
        {
            _tgClient = new TelegramBotClient(token);
            _hearsCommand = new List<HearsCommand>();
        }

        public void StartReceining(Func<TelegrafContext, Message, Task> messageHandler = null)
        {
            _tgClient.StartReceiving();

            _tgClient.OnMessage += (object sender, MessageEventArgs e) =>
            {
                var telegrafContext = new TelegrafContext(_tgClient, e.Message.From);
                messageHandler?.Invoke(telegrafContext, e.Message);

                foreach (var hearCommand in _hearsCommand.Where(x => x.IsMatch(e.Message)))
                {
                    var answerCommand = (IAnswerCommand)Activator.CreateInstance(hearCommand.CommandType);
                    answerCommand.User = e.Message.From;
                    answerCommand.Execute(telegrafContext, e.Message);
                }
            };
        }

        /// <summary>
        /// Execute the command after receiving a message containing the specified text.
        /// </summary>
        /// <param name="text">Text for compare</param>
        /// <param name="command">Command for invoke</param>
        /// <param name="isFullEqual">Complete equal is required if true, it is sufficient to contain if false</param>
        public void Hears<ICommand>(string text, bool isFullEqual = true) where ICommand : IAnswerCommand
        {
            var type = typeof(ICommand);
            _hearsCommand.Add(new HearsCommand { Text = text, CommandType = type, IsFullEqual = isFullEqual });
        }
    }
}
