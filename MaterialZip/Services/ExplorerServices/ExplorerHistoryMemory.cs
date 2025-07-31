using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ExplorerServices.Abstractions;
using MaterialZip.View.Controls;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MaterialZip.Services.ExplorerServices;

/// <summary>
/// Default <see cref="ExplorerHistory"/> memory saver
/// </summary>
/// <param name="logger"><see cref="Serilog"/> logger </param>
/// <remarks>To add a new directory to history do <see cref="CurrentDirectory"/> = {yourValue} to get just get property <see cref="CurrentDirectory"/> </remarks>
public sealed class ExplorerHistoryMemory(ILogger<ExplorerHistoryMemory> logger) : IExplorerHistoryMemory
{
    private const string GettingACurrentEntityLogMessage
        = "Succed loading {path} from history";
    private const string AddingANewDirectoryToHistoryLogMessage
        = "Added a new directory to history: {path}";
    private const string TryingGetDirectoryFromEmptyHistoryLogMessage
        = "Tried to get last directory from empty history, program will fall";
    private const string TryingToGetValueFromEmptyHistoryExceptionText
        = "Trying to get value from empty history collection, possible forgotten adding first element or validation"; 
    
    private List<FileEntity> _historyList = new List<FileEntity>(10);
    private bool _wasFirstAddingDid;
    
    /// <inheritdoc cref=" IExplorerHistoryMemory.HistoryList"/>
    public IEnumerable<FileEntity> HistoryList
    {
        get => _historyList;
        private set => _historyList = value.ToList();
    }

    /// <inheritdoc cref="IExplorerHistoryMemory.Index"/>
    public int Index { get; set; } = -0;
    
    /// <inheritdoc cref="IExplorerHistoryMemory.CurrentDirectory"/>
    public FileEntity CurrentDirectory { get => GetDirectory(); set => AddDirectory(value); }
    
    public bool IsRedoDone { get; set; }
    
    private FileEntity GetDirectory()
    {
        ThrowExceptionIfCannotGetDirectory();
        return GetDirectoryIgnoringElementsExisting();
    }

    private void ThrowExceptionIfCannotGetDirectory()
    {
        if (ElementsExist())
            return;
        
        logger.LogCritical(TryingGetDirectoryFromEmptyHistoryLogMessage);
        throw new EmptyHistoryException(TryingToGetValueFromEmptyHistoryExceptionText);
    }

    private FileEntity GetDirectoryIgnoringElementsExisting()
    {
        var directory = HistoryList.ElementAt(Index);
        logger.LogDebug(GettingACurrentEntityLogMessage, directory.Path );
        return directory;
    }

    private void AddDirectory(FileEntity directory)
    {
        _historyList.Add(directory);
        logger.LogDebug(AddingANewDirectoryToHistoryLogMessage, directory.Path);
        if (_wasFirstAddingDid)
            Index++;
        else
            _wasFirstAddingDid = true;
        CutHistoryListIfRedoIsDid();
    }

    private void CutHistoryListIfRedoIsDid()
    {
        if (IsRedoDone)
            HistoryList = HistoryList.Take(Index);
        IsRedoDone = false;
    }
    
    private bool ElementsExist() => _wasFirstAddingDid;
}