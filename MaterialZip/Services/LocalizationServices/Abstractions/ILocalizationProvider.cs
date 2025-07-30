namespace MaterialZip.Services.LocalizationServices.Abstractions;

/// <summary>
/// Represent default localization wrapper
/// </summary>
public interface ILocalizationProvider
{
    /// <summary>
    /// Application name
    /// </summary>
    public string AppName { get; }
}