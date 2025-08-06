using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Logging;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Class for getting hover button hex value
/// </summary>
public sealed class HoverButtonHexGetter : IHoverButtonHexGetter
{
    private const string HoverButtonHexWasLoadedLogMessage = "Hover button hex: {hex} was loaded successefully";
    
    private readonly IApplicationConfigurationManager _applicationConfigurationManager;

    /// <summary>
    /// Class for getting hover button hex value constructor
    /// </summary>
    /// <param name="applicationConfigurationManager"> <see cref="IApplicationConfigurationManager"/> instance from DI container</param>
    /// <param name="logger"><see cref="ILogger{T}"/> instance from DI container</param>
    public HoverButtonHexGetter(IApplicationConfigurationManager applicationConfigurationManager, ILogger<HoverButtonHexGetter> logger)
    {
        _applicationConfigurationManager = applicationConfigurationManager; ;
        logger.LogDebug(HoverButtonHexWasLoadedLogMessage, _applicationConfigurationManager.HoverColorHex);
    }
    
    /// <inheritdoc cref="IHoverButtonHexGetter.GetHoverButtonHex"/>
    public string GetHoverButtonHex()
    {
        return _applicationConfigurationManager.HoverColorHex;
    }
}