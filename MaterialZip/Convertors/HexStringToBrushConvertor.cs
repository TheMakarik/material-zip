using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MaterialZip.Convertors;

/// <summary>
/// Convert hex string to <see cref="Brush"/> using <see cref="BrushConverter"/>
/// </summary>
[ValueConversion(typeof(string), typeof(Brush))]
public sealed class HexStringToBrushConvertor : IValueConverter
{
    private const string HexPatternStart = "#";

    /// <summary>
    /// Instance of convertor
    /// </summary>
    public static HexStringToBrushConvertor Instance { get; } = new();
    
    /// <summary>
    /// Convert hex to <see cref="Brush"/>
    /// </summary>
    /// <param name="value">hex string</param>
    /// <param name="targetType">target type <see cref="Brush"/></param>
    /// <param name="parameter">Parameter from xaml</param>
    /// <param name="culture">Culture from xaml</param>
    /// <returns><see cref="Brush"/> from hex string</returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var hex = value as string;

        return new BrushConverter().ConvertFromString(hex!);
    }
    
    /// <summary>
    /// Convert back  <see cref="Brush"/> to  hex string
    /// </summary>
    /// <param name="value"><see cref="Brush"/> instance</param>
    /// <param name="targetType">target type string</param>
    /// <param name="parameter">Parameter from xaml</param>
    /// <param name="culture">Culture from xaml</param>
    /// <returns>string hex from <see cref="Brush"/></returns>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (value as Color?).ToString();
    }
    
}