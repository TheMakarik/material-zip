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
    /// Current application color
    /// </summary>
    /// <remarks>This enum will be converted to <see cref=" MaterialDesignColors.PrimaryColor"/></remarks>
    public MaterialColor Color { get; set; }

    /// <summary>
    /// Link to this application source
    /// </summary>
    public string GitHubSourceLink { get; set; }
    
    /// <summary>
    /// Hex color of button's hover effect
    /// </summary>
    public string HoverColorHex { get; set; }
    
    /// <summary>
    /// Last opened directory 
    /// </summary>
    /// <remarks>Dont change it every time last directory was changed, it's hard to update configuration every time, create a buffer and update configuration only on closing</remarks>
    public string LastDirectory { get; set; }
    
}