using MaterialZip.Model.Entities;

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
    /// <param name="directory">directory to add to the buffer</param>
    public void ToBuffer(FileEntity directory);
    
    /// <summary>
    /// Get directory from buffer
    /// </summary>
    /// <returns>directory from buffer</returns>
    public FileEntity FromBuffer();
}