namespace MaterialZip.Services.ExplorerServices.Abstractions;

/// <summary>
/// Default proxy abstractions of some methods of <see cref="System.IO.Directory"/> for explorer
/// </summary>
public interface ILowLevelExplorer
{
   
    /// <inheritdoc cref="System.IO.Directory.GetLogicalDrives"/>
    public IEnumerable<string> GetLogicalDrives();
    
    /// <inheritdoc cref="System.IO.Directory.EnumerateFiles(string)"/>
    public IEnumerable<string> EnumerateFiles(string directory);
    
    /// <inheritdoc cref="System.IO.Directory.EnumerateFiles(string)"/>
    public IEnumerable<string> EnumerateDirectories(string directory);
}