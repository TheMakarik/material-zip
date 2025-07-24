using System.Drawing;
using System.Windows.Media.Imaging;

namespace MaterialZip.Services.WindowsFunctions.Abstractions;

/// <summary>
/// Builds <see cref="BitmapSource"/> from <see cref="Icon"/> objects
/// </summary>
public interface IBitmapSourceBuilder
{
    /// <summary>
    /// Converts an <see cref="Icon"/> to a <see cref="BitmapSource"/>.
    /// </summary>
    /// <param name="icon">The icon to convert</param>
    /// <returns>
    /// A new <see cref="BitmapSource"/> created from the icon
    /// </returns>
    public BitmapSource Build(Icon icon);
}