using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialZip.Model.Entities;
using Serilog;

namespace MaterialZip.Convertors;

/// <summary>
/// Converts a <see cref="FileEntity"/> to a <see cref="FileDataGridEntity"/> for display purposes.
/// </summary>
[ValueConversion(typeof(IEnumerable<FileEntity>), typeof(IEnumerable<FileDataGridEntity>))]
public class FileEntityEnumerableToFileDataGridEntityEnumerableConvertor : IValueConverter
{

    private const string CannotFindDirectory = "Directory {path} was gotten but not exists";
    private const string HandledExceptionLogMessage = "Exceptions {exceptions} {text} was handled"; 
    
    /// <summary>
    /// Convertor instance 
    /// </summary>
    public static FileEntityEnumerableToFileDataGridEntityEnumerableConvertor Instance { get; } = new();
    
    /// <summary>
    /// Converts a <see cref="FileEntity"/>  Read only Collection to a display-ready <see cref="FileDataGridEntity"/>  Collection.
    /// </summary>
    /// <param name="value">The source <see cref="FileEntity"/>  Read only  Collection to convert</param>
    /// <param name="targetType">Not used (required by interface)</param>
    /// <param name="parameter">Not used (required by interface)</param>
    /// <param name="culture">Not used (required by interface)</param>
    /// <returns>
    /// A new <see cref="FileDataGridEntity"/>  Collection containing file information,
    /// or null if conversion fails
    /// </returns>
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
                        Ioc.Default.GetRequiredService<ILogger>().Error(CannotFindDirectory, info.FullName);
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
                    Ioc.Default.GetRequiredService<ILogger>().Warning(HandledExceptionLogMessage, ex, ex.Message);
                }
            }
            return result;
    }
    
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