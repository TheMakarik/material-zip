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
    
    /// <inheritdoc/>
    public string Create => Strings.Create;
    
    /// <inheritdoc/>
    public string File => Strings.File;
    
    /// <inheritdoc/>
    public string Directory => Strings.Directory;
    
    /// <inheritdoc/>
    public string Archive => Strings.Archive;
    
    /// <inheritdoc/>
    public string AddSelectedToArchive => Strings.AddSelectedToArchive;
    
    /// <inheritdoc/>
    public string AllDirectories => Strings.AllDirectories;
    
    /// <inheritdoc/>
    public string AllFiles => Strings.AllFiles;
    
    /// <inheritdoc/>
    public string ByRegex => Strings.ByRegex;
    
    /// <inheritdoc/>
    public string Copy => Strings.Copy;
    
    /// <inheritdoc/>
    public string Cut => Strings.Cut;
    
    /// <inheritdoc/>
    public string Delete => Strings.Delete;
    
    /// <inheritdoc/>
    public string ExtractHere => Strings.ExtractHere;
    
    /// <inheritdoc/>
    public string ExtractToSpecificFolder => Strings.ExtractToSpecificFolder;
    
    /// <inheritdoc/>
    public string ListView => Strings.ListView;
    
    /// <inheritdoc/>
    public string OpenGitHub => Strings.OpenGitHub;
    
    /// <inheritdoc/>
    public string Paste => Strings.Paste;
    
    /// <inheritdoc/>
    public string Rename => Strings.Rename;
    
    /// <inheritdoc/>
    public string Select => Strings.Select;
    
    /// <inheritdoc/>
    public string Settings => Strings.Settings;
    
    /// <inheritdoc/>
    public string ShowHistory => Strings.ShowHistory;
    
    /// <inheritdoc/>
    public string ShowInWindowsExplorer => Strings.ShowInWindowsExplorer;
    
    /// <inheritdoc/>
    public string TableView => Strings.TableView;
}