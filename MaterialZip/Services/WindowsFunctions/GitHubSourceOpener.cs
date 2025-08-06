using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using Serilog;

namespace MaterialZip.Services.WindowsFunctions;

/// <summary>
/// Represent service for opening this source code github page 
/// </summary>
/// <param name="urlOpener"><see cref="IUrlOpener"/> instance from DI</param>
/// <param name="applicationConfigurationManager"> <see cref="IApplicationConfigurationManager "/> instance from DI</param>
public sealed class GitHubSourceOpener(IUrlOpener urlOpener, IApplicationConfigurationManager applicationConfigurationManager) : IGitHubSourceOpener
{
    /// <inheritdoc cref="IGitHubSourceOpener.TryOpen"/>
    public bool TryOpen()
    {
        return urlOpener.TryOpen(applicationConfigurationManager.GitHubSourceLink);
    }
}