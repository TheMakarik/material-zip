using System.Collections.ObjectModel;
using MaterialZip.Model.Entities;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Serilog;

namespace MaterialZip.Services.ExplorerServices;

/// <summary>
/// Represent the default application <see cref="IExplorer"/> interface implementation with logging using <see cref="ILogger"/> and proxy pattern implementation of <see cref="System.IO.Directory"/>
/// </summary>
/// <param name="lowLevelExplorer">proxy pattern implementation of <see cref="System.IO.Directory"/>, <see cref="ILowLevelExplorer"/> implementation</param>
/// <param name="logger"><see cref="ILogger"/> instance</param>

public sealed class Explorer(ILowLevelExplorer lowLevelExplorer, ILogger logger) : IExplorer
{
    private const string GetLogicalDrivesSucceedLogMessage = "Succesefully got  logical drives from computer: {drives}";
    private const string GetDirectoryContentSucceedLogMessage = "Succesefully got {directory} content";
    private const string TriedToGetFileContentLogMessage = "Tried to get file's content from {path}, emplty collection returned and possible errors may happen";
    
    /// <inheritdoc cref="IExplorer.GetLogicalDrives"/>
    public IReadOnlyCollection<FileEntity> GetLogicalDrives()
    {
        var drives = lowLevelExplorer.GetLogicalDrives(); 
        var entities = ToFileEntity(drives, isDirectory: true);
        logger.Debug(GetLogicalDrivesSucceedLogMessage, drives);
        return new ReadOnlyCollection<FileEntity>(entities.ToList());
    }

    /// <inheritdoc cref="IExplorer.GetDirectoryContent"/>
    public IReadOnlyCollection<FileEntity> GetDirectoryContent(FileEntity directory)
    {
        if (!directory.IsDirectory)
        {
            logger.Warning(TriedToGetFileContentLogMessage, directory.Path);
            return new ReadOnlyCollection<FileEntity>(Enumerable.Empty<FileEntity>().ToList());
        }
        
        logger.Debug(GetDirectoryContentSucceedLogMessage, directory.Path);
        var entities = GetContent(directory.Path);
        return new ReadOnlyCollection<FileEntity>(entities.ToList());
    }

    private IEnumerable<FileEntity> ToFileEntity(string[] paths, bool isDirectory)
    {
        return paths.Select(p => new FileEntity(p, isDirectory)).ToArray(); 
    }

    private IEnumerable<FileEntity> GetContent(string path)
    {
        var directories = lowLevelExplorer.GetDirectories(path);
        var files = lowLevelExplorer.GetFiles(path);
        return ToFileEntity(directories, isDirectory: true)
            .Concat(ToFileEntity(files, isDirectory: false));
    }
}