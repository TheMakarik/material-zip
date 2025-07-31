using Microsoft.Extensions.Configuration;
using Serilog;

namespace MaterialZip.Bootstrapping.Abstractions;

/// <summary>
/// Represent abstraction for a application bootstrapper, supports logging using <see cref="Microsoft.Extensions.Logging"/>, configuration with <see cref="Microsoft.Extensions.Configuration"/> and di using <see cref="Microsoft.Extensions.DependencyInjection"/>
/// </summary>
/// <remarks>
/// Pay attention <see cref="IBootstrapper"/> implementation must get properties from builder. for example <see cref="IBootstrapperBuilder.CreateBootstrapper"/> and <see cref="IBootstrapperBuilder"/> must build <see cref="IBootstrapperBuilder"/> implementation class and load properties from constructor
/// </remarks>
public interface IBootstrapper
{
    /// <summary>
    /// DI Services container for application  with <see cref="Microsoft.Extensions.DependencyInjection"/>
    /// </summary>
    public IServiceProvider Services { get; init; }
    
    /// <summary>
    /// Application's configuration property with <see cref="Microsoft.Extensions.Configuration"/>
    /// </summary>
    public IConfiguration Configuration { get; init; }
}