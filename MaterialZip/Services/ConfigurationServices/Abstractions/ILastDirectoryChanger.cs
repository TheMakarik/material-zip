namespace MaterialZip.Services.ConfigurationServices.Abstractions;

/// <summary>
/// Represent default abstraction for changing last directory in configuration
/// </summary>
/// /// <remarks>
/// Do not call it too often, better use <see cref="ILastDirectoryBuffer"/> to change last directory and invoke this class only at application closing
/// </remarks>
public interface ILastDirectoryChanger
{
    /// <summary>
    /// Change LastDirectory in application configuration
    /// </summary>
    /// <param name="newDirectory">new directory to add in application configuration</param>
    public void ChangeLastDirectory(string newDirectory);
}