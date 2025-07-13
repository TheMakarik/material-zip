using System.Globalization;
using MaterialZip.Model.Enums;


namespace MaterialZip.Options.Abstractions;

/// <summary>
/// Represent default abstraction of the application's configuration
/// </summary>
public interface IApplicationOptions
{
    /// <summary>
    /// Application's Language's <see cref="CultureInfo"/>
    /// </summary>
    public CultureInfo Language { get; set; }
    
    /// <summary>
    /// Current Application's theme
    /// </summary>
    /// <remarks>This enum will be converted to <see cref=" MaterialDesignThemes.Wpf.BaseTheme"/></remarks>
    public MaterialTheme Theme { get; set; }
    
    /// <summary>
    /// Current application primary color
    /// </summary>
    /// <remarks>This enum will be converted to <see cref=" MaterialDesignColors.PrimaryColor"/></remarks>
    public MaterialColor PrimaryColor { get; set; }
    
    /// <summary>
    /// Current application secondary color
    /// </summary>
    /// <remarks>This enum will be converted to <see cref=" MaterialDesignColors.PrimaryColor"/></remarks>
    public MaterialColor SecondaryColor { get; set; }
    
    /// <summary>
    /// Last opened directory 
    /// </summary>
    /// <remarks>Dont change it every time last directory was changed, it's hard to update configuration every time, create a buffer and update configuration only on closing</remarks>
    public string LastDirectory { get; set; }
    
    /// <summary>
    /// Resource dictionaries location
    /// </summary>
    public string ResourcesLocation { get; set; }
}