using System.Globalization;
using System.IO;
using System.Windows.Data;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialZip.Model.Entities;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace MaterialZip.Convertors;

public class FileDataGridEntityToFileEntityConvertor : IValueConverter
{
    private const string CannotFindDirectory = "Directory {path} was gotten but not exists";
    private const string HandledExceptionLogMessage = "Exceptions {exceptions} {text} was handled";
    public static FileDataGridEntityToFileEntityConvertor Instance { get; } = new();
    
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo? culture)
    {
        
        if (value is not FileDataGridEntity entity)
            return null;
        
        var isDirectory = Directory.Exists(entity.Path);
        return new FileEntity(entity.Path, isDirectory);
    }

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

            var fileDataGridEntity = new FileDataGridEntity(
                Name: info.Name,
                Size: entity.IsDirectory ? null : (info as FileInfo)?.Length,
                LastChanging: info.LastWriteTime.ToLocalTime(),
                CreatedAt: info.CreationTime.ToLocalTime(),
                entity.Path
            );
            return fileDataGridEntity; 
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
        {
            Ioc.Default.GetRequiredService<ILogger>().Warning(HandledExceptionLogMessage, ex, ex.Message);
            return null;
        }
        
    }
}