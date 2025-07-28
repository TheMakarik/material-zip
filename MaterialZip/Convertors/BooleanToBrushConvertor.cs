using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Brush = System.Windows.Media.Brush;

namespace MaterialZip.Convertors;

[ValueConversion(typeof(bool), typeof(Brush))]
public class BooleanToBrushConvertor : IValueConverter
{
    public static BooleanToBrushConvertor Instance = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool boolean)
            return null;

        if (parameter is not Brush brushOnTrue)
            return null;

        return boolean ? brushOnTrue : GetButtonHex();
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private Brush? GetButtonHex()
    {
        var hex = Ioc.Default.GetRequiredService<IHoverButtonHexGetter>().GetHoverButtonHex();
        return (new BrushConverter().ConvertFromString(hex)) as Brush;
    }
}