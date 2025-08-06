using MaterialZip.Resources;
using MaterialZip.Services.LocalizationServices.Abstractions;

namespace MaterialZip.Services.LocalizationServices;

/// <inheritdoc cref="ILocalizationProvider"/>
public sealed class LocalizationProvider : ILocalizationProvider
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

    /// <inheritdoc/>
    public string ShowMore => Strings.ShowMoreText;
    
    /// <inheritdoc/>
    public string ExceptionOccured => Strings.ExceptionOccured;

    /// <inheritdoc/>
    public string ExceptionOccuredMessage => Strings.ExceptionOccuredMessage;
    
    /// <inheritdoc/>
    public string OpenLogs => Strings.OpenLogs;

    /// <inheritdoc/>
    public string CopyStackTrace => Strings.CopyStackTrace;
}