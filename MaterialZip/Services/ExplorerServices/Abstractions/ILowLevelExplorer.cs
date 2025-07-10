namespace MaterialZip.Services.ExplorerServices.Abstractions;

/// <summary>
/// Default proxy abstractions of some methods of <see cref="System.IO.Directory"/> for explorer
/// </summary>
public interface ILowLevelExplorer
{
   
    /// <inheritdoc cref="System.IO.Directory.GetLogicalDrives"/>
    public string[] GetLogicalDrives();
    
    /// <inheritdoc cref="System.IO.Directory.GetFiles(string)"/>
    public string[] GetFiles(string directory);
    
    /// <inheritdoc cref="System.IO.Directory.GetDirectories(string)"/>
    public string[] GetDirectories(string directory);
}