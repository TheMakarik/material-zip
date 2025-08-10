using MaterialZip.Services.ValidationServices.Abstractions;
using Microsoft.Extensions.Logging;

namespace MaterialZip.Services.ValidationServices;

/// <summary>
/// Default absolute url validator
/// </summary>
public sealed class AbsoluteUrlValidator(ILogger<AbsoluteUrlValidator> logger) : IValidator<string>
{
    private const string UrlValidationResultLogMessage = "Result of validation {url}: {result}";
    
    /// <summary>
    /// Validate url using <see cref="Uri.TryCreate(string, UriKind, out Uri)"/> method
    /// </summary>
    /// <param name="value">Value to validate</param>
    /// <returns>Result of the validating</returns>
    public bool IsValid(string value)
    {
        var validationResult =  Uri.TryCreate(value, UriKind.Absolute, out var result);
        logger.LogDebug(UrlValidationResultLogMessage, value, validationResult);
        return validationResult;
    }
}