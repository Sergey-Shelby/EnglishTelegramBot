﻿using System;
using Telegraf.Net;
using Telegraf.Net.Abstractions;
using Telegraf.Net.Helpers;

namespace EnglishTelegramBot.Extensions
{
    public class When : BaseWhen
    {
        //public static IBotBuilder UseWhenStatus<TCommand>(this IBotBuilder botBuilder, int status) where TCommand : IAnswerCommand =>
        //   botBuilder.UseWhen<TCommand>((ITelegrafContext context) => {
        //       var statusProvider = (StatusProvider)context.Services.GetService(typeof(StatusProvider));
        //       var statusCode = statusProvider.GetStatus(context.Update.Message.From.Id);
        //       return statusCode == status;
        //   });

        public static Predicate<ITelegrafContext> HasStatus(int status) =>
            (ITelegrafContext context) => {
                var statusProvider = (StatusProvider)context.Services.GetService(typeof(StatusProvider));
                var statusCode = statusProvider.GetStatus(context.Update.Message.From.Id);
                return statusCode == status;
                };
    }
}
