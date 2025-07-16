namespace MaterialZip.Services.ConfigurationServices.Abstractions;

/// <summary>
/// Represent default abstraction to get a last directory from configuration
/// </summary>
public interface ILastDirectoryGetter
{
    /// <summary>
    /// Get a last directory (was loaded then instance was created) from configuration
    /// </summary>
    public string LastDirectory { get; }
}