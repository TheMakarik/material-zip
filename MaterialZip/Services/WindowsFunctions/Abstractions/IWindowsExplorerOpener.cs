namespace MaterialZip.Services.WindowsFunctions.Abstractions;

/// <summary>
/// Represent windows explorer opening service
/// </summary>
public interface IWindowsExplorerOpener
{
    /// <summary>
    /// Open windows explorer with specific path
    /// </summary>
    /// <param name="path">Path for showing in windows explorer</param>
    public void Open(string path);
}