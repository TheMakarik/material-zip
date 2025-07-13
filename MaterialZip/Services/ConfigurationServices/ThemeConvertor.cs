using MaterialDesignThemes.Wpf;
using MaterialZip.Model.Enums;
using MaterialZip.Services.ConfigurationServices.Abstractions;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Theme convertor from <see cref="MaterialColor"/> to <see cref="BaseTheme"/>
/// </summary>
public class ThemeConvertor : IThemeConvertor
{
    /// <inheritdoc cref="IThemeConvertor.ToBaseTheme"/>
    public BaseTheme ToBaseTheme(MaterialTheme theme)
    {
        return theme == MaterialTheme.Light 
            ? BaseTheme.Light
            : BaseTheme.Dark;
    }
}