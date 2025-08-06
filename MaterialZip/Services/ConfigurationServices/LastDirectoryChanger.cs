using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Represent default <see cref="ILastDirectoryChanger"/> implementation to change last directory at configuration
/// </summary>
/// <param name="applicationConfigurationManager">Configuration manager from di</param>
/// /// <param name="logger">Logger from di</param>
/// <remarks>
/// Do not call it too often, better use <see cref="ILastDirectoryBuffer"/> to change last directory and invoke this class only at application closing
/// </remarks>
public sealed class LastDirectoryChanger(IApplicationConfigurationManager applicationConfigurationManager, ILogger<LastDirectoryChanger> logger) : ILastDirectoryChanger
{
    private const string LastDirectoryChangedLogMessage = "Last directory in configuration was changed to {directory}";
    
    /// <inheritdoc cref="ILastDirectoryChanger.ChangeLastDirectory"/>
    public void ChangeLastDirectory(string newDirectory)
    {
        applicationConfigurationManager.LastDirectory = newDirectory;
        logger.LogInformation(LastDirectoryChangedLogMessage, newDirectory);
    }
}