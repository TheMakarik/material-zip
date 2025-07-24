using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.Services.WindowsFunctions;

public class BitmapSourceBuilder : IBitmapSourceBuilder
{
    public BitmapSource Build(Icon icon)
    {
        return Imaging.CreateBitmapSourceFromHIcon(
            icon.Handle,
            Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
    }
}