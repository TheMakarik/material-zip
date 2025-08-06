namespace MaterialZip.Services.WindowsFunctions.Abstractions;

/// <summary>
/// Default service for opening logs directory in windows explorer
/// </summary>
public interface ILogPathOpener
{
    /// <summary>
    /// Open logs directory 
    /// </summary>
    public void Open();
}