using MaterialZip.Services.ConfigurationServices.Abstractions;
using Serilog;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Class for getting hover button hex value
/// </summary>
/// <param name="applicationConfigurationManager"> <see cref="IApplicationConfigurationManager"/> instance from DI container</param>
/// <param name="logger"><see cref="ILogger"/> instance from DI container</param>
public class HoverButtonHexGetter(IApplicationConfigurationManager applicationConfigurationManager, ILogger logger) : IHoverButtonHexGetter
{
    private const string HoverButtonHexWasLoadedLogMessage = "Hover button hex: {hex} was loaded successefully";
    
    /// <inheritdoc cref="IHoverButtonHexGetter.GetHoverButtonHex"/>
    public string GetHoverButtonHex()
    {
        var hex = applicationConfigurationManager.HoverColorHex;
        logger.Debug(HoverButtonHexWasLoadedLogMessage, hex);
        return hex;
    }
}