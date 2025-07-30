using MaterialZip.Services.LocalizationServices.Abstractions;
using Microsoft.Extensions.Localization;

namespace MaterialZip.Services.LocalizationServices;

/// <inheritdoc cref="ILocalizationProvider"/>
public class LocalizationProvider<T>(IStringLocalizer<T> localizer) : ILocalizationProvider
{
    /// <inheritdoc/>
    public string AppName => localizer[nameof(AppName)];
}