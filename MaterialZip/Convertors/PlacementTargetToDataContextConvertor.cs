using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MaterialZip.Convertors;

[ValueConversion(typeof(UIElement), typeof(object))]
public class PlacementTargetToDataContextConvertor : IValueConverter
{
    public static PlacementTargetToDataContextConvertor Instance { get; }= new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DependencyObject depObj)
        {
            var window = Window.GetWindow(depObj);
            return window?.DataContext;
        }
        return null;
        
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}