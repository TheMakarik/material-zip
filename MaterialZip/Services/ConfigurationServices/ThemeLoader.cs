using System.Windows.Media;
using MaterialDesignColors;
using MaterialDesignColors.ColorManipulation;
using MaterialDesignThemes.Wpf;
using MaterialZip.Model.Enums;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Class for loading application theme
/// </summary>
/// <param name="themeConvertor"> <see cref="IThemeConvertor"/> instance from DI container</param>
/// <param name="colorConvertor"> <see cref="IColorConvertor"/> instance from DI container</param>
/// <param name="applicationConfigurationManager"> <see cref="IApplicationConfigurationManager"/> instance from DI container</param>
/// <param name="paletteHelper"> <see cref="PaletteHelper"/> Instance from DI container</param>
/// <param name="logger"><see cref="ILogger"/>instance from DI container </param>
/// <remarks>
/// Class' method <see cref="LoadTheme"/> must be call once after <see cref="App"/> class created 
/// </remarks>
public class ThemeLoader(
    IThemeConvertor themeConvertor,
    IColorConvertor colorConvertor,
    IApplicationConfigurationManager applicationConfigurationManager,
    PaletteHelper paletteHelper,
    ILogger logger) : IThemeLoader
{
    private const string StartTryingToLoadThemeLogMessage =
        "Start trying to load theme, expected theme: {theme}, primary color: {primaryColor}, secondaryColor {secondaryColor}";
    private const string SucceedMaterialColorsConvertingLogMessage =
        "Material theme was succesefully converted to: theme: {theme}, primary color: {primaryColor}, secondaryColor: {secondaryColor}";
    private const string ThemeWasLoadedLogMessage
        = "Application theme was loaded";
    
    
    private readonly MaterialTheme _theme = applicationConfigurationManager.Theme;
    private readonly MaterialColor _primaryColor = applicationConfigurationManager.PrimaryColor;
    private readonly MaterialColor _secondaryColor = applicationConfigurationManager.SecondaryColor;
    
    /// <summary>
    /// Load current theme from configuration
    /// </summary>
    public void LoadTheme()
    {
        logger.Information(StartTryingToLoadThemeLogMessage,
            _theme.ToString(), 
            _primaryColor.ToString(), 
            _secondaryColor.ToString());
        
        var applicationTheme = ConvertMaterialDesign();
        
        logger.Information(SucceedMaterialColorsConvertingLogMessage, 
            applicationTheme.Theme.ToString(), 
            applicationTheme.PrimaryColor.ToString(), 
            applicationTheme.SecondaryColor.ToString());

        var theme = Theme.Create(applicationTheme.Theme, applicationTheme.PrimaryColor, applicationTheme.SecondaryColor);
        paletteHelper.SetTheme(theme);
        
        logger.Information(ThemeWasLoadedLogMessage);

    }

    private (BaseTheme Theme, Color PrimaryColor, Color SecondaryColor) ConvertMaterialDesign()
    {
        var theme = themeConvertor.ToBaseTheme(_theme);
        var primaryColor = colorConvertor.ToWpfColor(_primaryColor);
        var secondaryColor = colorConvertor.ToWpfColor(_secondaryColor);

        return (theme, primaryColor, secondaryColor); 
    }
    
    
}