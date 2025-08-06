using System.Diagnostics;
using MaterialZip.Extensions;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using Microsoft.Extensions.Logging;
using System.IO;

namespace MaterialZip.Services.WindowsFunctions;

/// <inheritdoc cref="IWindowsExplorerOpener"/>
public sealed class WindowsExplorerOpener(
    ILogger<WindowsExplorerOpener> logger, 
    IApplicationConfigurationManager applicationConfigurationManager, 
    IProcessRunner processRunner) : IWindowsExplorerOpener
{
    private const string ExceptionOccuredDyingWindowsExplorerOpening 
        = "Exception {exception} occured while opening {path}";
    private const string FileWasShownInWindowsExplorer 
        = "The file {file} was shown in explorer";
    
    /// <inheritdoc />
    public void Open(string path)
    {
        var explorerPath = applicationConfigurationManager.WindowsExplorerPathInWindows;
        var windowsFolder = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
        var fullPath = windowsFolder + explorerPath.ReplaceAltDirectorySeparator();
        try
        {
            processRunner.Run(new ProcessStartInfo(
                fileName: fullPath,
                arguments: path
            ));
            logger.LogDebug(FileWasShownInWindowsExplorer, path);
        }
        catch (Exception e)
        {
            logger.LogError(ExceptionOccuredDyingWindowsExplorerOpening, e.ToString(), fullPath);
        }
      
    }
}