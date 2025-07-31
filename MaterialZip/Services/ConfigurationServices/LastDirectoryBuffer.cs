using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog;

namespace MaterialZip.Services.ConfigurationServices;

public class LastDirectoryBuffer(ILogger<LastDirectoryBuffer> logger) : ILastDirectoryBuffer
{
    private const string DirectoryInBufferNotFoundExceptionText 
        = "Cannot find directory in buffer, possible forgotten validation";
    private const string DirectoryWasLoadedToTheBufferMessage 
        = "{path} was loaded to the buffer replacing {oldPath}";
    private const string DirectoryWasGottenFromBufferLogMessage
        = "{path} was gotten from the buffer";
    private const string DefaultPath 
        = "...";
    
    private FileEntity? _buffer;

    /// <inheritdoc cref="ILastDirectoryBuffer.IsBufferEmpty"/>
    public bool IsBufferEmpty => _buffer is null;
    
    /// <inheritdoc cref="ILastDirectoryBuffer.ToBuffer"/>
    public void ToBuffer(FileEntity directory)
    {
        logger.LogDebug(DirectoryWasLoadedToTheBufferMessage, directory.Path, 
            _buffer.GetValueOrDefault(new FileEntity(DefaultPath, true)).Path);
        _buffer = directory;
    }
    
    /// <inheritdoc cref="ILastDirectoryBuffer.FromBuffer"/>
    public FileEntity FromBuffer()
    {
        if (IsBufferEmpty)
            throw new DirectoryInBufferNotFoundException(DirectoryInBufferNotFoundExceptionText);
        #pragma warning disable
        logger.LogInformation(DirectoryWasGottenFromBufferLogMessage, _buffer.Value.Path);
        return _buffer.Value;
        #pragma warning restore
    }
}