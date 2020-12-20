using ArmaForces.ArmaServerManager.Discord.Configuration;
using ArmaForces.ArmaServerManager.Discord.Features.Mods;
using ArmaForces.ArmaServerManager.Discord.Features.Server;
using ArmaForces.ArmaServerManager.Discord.Features.ServerConfig;
using Microsoft.Extensions.DependencyInjection;

namespace ArmaForces.ArmaServerManager.Discord.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Loads Manager modules into <paramref name="serviceCollection"/>.
        /// Uses default <see cref="IManagerConfiguration"/> implementation.
        /// </summary>
        /// <returns>The same <see cref="IServiceCollection"/> for method chaining.</returns>
        public static IServiceCollection AddServerManager(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IManagerConfiguration, ManagerConfiguration>()
                .AddManagerClients()
                .AddManagerModules();
        }

        /// <summary>
        /// Loads Manager modules into <paramref name="serviceCollection"/>.
        /// Uses <typeparamref name="TConfiguration"/> as singleton implementation for <see cref="IManagerConfiguration"/>.
        /// </summary>
        /// <returns>The same <see cref="IServiceCollection"/> for method chaining.</returns>
        public static IServiceCollection AddServerManager<TConfiguration>(this IServiceCollection serviceCollection) where TConfiguration : class, IManagerConfiguration
        {
            return serviceCollection
                .AddSingleton<IManagerConfiguration, TConfiguration>()
                .AddManagerClients()
                .AddManagerModules();

        }

        /// <summary>
        /// Loads Manager modules into <paramref name="serviceCollection"/>.
        /// Does not include any implementation for <see cref="IManagerConfiguration"/>.
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <returns></returns>
        public static IServiceCollection AddServerManagerWithoutConfiguration(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddManagerClients()
                .AddManagerModules();
        }
        
        private static IServiceCollection AddManagerClients(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<IServerManagerClient, ServerManagerClient>()
                .AddSingleton<IModsManagerClient, ModsManagerClient>()
                .AddSingleton<IConfigurationManagerClient, ConfigurationManagerClient>();
        }

        private static IServiceCollection AddManagerModules(this IServiceCollection serviceCollection)
        {
            return serviceCollection
                .AddSingleton<ModsModule>()
                .AddSingleton<ServerModule>()
                .AddSingleton<ServerConfigModule>();
        }
    }
}
