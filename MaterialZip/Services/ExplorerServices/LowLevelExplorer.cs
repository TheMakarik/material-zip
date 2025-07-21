using System.IO;
using MaterialZip.Services.ExplorerServices.Abstractions;

namespace MaterialZip.Services.ExplorerServices;

/// <summary>
/// Default proxy implementation of some methods of  <see cref="System.IO.Directory"/>
/// </summary>
public sealed class LowLevelExplorer : ILowLevelExplorer
{
    /// <inheritdoc cref="ILowLevelExplorer.GetLogicalDrives"/>
    public IEnumerable<string>GetLogicalDrives()
    {
        return Directory.GetLogicalDrives();
    }

    /// <inheritdoc cref="ILowLevelExplorer.GetFiles"/>
    public IEnumerable<string> GetFiles(string directory)
    {
        return Directory.EnumerateFiles(directory);
    }

    /// <inheritdoc cref="ILowLevelExplorer.GetDirectories"/>
    public IEnumerable<string> GetDirectories(string directory)
    {
        return Directory.EnumerateDirectories(directory);
    }
}