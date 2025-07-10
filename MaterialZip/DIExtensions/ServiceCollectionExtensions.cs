using MaterialZip.Services.ExplorerServices;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MaterialZip.DIExtensions;

/// <summary>
/// <see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/> extensions methods for normal-view of <see cref="Bootstrapping.Bootstrapper"/>
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add a explorer services to the <see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/>
    /// </summary>
    /// <param name="services"><see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/> instance</param>
    /// <returns><see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/> instance with explorer services</returns>
    /// <remarks>
    /// Services that will be add by interface: <see cref="ExplorerHistory"/>, <see cref="ExplorerHistoryMemory"/>, <see cref="Explorer"/>, <see cref="LowLevelExplorer"/>,
    /// please dont use <see cref="LowLevelExplorer"/> and <see cref="ExplorerHistoryMemory"/> they need to be only in <see cref="Explorer"/> and <see cref="ExplorerHistory"/> 
    /// </remarks>
    public static IServiceCollection AddExplorer(this IServiceCollection services)
    {
        services
            .AddSingleton<IExplorer, Explorer>()
            .AddSingleton<ILowLevelExplorer, LowLevelExplorer>()
            .AddTransient<IExplorerHistoryMemory, ExplorerHistoryMemory>()
            .AddTransient<IExplorerHistory, ExplorerHistory>();
        return services;
    }

    public static IServiceCollection ConfigureByPattern<T>(this IServiceCollection services, IConfigurationManager configuration)
    {
        return services;
    }
}