using System.Drawing;

namespace MaterialZip.Services.WindowsFunctions.Abstractions;

/// <summary>
/// Extracts associated icons from files.
/// </summary>
public interface IAssociatedIconExtractor
{
    /// <summary>
    /// Extracts the associated icon from the specified file.
    /// </summary>
    /// <param name="filePath">The path to the file to extract the icon from.</param>
    /// <returns>
    /// The extracted <see cref="Icon"/> or null if extraction fails.
    /// </returns>
    public Icon? Extract(string filePath);
}
