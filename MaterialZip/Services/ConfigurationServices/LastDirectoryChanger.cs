using MaterialZip.Services.ConfigurationServices.Abstractions;
using Serilog;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Represent default <see cref="ILastDirectoryChanger"/> implementation to change last directory at configuration
/// </summary>
/// <param name="applicationConfigurationManager"></param>
/// <remarks>
/// Do not call it too often, better use <see cref="ILastDirectoryBuffer"/> to change last directory and invoke this class only at application closing
/// </remarks>
public class LastDirectoryChanger(IApplicationConfigurationManager applicationConfigurationManager, ILogger logger) : ILastDirectoryChanger
{
    private const string LastDirectoryChangedLogMessage = "Last directory in configuration was changed to {directory}";
    
    /// <inheritdoc cref="ILastDirectoryChanger.ChangeLastDirectory"/>
    public void ChangeLastDirectory(string newDirectory)
    {
        applicationConfigurationManager.LastDirectory = newDirectory;
        logger.Information(LastDirectoryChangedLogMessage, newDirectory);
    }
}