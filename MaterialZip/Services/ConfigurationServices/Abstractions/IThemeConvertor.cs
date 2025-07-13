using MaterialDesignThemes.Wpf;
using MaterialZip.Model.Enums;

namespace MaterialZip.Services.ConfigurationServices.Abstractions;

/// <summary>
/// Represent convertor from  <see cref="MaterialTheme"/> to <see cref="BaseTheme"/> abstractions
/// </summary>
public interface IThemeConvertor
{
    /// <summary>
    /// Convert <see cref="MaterialColor"/> to <see cref="BaseTheme"/>
    /// </summary>
    /// <param name="theme">Theme to convert</param>
    /// <returns>Converted <see cref="MaterialTheme"/> to <see cref="BaseTheme"/></returns>
    public BaseTheme ToBaseTheme(MaterialTheme theme);
}