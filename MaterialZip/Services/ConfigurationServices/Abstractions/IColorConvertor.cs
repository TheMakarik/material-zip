using System.Windows.Media;
using MaterialDesignColors;
using MaterialZip.Model.Enums;

namespace MaterialZip.Services.ConfigurationServices.Abstractions;

/// <summary>
/// Represents abstractions for a <see cref="MaterialColor"/> convertor to <see cref="Color"/>
/// </summary>
public interface IColorConvertor
{
    /// <summary>
    /// Convert <see cref="color"/> to <see cref="PrimaryColor"/> using switch-case
    /// </summary>
    /// <param name="color">color to convert</param>
    /// <returns><see cref="Color"/> from <see cref="Color"/></returns>
    public Color ToWpfColor(MaterialColor color);
    
}