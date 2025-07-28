namespace MaterialZip.Services.WindowsFunctions.Abstractions;

/// <summary>
/// Abstraction to open url in the browser by default
/// </summary>
public interface IUrlOpener
{
    /// <summary>
    /// Try open url in the browser by default
    /// </summary>
    /// <param name="url">url to open</param>
    /// <returns>represent result that mean was the url open or not, possible reason to not open url is unvalid url</returns>
    public bool TryOpen(string url);
}