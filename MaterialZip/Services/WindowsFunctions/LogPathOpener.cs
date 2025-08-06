using System.Diagnostics;
using System.IO;
using System.Reflection;
using MaterialZip.Extensions;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.Services.WindowsFunctions;

/// <inheritdoc cref="ILogPathOpener"/>
public sealed class LogPathOpener(IWindowsExplorerOpener windowsExplorerOpener, IApplicationConfigurationManager applicationConfigurationManager) : ILogPathOpener
{
    /// <inheritdoc/>
    public void Open()
    {
        var path = GetAbsoluteLogPath();
        windowsExplorerOpener.Open(path);
    }

    private string GetAbsoluteLogPath() 
        => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + applicationConfigurationManager.LogsPath.ReplaceAltDirectorySeparator();
}