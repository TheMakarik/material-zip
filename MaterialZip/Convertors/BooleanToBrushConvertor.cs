using System.Drawing;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Brush = System.Windows.Media.Brush;

namespace MaterialZip.Convertors;

/// <summary>
/// Converts a boolean value to a Brush, using a specified Brush for true and a color from IHoverButtonHexGetter for false
/// </summary>
[ValueConversion(typeof(bool), typeof(Brush))]
public sealed class BooleanToBrushConvertor : IValueConverter
{
    
    private const string ConvertBackNotSupportedMessage = "Conversion from Brush back to bool is not supported";

    /// <summary>
    /// Singleton instance of the converter
    /// </summary>
    public static readonly BooleanToBrushConvertor Instance = new();

    /// <summary>
    /// Converts a boolean value to a Brush
    /// </summary>
    /// <param name="value">The boolean value to convert</param>
    /// <param name="targetType">The target type (should be Brush)</param>
    /// <param name="parameter">Brush to be used when value is true</param>
    /// <param name="culture">Culture information</param>
    /// <returns>
    /// Parameter Brush when true, color from IHoverButtonHexGetter when false,
    /// or null if input is invalid
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not bool boolean)
            return null;

        if (parameter is not Brush brushOnTrue)
            return null;

        return boolean ? brushOnTrue : GetButtonHex();
    }

    /// <summary>
    /// Reverse conversion is not supported
    /// </summary>
    /// <exception cref="NotSupportedException">Always thrown when called</exception>
    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException(ConvertBackNotSupportedMessage);
    }

   
    private Brush? GetButtonHex()
    {
        var hex = Ioc.Default.GetRequiredService<IHoverButtonHexGetter>().GetHoverButtonHex();
        return new BrushConverter().ConvertFromString(hex) as Brush;
    }
}