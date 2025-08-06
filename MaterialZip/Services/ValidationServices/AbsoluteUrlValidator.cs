using MaterialZip.Services.ValidationServices.Abstractions;

namespace MaterialZip.Services.ValidationServices;

/// <summary>
/// Default absolute url validator
/// </summary>
public sealed class AbsoluteUrlValidator : IValidator
{
    /// <summary>
    /// Validate url using <see cref="Uri.TryCreate(string, UriKind, out Uri)"/> method
    /// </summary>
    /// <param name="value">Value to validate</param>
    /// <typeparam name="T">Type of validating value</typeparam>
    /// <returns>Result of the validating</returns>
    public bool IsValid<T>(T value)
    {
        if (value is not string uri)
            return false;
        return Uri.TryCreate(uri, UriKind.Absolute, out var result);
    }
}