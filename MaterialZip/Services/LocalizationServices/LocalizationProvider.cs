using MaterialZip.Resources;
using MaterialZip.Services.LocalizationServices.Abstractions;

namespace MaterialZip.Services.LocalizationServices;

/// <inheritdoc cref="ILocalizationProvider"/>
public class LocalizationProvider : ILocalizationProvider
{
    /// <inheritdoc/>
    public string AppName => Strings.AppName;

    /// <inheritdoc/>
    public string Name => Strings.Name;
    
    /// <inheritdoc/>
    public string Size => Strings.Size;
    
    /// <inheritdoc/>
    public string DateOfCreating => Strings.DateOfCreating;
    

    /// <inheritdoc/>
    public string DateOfChanging => Strings.DateOfChanging;
    
    /// <inheritdoc/>
    public string Edit => Strings.Edit;
    
    /// <inheritdoc/>
    public string Services => Strings.Services;
    
    /// <inheritdoc/>
    public string View => Strings.View;
}