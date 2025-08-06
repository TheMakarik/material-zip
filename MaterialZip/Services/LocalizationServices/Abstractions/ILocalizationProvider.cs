namespace MaterialZip.Services.LocalizationServices.Abstractions;

/// <summary>
/// Represent default <see cref="MaterialZip.Resources.Strings"/> wrapper
/// </summary>
public interface ILocalizationProvider
{
    /// <summary>
    /// Application name
    /// </summary>
    public string AppName { get; }
    
    /// <summary>
    /// Name of <see cref="System.Windows.Controls.DataGrid"/> file header
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// Size of <see cref="System.Windows.Controls.DataGrid"/> file header
    /// </summary>
    public string Size { get; }

    /// <summary>
    ///  Date of creating of <see cref="System.Windows.Controls.DataGrid"/> file header
    /// </summary>
    public string DateOfCreating { get; }
    
    /// <summary>
    ///  Date of changing of <see cref="System.Windows.Controls.DataGrid"/> file header
    /// </summary>
    public string DateOfChanging { get; }
    
    /// <summary>
    /// Edit menu
    /// </summary>
    public string Edit { get; }
    
    /// <summary>
    ///  Services menu
    /// </summary>
    public string Services { get; }
    
    /// <summary>
    /// View menu
    /// </summary>
    public string View { get; }

    /// <summary>
    /// Show more summary
    /// </summary>
    public string ShowMore { get; }
    
    /// <summary>
    /// Exception occurred message
    /// </summary>
    
    public string ExceptionOccured { get; }
    
    /// <summary>
    /// Exception occured long message 
    /// </summary>
    public string ExceptionOccuredMessage { get; }

    /// <summary>
    /// Open logs message
    /// </summary>
    public string OpenLogs { get; }
    
    /// <summary>
    /// Copy stack trace message
    /// </summary>
    public string CopyStackTrace { get; }
}