using System.Drawing;
using System.Windows.Media.Imaging;

namespace MaterialZip.Services.WindowsFunctions.Abstractions;

public interface IBitmapSourceBuilder
{
    public BitmapSource Build(Icon icon);
}