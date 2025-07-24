using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialZip.Model.Entities;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using Serilog;
using Image = System.Windows.Controls.Image;

namespace MaterialZip.Services.WindowsFunctions;


public class IconExtractor(ILogger logger, IAssociatedIconExtractor extractor, IBitmapSourceBuilder bitmapSourceBuilder) : IIconExtractor
{
    public BitmapSource? FromPath(string path)
    {
        var icon = extractor.Extract(path);
        if (icon is null)
            return null;
        
        return bitmapSourceBuilder.Build(icon);
    }
}