using MaterialZip.Model.Entities;

namespace MaterialZip.Services.ExplorerServices.Abstractions;

/// <summary>
/// Base abstraction for <see cref="ExplorerHistory"/> memory saver
/// </summary>
public interface IExplorerHistoryMemory
{
    /// <summary>
    /// List of saved <see cref="FileEntity"/>
    /// </summary>
    public List<FileEntity> HistoryList { get; }
    
    /// <summary>
    /// Index of current <see cref="FileEntity"/>
    /// </summary>
    public int Index { get; set; }
    
    /// <summary>
    /// Current <see cref="FileEntity"/>, on set will update <see cref="HistoryList"/>
    /// </summary>
    public FileEntity CurrentDirectory { get; set; }
}