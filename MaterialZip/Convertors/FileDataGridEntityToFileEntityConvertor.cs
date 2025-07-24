using System.Globalization;
using System.IO;
using System.Windows.Data;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialZip.Model.Entities;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MaterialZip.Convertors;

/// <summary>
/// Converts between <see cref="FileDataGridEntity"/> and <see cref="FileEntity"/> types.
/// Implements <see cref="IValueConverter"/> for use in data bindings.
/// </summary>
/// <remarks>
/// This converter handles both directory and file paths, checking their existence in the file system.
/// Provides a static instance via <see cref="Instance"/> property for reuse.
/// </remarks>
[ValueConversion(typeof(FileDataGridEntity), typeof(FileEntity))]
public class FileDataGridEntityToFileEntityConvertor : IValueConverter
{

    private const string CannotFindDirectory = "Directory {path} was gotten but not exists";
    private const string HandledExceptionLogMessage = "Exceptions {exceptions} {text} was handled";
    
    /// <summary>
    /// Gets the singleton instance of this converter.
    /// </summary>
    public static FileDataGridEntityToFileEntityConvertor Instance { get; } = new();
    
    /// <summary>
    /// Converts a <see cref="FileDataGridEntity"/> to a <see cref="FileEntity"/>.
    /// </summary>
    /// <param name="value">The value to convert (expected to be <see cref="FileDataGridEntity"/>).</param>
    /// <param name="targetType">The target type (expected to be <see cref="FileEntity"/>).</param>
    /// <param name="parameter">Optional converter parameter.</param>
    /// <param name="culture">The culture information for conversion.</param>
    /// <returns>
    /// A new <see cref="FileEntity"/> instance or null if:
    /// - The input value is not a <see cref="FileDataGridEntity"/>
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value is not FileDataGridEntity entity)
            return null;
        
        var isDirectory = Directory.Exists(entity.Path);
        return new FileEntity(entity.Path, isDirectory);
    }

    /// <summary>
    /// Converts a <see cref="FileEntity"/> back to a <see cref="FileDataGridEntity"/>.
    /// </summary>
    /// <param name="value">The value to convert back (expected to be <see cref="FileEntity"/>).</param>
    /// <param name="targetType">The target type (expected to be <see cref="FileDataGridEntity"/>).</param>
    /// <param name="parameter">Optional converter parameter.</param>
    /// <param name="culture">The culture information for conversion.</param>
    /// <returns>
    /// A new <see cref="FileDataGridEntity"/> instance or null if:
    /// - The input value is not a <see cref="FileEntity"/>
    /// - The file/directory doesn't exist
    /// - An <see cref="IOException"/> or <see cref="UnauthorizedAccessException"/> occurs
    /// </returns>
    /// <exception cref="IOException">Thrown when file system access fails.</exception>
    /// <exception cref="UnauthorizedAccessException">Thrown when lacking permissions.</exception>
    /// <remarks>
    /// Logs warnings through <see cref="ILogger"/> when exceptions are handled.
    /// </remarks>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        if (value is not FileEntity entity)
            return null;
        
        try
        {
            FileSystemInfo info = entity.IsDirectory
                ? new DirectoryInfo(entity.Path)
                : new FileInfo(entity.Path);

            if (!info.Exists)
            {
                Ioc.Default.GetRequiredService<ILogger>().Error(CannotFindDirectory, info.FullName);
                return null;
            }

            return new FileDataGridEntity(
                Name: info.Name,
                Size: entity.IsDirectory ? null : (info as FileInfo)?.Length,
                LastChanging: info.LastWriteTime.ToLocalTime(),
                CreatedAt: info.CreationTime.ToLocalTime(),
                entity.Path
            );
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Ioc.Default.GetRequiredService<ILogger>().Warning(HandledExceptionLogMessage, ex, ex.Message);
            return null;
        }
    }
}