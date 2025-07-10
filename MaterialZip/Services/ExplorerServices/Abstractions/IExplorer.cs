using MaterialZip.Model.Entities;

namespace MaterialZip.Services.ExplorerServices.Abstractions;

/// <summary>
/// Represent the abstraction of the default application explorer
/// </summary>
public interface IExplorer
{
    
    /// <summary>
    /// Get a logical drives from system
    /// </summary>
    /// <returns>new <see cref="IReadOnlyCollection{FileEntity}"/> with <see cref="FileEntity.IsDirectory"/> = true</returns>
    public IReadOnlyCollection<FileEntity> GetLogicalDrives();
    
    /// <summary>
    /// Get a content as <see cref="FileEntity"/> from the specific <see cref="FileEntity"/>
    /// </summary>
    /// <param name="directory"><see cref="FileEntity"/> instance/></param>
    /// <returns><see cref="IReadOnlyCollection{FileEntity}"/>  directory content</returns>
    /// <remarks>if <see cref="FileEntity"/> is not directory method must return empty array</remarks>
    public IReadOnlyCollection<FileEntity> GetDirectoryContent(FileEntity directory);
}