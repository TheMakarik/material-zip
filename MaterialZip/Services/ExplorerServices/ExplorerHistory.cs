using System.Collections;
using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MaterialZip.Services.ExplorerServices;


public sealed class ExplorerHistory(ILogger<ExplorerHistory> logger, IExplorerHistoryMemory memory) : IExplorerHistory
{
    
    private const string CannotRedoLogMessage = "Cannot redo because CanRedo is false, current index: {index}, history count: {count}, exception will be thrown";
    private const string CannotUndoLogMessage = "Cannot undo because CanUndo is false, current index: {index}, history count: {count}, exception will be thrown";
    private const string UndoWasMadeLogMessage = "Undo from {directory} to {directory}";
    private const string RedoWasMadeLogMessage = "Redo from {directory} to {directory}";
    
    private const string CannotRedoExceptionText = "Tried to invoke redo while CanRedo is false, possible forgotten validation";
    private const string CannotUndoExceptionText = "Tried to invoke undo while CanUndo is false, possible forgotten validation";
    
    private object _lock = new();
    
    /// <inheritdoc cref="IExplorerHistory.CurrentDirectory"/>
    public FileEntity CurrentDirectory { get => memory.CurrentDirectory; set => memory.CurrentDirectory = value; }
    
    /// <inheritdoc cref="IExplorerHistory.CanRedo"/>
    public bool CanRedo => memory.Index + 1 < memory.HistoryList.Count();
 
    /// <inheritdoc cref="IExplorerHistory.CanUndo"/>
    public bool CanUndo => memory.Index > 0;
    
    /// <inheritdoc cref="IExplorerHistory.Redo"/>
    public void Redo()
    {
        lock (_lock)
        {
            if (!CanRedo)
            {
                logger.LogCritical(CannotRedoLogMessage, memory.Index, memory.HistoryList.Count());
                throw new CannotRedoException(CannotRedoExceptionText); 
            }
            memory.Index++;
            memory.IsRedoDone = true;
            logger.LogDebug(RedoWasMadeLogMessage, memory.HistoryList.ElementAt(memory.Index - 1), CurrentDirectory.Path);
        }
    }

    /// <inheritdoc cref="IExplorerHistory.Undo"/>
    public void Undo()
    {
        lock (_lock)
        {
            if (!CanUndo)
            {
                logger.LogCritical(CannotUndoLogMessage, memory.Index, memory.HistoryList.Count());
                throw new CannotUndoException(CannotUndoExceptionText);
            }
            memory.Index--;
            logger.LogDebug(UndoWasMadeLogMessage, memory.HistoryList.ElementAt(memory.Index + 1), CurrentDirectory.Path);
        }
    }

    /// <inheritdoc cref="List{T}.GetEnumerator"/>
    public IEnumerator<FileEntity> GetEnumerator() 
        => memory.HistoryList.GetEnumerator();

    /// <inheritdoc cref="List{T}.GetEnumerator"/>
    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();
    
}