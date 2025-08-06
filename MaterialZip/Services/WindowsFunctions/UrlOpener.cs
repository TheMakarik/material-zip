using System.Diagnostics;
using MaterialZip.Services.ValidationServices;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MaterialZip.Services.WindowsFunctions;

/// <summary>
/// Represent default implementation of <see cref="IUrlOpener"/>
/// </summary>
/// <param name="logger"><see cref="Serilog.ILogger"/> instance from DI</param>
/// <param name="processRunner"><see cref="IProcessRunner"/>instance from DI</param>
/// <param name="urlValidator"><see cref="AbsoluteUrlValidator"/>instance from DI</param>
public sealed class UrlOpener(ILogger<UrlOpener> logger, IProcessRunner processRunner, AbsoluteUrlValidator urlValidator) : IUrlOpener
{
    private const string ExceptionWasHappenedLogMessage = "Exception was happened and was handled";
    private const string UrlIsNotValidLogMessage = "Cannot open {url} because it is not valid url";
    private const string UrlWasOpenedSuccessfully = "{url} was opened successfully";
        
    /// <inheritdoc cref="IUrlOpener.TryOpen"/>
    public bool TryOpen(string url)
    {
        if (!urlValidator.IsValid(url))
        {
            logger.LogError(UrlIsNotValidLogMessage, url);
            return false;
        }
        
        try
        {
            processRunner.Run(new ProcessStartInfo
            {
                FileName = url,
                UseShellExecute = true 
            });
            logger.LogDebug(UrlWasOpenedSuccessfully, url);
            return true;
        }
        catch (Exception exception)
        {
            logger.LogWarning(exception, ExceptionWasHappenedLogMessage);
            return false;
        }
    }
}