using System.Windows;
using System.Windows.Data;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.ExplorerServices;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Serilog;


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
    /// Services that will be added by interface: <see cref="ExplorerHistory"/>, <see cref="ExplorerHistoryMemory"/>, <see cref="Explorer"/>, <see cref="LowLevelExplorer"/>,
    /// please don't use <see cref="LowLevelExplorer"/> and <see cref="ExplorerHistoryMemory"/> they need to be only in <see cref="Explorer"/> and <see cref="ExplorerHistory"/> 
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

    /// <summary>
    /// Add a theme loader services to the  <see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/>
    /// </summary>
    /// <param name="services"><see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/> instance</param>
    /// <returns><see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/> instance with theme loader services</returns>
    /// <remarks>
    /// Services that will be added by interface: <see cref="IThemeLoader"/>, <see cref="IColorConvertor"/> and <see cref="IThemeConvertor"/> and also trying to add <see cref="IApplicationConfigurationManager"/>
    /// Please do not use  <see cref="IColorConvertor"/> and <see cref="IThemeConvertor"/>  in your application, they only need to  
    /// </remarks>
    public static IServiceCollection AddThemeLoader(this IServiceCollection services)
    {
        services
            .AddScoped<IThemeLoader, ThemeLoader>()
            .AddTransient<IColorConvertor, ColorConvertor>()
            .AddTransient<IThemeConvertor, ThemeConvertor>()
            .TryAddScoped<IApplicationConfigurationManager, ApplicationConfigurationManager>();
        return services;
    }

    /// <summary>
    /// Add a last directory manager services to the  <see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/>
    /// </summary>
    /// <param name="services"><see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/> instance</param>
    /// <returns><see cref="Microsoft.Extensions.DependencyInjection.ServiceCollection"/> instance with theme loader services</returns>
    /// <remarks>
    /// Services that will be added by interface: <see cref="ILastDirectoryGetter"/>, <see cref="ILastDirectoryBuffer"/> and <see cref="ILastDirectoryChanger"/> and also trying to add <see cref="IApplicationConfigurationManager"/>
    /// </remarks>
    public static IServiceCollection AddLastDirectoryManagers(this IServiceCollection services)
    {
        services
            .AddScoped<ILastDirectoryChanger, LastDirectoryChanger>()
            .AddScoped<ILastDirectoryGetter, LastDirectoryGetter>()
            .AddSingleton<ILastDirectoryBuffer, LastDirectoryBuffer>()
            .TryAddScoped<IApplicationConfigurationManager, ApplicationConfigurationManager>();
        return services;
    }
    
}