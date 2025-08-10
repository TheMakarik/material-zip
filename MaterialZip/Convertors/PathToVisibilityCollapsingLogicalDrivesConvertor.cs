using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using MaterialZip.Model.Entities;

namespace MaterialZip.Convertors;

[ValueConversion(typeof(string), typeof(Visibility))]
public class PathToVisibilityCollapsingLogicalDrivesConvertor : IValueConverter
{
    public static PathToVisibilityCollapsingLogicalDrivesConvertor Instance { get; } = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
      
        if(value is string path && IsPathLogicalDrive(path))
            return Visibility.Collapsed;
        return Visibility.Visible;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
    
    
    private bool IsPathLogicalDrive(string path) 
        => path.Length == 3; //logical drive path like: C:/, D:/ etc
}