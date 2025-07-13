using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
using MaterialZip.Model.Extensions;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Serilog;

namespace MaterialZip.Services.ExplorerServices;

/// <summary>
/// Default <see cref="ExplorerHistory"/> memory saver
/// </summary>
/// <param name="logger"><see cref="Serilog"/> logger </param>
/// <remarks>To add a new directory to history do <see cref="CurrentDirectory"/> = {yourValue} to get just get property <see cref="CurrentDirectory"/> </remarks>
public sealed class ExplorerHistoryMemory(ILogger logger) : IExplorerHistoryMemory
{
    private const string GettingACurrentEntityLogMessage
        = "Succed loading {path} from history";
    private const string AddingANewDirectoryToHistoryLogMessage
        = "Added a new directory to history: {path}";
    private const string TryingGetDirectoryFromEmptyHistoryLogMessage
        = "Tried to get last directory from empty history, program will fall";

    private const string TryingToGetValueFromEmptyHistoryExceptionText
        = "Trying to get value from empty history collection, possible forgotten adding first element or validation"; 
   
    /// <inheritdoc cref=" IExplorerHistoryMemory.HistoryList"/>
    public List<FileEntity> HistoryList { get; private set; } = new List<FileEntity>(10);
  
    /// <inheritdoc cref="IExplorerHistoryMemory.Index"/>
    public int Index { get; set; } = -1;
    
    /// <inheritdoc cref="IExplorerHistoryMemory.CurrentDirectory"/>
    public FileEntity CurrentDirectory { get => GetDirectory(); set => AddDirectory(value); }
    
    private FileEntity GetDirectory()
    {
        ThrowExceptionIfCannotGetDirectory();
        return GetDirectoryIgnoringElementsExisting();
    }

    private void ThrowExceptionIfCannotGetDirectory()
    {
        if (ElementsDoNotExist())
        {
            logger.Fatal(TryingGetDirectoryFromEmptyHistoryLogMessage);
            throw new EmptyHistoryException(TryingToGetValueFromEmptyHistoryExceptionText);
        }
    }

    private FileEntity GetDirectoryIgnoringElementsExisting()
    {
        var directory = HistoryList[Index];
        logger.Debug(GettingACurrentEntityLogMessage, directory.Path );
        return directory;
    }

    private void AddDirectory(FileEntity directory)
    {
        CutHistoryListIfRedoIsDid();
        HistoryList.Add(directory);
        logger.Debug(AddingANewDirectoryToHistoryLogMessage, directory.Path);
        Index++;
    }

    private void CutHistoryListIfRedoIsDid()
    {
        if (IsRedoDone)
            HistoryList = HistoryList.SubList(0, Index);
    }
    
    private bool ElementsDoNotExist() => Index == -1;
    private bool IsRedoDone => Index + 1 != HistoryList.Count;


}