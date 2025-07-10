using System.IO;
using MaterialZip.Services.ExplorerServices.Abstractions;

namespace MaterialZip.Services.ExplorerServices;

/// <summary>
/// Default proxy implementation of some methods of  <see cref="System.IO.Directory"/>
/// </summary>
public sealed class LowLevelExplorer : ILowLevelExplorer
{
    /// <inheritdoc cref="ILowLevelExplorer.GetLogicalDrives"/>
    public string[] GetLogicalDrives()
    {
        return Directory.GetLogicalDrives();
    }

    /// <inheritdoc cref="ILowLevelExplorer.GetFiles"/>
    public string[] GetFiles(string directory)
    {
        return Directory.GetFiles(directory);
    }

    /// <inheritdoc cref="ILowLevelExplorer.GetDirectories"/>
    public string[] GetDirectories(string directory)
    {
        return Directory.GetDirectories(directory);
    }
}