using EnglishTelegramBot.DomainCore.Framework;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
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

        /// <summary>
        /// Add scoped command and query handlers
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        public static void AddScopedHandlers(this IServiceCollection services, Assembly assembly)
        {
            var types = assembly.GetTypes();
            var handlers = from type in types
                           from @interface in type.GetInterfaces()
                           where @interface.IsGenericType &&
                                 (typeof(ICommandHandler<>).IsAssignableFrom(@interface.GetGenericTypeDefinition())
                                  || typeof(ICommandResultHandler<,>).IsAssignableFrom(@interface.GetGenericTypeDefinition())
                                  || typeof(IQueryHandler<,>).IsAssignableFrom(@interface.GetGenericTypeDefinition()))
                           select new { Type = type, Interface = @interface };

            foreach (var cc in handlers)
                services.AddScoped(cc.Interface, cc.Type);
        }
    }
}
