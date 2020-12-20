using System;
using System.Threading.Tasks;
using ArmaForces.ArmaServerManager.Discord.Bot.Extensions;
using ArmaForces.ArmaServerManager.Discord.Bot.Services;
using ArmaForces.ArmaServerManager.Discord.Extensions;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ArmaForces.ArmaServerManager.Discord.Bot
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((context, serviceCollection) => PrepareServiceCollection(serviceCollection));

        private static IServiceCollection PrepareServiceCollection(IServiceCollection serviceCollection)
            => serviceCollection
                .AddSingleton(CreateDiscordSocketClient)
                .AddServerManager()
                .AddLogging()
                .AddHostedService<StartupService>()
                .AddHostedService<CommandsService>();

        private static DiscordSocketClient CreateDiscordSocketClient(IServiceProvider provider)
        {
            var client = new DiscordSocketClient();
            var logger = provider.GetService<ILogger<DiscordSocketClient>>();
            client.Log += message => message.LogWithLoggerAsync(logger);
            return client;
        }
    }
}
