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
    /// Summary menu
    /// </summary>
    public string View { get; }
}