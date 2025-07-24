using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.Convertors;

[ValueConversion(typeof(string), typeof(ImageSource))]
public class PathToImageSourceConvertor : IValueConverter
{

    public static PathToImageSourceConvertor Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var path = value as string;

        if (Directory.Exists(path))
        {
            if (IsDirectory(path))
                return new BitmapImage(new Uri("pack://application:,,,/Assets/FileIcons/folder.png"));
            return new BitmapImage(new Uri("pack://application:,,,/Assets/FileIcons/harddisk.png"));
        }
        
        if (path is null)
            return null;
        
        return Ioc.Default.GetRequiredService<IIconExtractor>().FromPath(path);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    
    private bool IsDirectory(string path) => path.Length > 3;
}
