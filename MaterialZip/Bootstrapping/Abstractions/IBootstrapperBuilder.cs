using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MaterialZip.Bootstrapping.Abstractions;

/// <summary>
/// Represent interface for creating <see cref="IBootstrapper"/> instance supports logging using <see cref="Microsoft.Extensions.Logging"/>, configuration with <see cref="Microsoft.Extensions.Configuration"/> and di using <see cref="Microsoft.Extensions.DependencyInjection"/>
/// </summary>
public interface IBootstrapperBuilder
{
    /// <summary>
    /// Logger factory for application  with <see cref="Microsoft.Extensions.Logging"/>
    /// </summary>
    public LoggerConfiguration Logging { get; set; }
    
    /// <summary>
    ///  DI Services container builder  for application  with <see cref="Microsoft.Extensions.DependencyInjection"/>
    /// </summary>
    /// 
    public IServiceCollection Services { get; set; }
    
    /// <summary>
    /// Application's configuration manager property with <see cref="Microsoft.Extensions.Configuration"/>
    /// </summary>
    public IConfigurationManager Configuration { get; set; }
    
    /// <summary>
    /// Build the <see cref="IBootstrapper"/> instance as <see cref="Bootstrapper"/>
    /// </summary>
    /// <returns>new <see cref="IBootstrapper"/> instance</returns>
    public IBootstrapper CreateBootstrapper();
}