using System.Drawing;
using System.Windows.Media.Imaging;
using MaterialZip.Model.Entities;

namespace MaterialZip.Services.WindowsFunctions.Abstractions;

/// <summary>
/// Provides functionality to extract icons from file system paths and convert them to bitmap sources
/// </summary>
public interface IIconExtractor
{
    /// <summary>
    /// Gets a <see cref="BitmapSource"/> representing the icon for the specified file path
    /// </summary>
    /// <param name="path">The file system path to get the icon for</param>
    /// <returns>
    /// A <see cref="BitmapSource"/> representing the file icon, or null if the icon couldn't be extracted
    /// </returns>
    public BitmapSource? FromPath(string path); 
}