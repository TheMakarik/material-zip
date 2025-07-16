using MaterialZip.Services.ConfigurationServices.Abstractions;
using Serilog;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Class for getting a last directory from <see cref="IApplicationConfigurationManager"/>
/// </summary>
/// <param name="configurationManager"><see cref="IApplicationConfigurationManager"/> instance </param>
public class LastDirectoryGetter(IApplicationConfigurationManager configurationManager) : ILastDirectoryGetter
{
    /// <inheritdoc cref="ILastDirectoryGetter.LastDirectory"/>
    public string LastDirectory { get; } = configurationManager.LastDirectory;
}