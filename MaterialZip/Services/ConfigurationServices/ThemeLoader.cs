using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using MaterialZip.Model.Enums;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Logging;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Class for loading application theme
/// </summary>
/// <param name="colorConvertor"> <see cref="IColorConvertor"/> instance from DI container</param>
/// <param name="applicationConfigurationManager"> <see cref="IApplicationConfigurationManager"/> instance from DI container</param>
/// <param name="paletteHelper"> <see cref="PaletteHelper"/> Instance from DI container</param>
/// <param name="logger"><see cref="ILogger{T}"/>instance from DI container </param>
/// <remarks>
/// Class' method <see cref="LoadTheme"/> must be call once after <see cref="App"/> class created 
/// </remarks>
public sealed class ThemeLoader(
    IColorConvertor colorConvertor,
    IApplicationConfigurationManager applicationConfigurationManager,
    PaletteHelper paletteHelper,
    ILogger<ThemeLoader> logger) : IThemeLoader
{
    private const string StartTryingToLoadThemeLogMessage =
        "Run trying to load theme with color: {Color}";
    private const string SucceedMaterialColorsConvertingLogMessage =
        "Material color was succesefully converted to: {primaryColor}";
    private const string ThemeWasLoadedLogMessage
        = "Application theme was loaded";
    

    private readonly MaterialColor _primaryColor = applicationConfigurationManager.Color;

    /// <summary>
    /// Load current theme from configuration
    /// </summary>
    public void LoadTheme()
    {
        logger.LogInformation(StartTryingToLoadThemeLogMessage, _primaryColor.ToString());
        
        var color = ConvertMaterialDesignColor();
        
        logger.LogInformation(SucceedMaterialColorsConvertingLogMessage, color.ToString());

        SetTheme(color);
        logger.LogInformation(ThemeWasLoadedLogMessage);

    }

    private void SetTheme(Color color)
    {
        var theme = paletteHelper.GetTheme();
        theme.SetPrimaryColor(color);
        paletteHelper.SetTheme(theme);
    }

    private Color  ConvertMaterialDesignColor()
    {
        var color = colorConvertor.ToWpfColor(_primaryColor);
        return color;
    }
}