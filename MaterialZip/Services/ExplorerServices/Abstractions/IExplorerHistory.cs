using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;

namespace MaterialZip.Services.ExplorerServices.Abstractions;

/// <summary>
/// Represents default abstraction of the explorer history saver
/// </summary>
public interface IExplorerHistory : IEnumerable<FileEntity>
{
    /// <summary>
    /// Represents current directory at history, use setter to add and getter to get directory
    /// </summary>
    public FileEntity CurrentDirectory { get; set; }
    
       
    /// <summary>
    /// Can you invoke <see cref="Redo"/> method without exception
    /// </summary>
    public bool CanRedo { get; }
    
    /// <summary>
    /// Can you invoke <see cref="Undo"/> method without exception
    /// </summary>
    public bool CanUndo { get; }
    
    /// <summary>
    /// Redo last <see cref="FileEntity"/> 
    /// </summary>
    /// <exception cref="CannotRedoException">Throws when you cannot make redo because <see cref="CanRedo"/> is false/></exception>
    public void Redo();
    
    /// <summary>
    /// Redo last <see cref="FileEntity"/> 
    /// </summary>
    /// <exception cref="CannotRedoException">Throws when you cannot make redo because <see cref="CanRedo"/> is false</exception>

    public void Undo();
    
}