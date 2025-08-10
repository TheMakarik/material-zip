namespace MaterialZip.Services.LocalizationServices.Abstractions;

/// <summary>
/// Represent default <see cref="MaterialZip.Resources.Strings"/> wrapper
/// </summary>
public interface ILocalizationProvider
{
    /// <summary>
    /// Application name
    /// </summary>
    string AppName { get; }
    
    /// <summary>
    /// Name of <see cref="System.Windows.Controls.DataGrid"/> file header
    /// </summary>
    string Name { get; }
    
    /// <summary>
    /// Size of <see cref="System.Windows.Controls.DataGrid"/> file header
    /// </summary>
    string Size { get; }

    /// <summary>
    /// Date of creating of <see cref="System.Windows.Controls.DataGrid"/> file header
    /// </summary>
    string DateOfCreating { get; }
    
    /// <summary>
    /// Date of changing of <see cref="System.Windows.Controls.DataGrid"/> file header
    /// </summary>
    string DateOfChanging { get; }
    
    /// <summary>
    /// Edit menu
    /// </summary>
    string Edit { get; }
    
    /// <summary>
    /// Services menu
    /// </summary>
    string Services { get; }
    
    /// <summary>
    /// View menu
    /// </summary>
    string View { get; }

    /// <summary>
    /// Show more summary
    /// </summary>
    string ShowMore { get; }
    
    /// <summary>
    /// Exception occurred message
    /// </summary>
    string ExceptionOccured { get; }
    
    /// <summary>
    /// Exception occurred long message 
    /// </summary>
    string ExceptionOccuredMessage { get; }

    /// <summary>
    /// Open logs message
    /// </summary>
    string OpenLogs { get; }
    
    /// <summary>
    /// Copy stack trace message
    /// </summary>
    string CopyStackTrace { get; }
    
    /// <summary>
    /// Create message
    /// </summary>
    string Create { get; }
    
    /// <summary>
    /// Create file message
    /// </summary>
    string File { get; }
    
    /// <summary>
    /// Create directory message
    /// </summary>
    string Directory { get; }
    
    /// <summary>
    /// Create archive message
    /// </summary>
    string Archive { get; }

    /// <summary>
    /// Add selected to archive
    /// </summary>
    string AddSelectedToArchive { get; }
    
    /// <summary>
    /// All folders filter
    /// </summary>
    string AllDirectories { get; }
    
    /// <summary>
    /// All files filter
    /// </summary>
    string AllFiles { get; }
    
    /// <summary>
    /// Filter by regex
    /// </summary>
    string ByRegex { get; }
    
    /// <summary>
    /// Copy operation
    /// </summary>
    string Copy { get; }
    
    /// <summary>
    /// Cut operation
    /// </summary>
    string Cut { get; }
    
    /// <summary>
    /// Delete operation
    /// </summary>
    string Delete { get; }
    
    /// <summary>
    /// Extract here operation
    /// </summary>
    string ExtractHere { get; }
    
    /// <summary>
    /// Extract to specific folder
    /// </summary>
    string ExtractToSpecificFolder { get; }
    
    /// <summary>
    /// List view mode
    /// </summary>
    string ListView { get; }
    
    /// <summary>
    /// Open GitHub repository
    /// </summary>
    string OpenGitHub { get; }
    
    /// <summary>
    /// Paste operation
    /// </summary>
    string Paste { get; }
    
    /// <summary>
    /// Rename operation
    /// </summary>
    string Rename { get; }
    
    /// <summary>
    /// Select operation
    /// </summary>
    string Select { get; }
    
    /// <summary>
    /// Settings menu
    /// </summary>
    string Settings { get; }
    
    /// <summary>
    /// Show history
    /// </summary>
    string ShowHistory { get; }
    
    /// <summary>
    /// Show in Windows Explorer
    /// </summary>
    string ShowInWindowsExplorer { get; }
    
    /// <summary>
    /// Table view mode
    /// </summary>
    string TableView { get; }
}