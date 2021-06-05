using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Telegraf.Net.Abstractions;

namespace EnglishTelegramBot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection serviceCollection)
        {
            var result = typeof(ServiceCollectionExtensions).Assembly.GetTypes().Where(c => typeof(IAnswerCommand).IsAssignableFrom(c) && !c.IsInterface);
            result.ToList().ForEach(x => serviceCollection.AddScoped(x));
            return serviceCollection;
        }
    }
}
