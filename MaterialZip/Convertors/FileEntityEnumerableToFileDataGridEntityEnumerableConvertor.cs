using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialZip.Model.Entities;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace MaterialZip.Convertors;

/// <summary>
/// Converts between collections of <see cref="FileEntity"/> and <see cref="FileDataGridEntity"/> types.
/// Implements <see cref="IValueConverter"/> for use in data bindings with enumerable collections.
/// </summary>
/// <remarks>
/// This converter handles batch conversion of file system entities, including error handling and logging.
/// Provides a static instance via <see cref="Instance"/> property for reuse.
/// </remarks>
[ValueConversion(typeof(IEnumerable<FileEntity>), typeof(IEnumerable<FileDataGridEntity>))]
public sealed class FileEntityEnumerableToFileDataGridEntityEnumerableConvertor : IValueConverter
{
    
    private const string CannotFindDirectory = "Directory {path} was gotten but not exists";
    private const string HandledExceptionLogMessage = "Exceptions {exceptions} {text} was handled";
    
    /// <summary>
    /// Gets the singleton instance of this converter.
    /// </summary>
    public static FileEntityEnumerableToFileDataGridEntityEnumerableConvertor Instance { get; } = new();
    
    /// <summary>
    /// Converts a collection of <see cref="FileEntity"/> to a collection of <see cref="FileDataGridEntity"/>.
    /// </summary>
    /// <param name="value">The enumerable collection to convert (expected to be <see cref="IEnumerable{FileEntity}"/>).</param>
    /// <param name="targetType">The target type (expected to be <see cref="IEnumerable{FileDataGridEntity}"/>).</param>
    /// <param name="parameter">Optional converter parameter.</param>
    /// <param name="culture">The culture information for conversion.</param>
    /// <returns>
    /// A new <see cref="List{FileDataGridEntity}"/> containing successfully converted items or null if:
    /// - The input value is not an <see cref="IEnumerable{FileEntity}"/>
    /// </returns>
    /// <remarks>
    /// Skips items that:
    /// - Don't exist in the file system
    /// - Cause IO-related exceptions
    /// Logs errors and warnings through <see cref="Serilog.ILogger"/>.
    /// </remarks>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IEnumerable<FileEntity> entities)
            return null;

        var result = new List<FileDataGridEntity>(entities.Count());
        
        foreach (var entity in entities)
        {
            try
            {
                FileSystemInfo info = entity.IsDirectory
                    ? new DirectoryInfo(entity.Path)
                    : new FileInfo(entity.Path);

                if (!info.Exists)
                {
                    Ioc.Default
                        .GetRequiredService<ILogger<FileEntityEnumerableToFileDataGridEntityEnumerableConvertor>>()
                        .LogError(CannotFindDirectory, info.FullName);
                    continue;
                }

                var fileDataGridEntity = new FileDataGridEntity(
                    Name: info.Name,
                    Size: entity.IsDirectory ? null : (info as FileInfo)?.Length,
                    LastChanging: info.LastWriteTime.ToLocalTime(),
                    CreatedAt: info.CreationTime.ToLocalTime(),
                    entity.Path
                );
                
                result.Add(fileDataGridEntity);
            }
            catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
            {
                Ioc.Default
                    .GetRequiredService<ILogger<FileEntityEnumerableToFileDataGridEntityEnumerableConvertor>>()
                    .LogWarning(HandledExceptionLogMessage, ex, ex.Message);
            }
        }
        return result;
    }
    
    /// <summary>
    /// Converts a collection of <see cref="FileDataGridEntity"/> back to <see cref="FileEntity"/> collection.
    /// </summary>
    /// <param name="value">The enumerable collection to convert back (expected to be <see cref="IEnumerable{FileDataGridEntity}"/>).</param>
    /// <param name="targetType">The target type (expected to be <see cref="IEnumerable{FileEntity}"/>).</param>
    /// <param name="parameter">Optional converter parameter.</param>
    /// <param name="culture">The culture information for conversion.</param>
    /// <returns>
    /// A new <see cref="List{FileEntity}"/> or null if:
    /// - The input value is not an <see cref="IEnumerable{FileDataGridEntity}"/>
    /// </returns>
    /// <remarks>
    /// This conversion is simpler than the forward conversion as it only requires path existence checks.
    /// </remarks>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not IEnumerable<FileDataGridEntity> entities)
            return null;

        var result = new List<FileEntity>(entities.Count());
        
        foreach (var fileDataGridEntity in entities)
        {
            var isDirectory = Directory.Exists(fileDataGridEntity.Path);
            result.Add(new FileEntity(fileDataGridEntity.Path, isDirectory));
        }
        return result;
    }
}