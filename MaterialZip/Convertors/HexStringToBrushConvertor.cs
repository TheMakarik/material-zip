using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialZip.Convertors;

[ValueConversion(typeof(string), typeof(Brush))]
public class HexStringToBrushConvertor : IValueConverter
{
    private const string HexPatternStart = "#";

    public static HexStringToBrushConvertor Instance { get; } = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var hex = value as string;

        return new BrushConverter().ConvertFromString(hex!);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value as Color?).ToString();
    }
    
}