using System.Runtime.CompilerServices;
using MaterialZip.Bootstrapping.Abstractions;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace MaterialZip.Bootstrapping;

/// <summary>
/// Default implementation of <see cref="IBootstrapper"/> for loading the <see cref="System.Windows.Application"/> instance with DI, configuration and logging
/// </summary>
/// <param name="logging">Logger instance</param>
/// <param name="services">Service provider instance</param>
/// <param name="configuration">Configuration instance</param>
public class Bootstrapper(
    ILogger logging, 
    IServiceProvider services, 
    IConfiguration configuration) : IBootstrapper
{
    /// <inheritdoc cref="IBootstrapper.Logging"/>
    public ILogger Logging { get; init; } = logging;

    /// <inheritdoc cref="IBootstrapper.Services"/>
    public IServiceProvider Services { get; init; } = services;

    /// <inheritdoc cref="IBootstrapper.Configuration"/>
    public IConfiguration Configuration { get; init; } = configuration;

    /// <summary>
    /// Create a <see cref="BootstrapperBuilder"/> instance of <see cref="IBootstrapperBuilder"/> interface
    /// </summary>
    /// <returns>a new <see cref="BootstrapperBuilder"/> instance </returns>
    public static IBootstrapperBuilder CreateBuilder()
    {
        return new BootstrapperBuilder(); 
    }
}