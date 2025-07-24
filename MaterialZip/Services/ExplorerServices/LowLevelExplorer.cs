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

    /// <inheritdoc cref="ILowLevelExplorer.EnumerateFiles"/>
    public IEnumerable<string> EnumerateFiles(string directory)
    {
        return Directory.EnumerateFiles(directory);
    }

    /// <inheritdoc cref="ILowLevelExplorer.EnumerateDirectories"/>
    public IEnumerable<string> EnumerateDirectories(string directory)
    {
        return Directory.EnumerateDirectories(directory);
    }
}