using MaterialZip.Bootstrapping.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MaterialZip.Bootstrapping;

/// <summary>
/// Builder of <see cref="Bootstrapper"/> inctance
/// </summary>
public class BootstrapperBuilder : IBootstrapperBuilder
{
    /// <inheritdoc cref="IBootstrapperBuilder.Services"/>
    public IServiceCollection Services { get; set; } = new ServiceCollection();
    
    /// <inheritdoc cref="IBootstrapperBuilder.Configuration"/>
    public IConfigurationManager Configuration { get; set; } = new ConfigurationManager();
    
    /// <inheritdoc cref="IBootstrapperBuilder.CreateBootstrapper"/>
    public IBootstrapper CreateBootstrapper()
    {
        return new Bootstrapper(Services.BuildServiceProvider(), Configuration.Build()); 
    }
}