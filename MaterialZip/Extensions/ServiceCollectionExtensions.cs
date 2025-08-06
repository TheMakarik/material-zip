using System.IO;
using MaterialZip.Factories;
using MaterialZip.Factories.Abstractions;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.ExplorerServices;
using MaterialZip.Services.ExplorerServices.Abstractions;
using MaterialZip.Services.ValidationServices;
using MaterialZip.Services.WindowsFunctions;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using MaterialZip.View;
using MaterialZip.ViewModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MaterialZip.Extensions;

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
    /// Services that will be added by interface: <see cref="IThemeLoader"/> and<see cref="IColorConvertor"/> and also trying to add <see cref="IApplicationConfigurationManager"/>
    /// Please do not use  <see cref="IColorConvertor"/>  in your application, they only need to be used in <see cref="IThemeLoader"/> 
    /// </remarks>
    public static IServiceCollection AddThemeLoader(this IServiceCollection services)
    {
        services
            .AddScoped<IThemeLoader, ThemeLoader>()
            .AddTransient<IColorConvertor, ColorConvertor>()
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

    /// <summary>
    /// Add default icon extractor to the <see cref="services"/>
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance</param>
    /// <returns>>
    /// <see cref="IServiceCollection"/> instance with <see cref="IBitmapSourceBuilder"/>, <see cref="IIconExtractor"/>
    /// and <see cref="IAssociatedIconExtractor"/> services
    /// </returns>
    public static IServiceCollection AddIconExtractor(this IServiceCollection services)
    {
        services.AddSingleton<IBitmapSourceBuilder, BitmapSourceBuilder>()
            .AddSingleton<IIconExtractor, IconExtractor>()
            .AddSingleton<IAssociatedIconExtractor, AssociatedIconExtractor>();
        return services;
    }

    /// <summary>
    /// Adds a url openers services to <see cref="services"/>
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance where services will be added</param>
    /// <returns><see cref="IServiceCollection"/> instance with url openers services</returns>
    /// <remarks>
    /// Services that will be added: <see cref="IUrlOpener"/>, <see cref="IGitHubSourceOpener"/>
    /// Services that will be added if was not added: <see cref="IApplicationConfigurationManager"/>, <see cref="IProcessRunner"/>
    /// </remarks>
    public static IServiceCollection AddUrlOpeners(this IServiceCollection services)
    {
        services
            .AddScoped<IUrlOpener, UrlOpener>()
            .AddScoped<IGitHubSourceOpener, GitHubSourceOpener>()
            .TryAddScoped<IProcessRunner, ProcessRunner>();
        services.TryAddScoped<IApplicationConfigurationManager, ApplicationConfigurationManager>();
        return services;
    }

    /// <summary>
    /// Add all validator to DI container
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> to add validators</param>
    /// <returns><see cref="IServiceCollection"/> instance with validators</returns>
    /// <remarks>
    /// Validators that will be added: <see cref="AbsoluteUrlValidator"/>
    /// </remarks>
    public static IServiceCollection AddValidators(this IServiceCollection services)
    {
        return services.AddSingleton<AbsoluteUrlValidator>();
    }

    /// <summary>
    /// Add exception view services to the <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance</param>
    /// <returns><see cref="IServiceCollection"/> instance with exception view services</returns>
    /// <remarks>
    /// Services that will be added: 
    /// <see cref="IExceptionOccuredViewFactory"/>, 
    /// <see cref="ExceptionOccuredView"/>, 
    /// <see cref="ExceptionOccuredViewModel"/>
    /// </remarks>
    public static IServiceCollection AddExceptionView(this IServiceCollection services)
    {
        return services
            .AddScoped<IExceptionOccuredViewFactory, ExceptionOccuredViewFactory>()
            .AddScoped<ExceptionOccuredView>()
            .AddScoped<ExceptionOccuredViewModel>();
    }

    /// <summary>
    /// Add Windows Explorer related services to the <see cref="IServiceCollection"/>
    /// </summary>
    /// <param name="services"><see cref="IServiceCollection"/> instance</param>
    /// <returns><see cref="IServiceCollection"/> instance with Windows Explorer services</returns>
    /// <remarks>
    /// Services that will be added: 
    /// <see cref="IWindowsExplorerOpener"/>, 
    /// <see cref="ILogPathOpener"/>.
    /// Also tries to add <see cref="IApplicationConfigurationManager"/> if not already registered.
    /// </remarks>
    public static IServiceCollection AddWindowsExplorerServices(this IServiceCollection services)
    {
        services
            .AddSingleton<IWindowsExplorerOpener, WindowsExplorerOpener>()
            .AddSingleton<ILogPathOpener, LogPathOpener>()
            .TryAddScoped<IApplicationConfigurationManager, ApplicationConfigurationManager>();
        return services;
    }
    
}