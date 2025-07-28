namespace MaterialZip.Services.WindowsFunctions.Abstractions;

/// <summary>
/// Represents default abstraction to open this project code source
/// </summary>
public interface IGitHubSourceOpener
{
    /// <summary>
    /// Try to open this project code source in the browser
    /// </summary>
    /// <returns>Result represents that was the url open or not</returns>
    public bool TryOpen();
}