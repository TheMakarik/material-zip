using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.Services.WindowsFunctions;

/// <inheritdoc cref="IBitmapSourceBuilder"/>
public class BitmapSourceBuilder : IBitmapSourceBuilder
{
    /// <inheritdoc/>
    public BitmapSource Build(Icon icon)
    {
        return Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
    }
}