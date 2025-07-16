using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Serilog;

namespace MaterialZip.Services.ConfigurationServices;

public class LastDirectoryBuffer(ILogger logger) : ILastDirectoryBuffer
{
    private const string DirectoryInBufferNotFoundExceptionText 
        = "Cannot find directory in buffer, possible forgotten validation";
    private const string DirectoryWasLoadedToTheBufferMessage 
        = "{path} was loaded to the buffer replacing {oldPath}";
    private const string DirectoryWasGottenFromBufferLogMessage
        = "{path} was gotten from the buffer";
    
    private string? _buffer;

    /// <inheritdoc cref="ILastDirectoryBuffer.IsBufferEmpty"/>
    public bool IsBufferEmpty => _buffer is null;
    
    /// <inheritdoc cref="ILastDirectoryBuffer.ToBuffer"/>
    public void ToBuffer(string directoryPath)
    {
        logger.Debug(DirectoryWasLoadedToTheBufferMessage, directoryPath, _buffer ?? "/");
        _buffer = directoryPath;
    }
    
    /// <inheritdoc cref="ILastDirectoryBuffer.FromBuffer"/>
    public string FromBuffer()
    {
        if (IsBufferEmpty)
            throw new DirectoryInBufferNotFoundException(DirectoryInBufferNotFoundExceptionText);
        #pragma warning disable
        logger.Information(DirectoryWasGottenFromBufferLogMessage, _buffer);
        return _buffer;
        #pragma warning restore
    }
}