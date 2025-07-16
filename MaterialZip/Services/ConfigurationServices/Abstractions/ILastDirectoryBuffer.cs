namespace MaterialZip.Services.ConfigurationServices.Abstractions;

/// <summary>
/// Represents a buffer for last directory to not update configuration every time
/// </summary>
public interface ILastDirectoryBuffer
{
    /// <summary>
    /// Is this buffer empty
    /// </summary>
    public bool IsBufferEmpty { get; }
    
    /// <summary>
    /// Add directory to buffer
    /// </summary>
    /// <param name="directoryPath">directory path</param>
    public void ToBuffer(string directoryPath);
    
    /// <summary>
    /// Get directory from buffer
    /// </summary>
    /// <returns>directory from buffer</returns>
    public string FromBuffer();
}