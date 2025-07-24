using System.Collections.ObjectModel;
using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
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
    private const string TriedToGetFileContentLogMessage = "Tried to get file's content from {path}, exception was thrown";
    private const string CannotGetContentOfTheFileExceptionText = "Cannot get content of the file {0}";
    
    /// <inheritdoc cref="IExplorer.GetLogicalDrivesAsync"/>
    public async Task<IEnumerable<FileEntity>> GetLogicalDrivesAsync()
    {
        return await Task.Run(() =>
        {
            var drives = lowLevelExplorer.GetLogicalDrives();
            var entities = ToFileEntity(drives, isDirectory: true);
            logger.Debug(GetLogicalDrivesSucceedLogMessage, drives);
            return entities.ToList();
        });
    }
    
    /// <inheritdoc cref="IExplorer.GetDirectoryContentAsync"/>
    public async Task<IEnumerable<FileEntity>> GetDirectoryContentAsync(FileEntity directory)
    {
        if (!directory.IsDirectory)
        {
            logger.Error(TriedToGetFileContentLogMessage, directory.Path);
            throw new CannotGetFileContentException(string.Format(CannotGetContentOfTheFileExceptionText, directory.Path));
        }
        
        logger.Debug(GetDirectoryContentSucceedLogMessage, directory.Path);
        var entities = await GetContent(directory.Path);
        return entities;
    }

    private IEnumerable<FileEntity> ToFileEntity(IEnumerable<string> paths, bool isDirectory)
    {
        return paths.Select(p => new FileEntity(p, isDirectory)).ToArray(); 
    }

    private async Task<IEnumerable<FileEntity>> GetContent(string path)
    {
        return await Task.Run(() =>
        {
            var directories = lowLevelExplorer.EnumerateDirectories(path);
            var files = lowLevelExplorer.EnumerateFiles(path);
            return ToFileEntity(directories, isDirectory: true)
                .Concat(ToFileEntity(files, isDirectory: false));
        });
    }
}