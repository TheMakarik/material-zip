using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using CommunityToolkit.Mvvm.DependencyInjection;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace MaterialZip.Convertors;

/// <summary>
/// Converts file system paths to appropriate image sources for display in UI.
/// </summary>
/// <remarks>
/// Handles both files and directories, using different icons for each type.
/// The converter uses <see cref="IIconExtractor"/> service for file icons.
/// </remarks>
[ValueConversion(typeof(string), typeof(ImageSource))]
public class PathToImageSourceConvertor : IValueConverter
{
    private const string FolderIconPath = "pack://application:,,,/Assets/FileIcons/folder.png";
    private const string DriveIconPath = "pack://application:,,,/Assets/FileIcons/harddisk.png";
    
    private const string FileNotFoundLogMessage = "File not found at path: {Path}";
    private const string IconExtractionFailedLogMessage = "Failed to extract icon for path: {Path}. Error: {Error}";
    private const string NullInputLogMessage = "Null input received in PathToImageSourceConvertor";
    
    private const string ConvertBackNotSupportedMessage = "ConvertBack is not supported for PathToImageSourceConvertor";
    
    /// <summary>
    /// Singleton instance of the converter.
    /// </summary>
    public static PathToImageSourceConvertor Instance { get; } = new();
    
    private readonly ILogger<PathToImageSourceConvertor> _logger;

    public PathToImageSourceConvertor()
    {
        _logger = Ioc.Default.GetRequiredService<ILogger<PathToImageSourceConvertor>>();
    }

    /// <summary>
    /// Converts a file system path to an appropriate ImageSource.
    /// </summary>
    /// <param name="value">The path string to convert.</param>
    /// <param name="targetType">The target type (expected to be ImageSource).</param>
    /// <param name="parameter">Optional converter parameter.</param>
    /// <param name="culture">The culture information for conversion.</param>
    /// <returns>
    /// An ImageSource representing:
    /// - Folder icon for directories
    /// - Drive icon for root paths
    /// - File icon for files (via IIconExtractor)
    /// - null for invalid paths
    /// </returns>
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is not string path)
        {
            _logger.LogWarning(NullInputLogMessage);
            return null;
        }

        try
        {
            if (Directory.Exists(path))
            {
                return IsDirectory(path) 
                    ? new BitmapImage(new Uri(FolderIconPath)) 
                    : new BitmapImage(new Uri(DriveIconPath));
            }

            if (!File.Exists(path))
            {
                _logger.LogWarning(FileNotFoundLogMessage, path);
                return null;
            }

            return Ioc.Default.GetRequiredService<IIconExtractor>().FromPath(path);
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException or UriFormatException)
        {
            _logger.LogError(ex, IconExtractionFailedLogMessage, path, ex.Message);
            return null;
        }
    }

    /// <summary>
    /// This conversion is not supported and will always throw an exception.
    /// </summary>
    /// <exception cref="NotSupportedException">Always thrown when this method is called.</exception>
    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        _logger.LogError(ConvertBackNotSupportedMessage);
        throw new NotSupportedException(ConvertBackNotSupportedMessage);
    }
    
    private bool IsDirectory(string path) => path.Length > 3;
}