using System.Collections;
using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Serilog;

namespace MaterialZip.Services.ExplorerServices;


public sealed class ExplorerHistory(ILogger logger, IExplorerHistoryMemory memory) : IExplorerHistory
{
    
    private const string CannotRedoLogMessage = "Cannot redo because CanRedo is false, current index: {index}, history count: {count}, exception will be thrown";
    private const string CannotUndoLogMessage = "Cannot undo because CanUndo is false, current index: {index}, history count: {count}, exception will be thrown";

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
                logger.Fatal(CannotRedoLogMessage, memory.Index, memory.HistoryList.Count());
                throw new CannotRedoException(CannotRedoExceptionText); 
            }
            memory.Index++;
            memory.IsRedoDone = true;
            Console.Write("[");
            foreach(var element in memory.HistoryList)
                Console.Write(element.Path);
            Console.Write("]");
            Console.Write("Index " + memory.Index);
        }
    }

    /// <inheritdoc cref="IExplorerHistory.Undo"/>
    public void Undo()
    {
        lock (_lock)
        {
            if (!CanUndo)
            {
                logger.Fatal(CannotUndoLogMessage, memory.Index, memory.HistoryList.Count());
                throw new CannotUndoException(CannotUndoExceptionText);
            }
            memory.Index--;
            Console.Write("Elements: [");
            foreach(var element in memory.HistoryList)
                Console.Write(element.Path + ", ");
            Console.Write("]\n");
            Console.WriteLine("Index " + memory.Index); 
        }
    }

    /// <inheritdoc cref="List{T}.GetEnumerator"/>
    public IEnumerator<FileEntity> GetEnumerator() 
        => memory.HistoryList.GetEnumerator();

    /// <inheritdoc cref="List{T}.GetEnumerator"/>
    IEnumerator IEnumerable.GetEnumerator() 
        => GetEnumerator();
    
}