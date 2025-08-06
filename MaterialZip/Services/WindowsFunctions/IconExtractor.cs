using System.Drawing;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MaterialZip.Model.Entities;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog;
using Image = System.Windows.Controls.Image;

namespace MaterialZip.Services.WindowsFunctions;


/// <summary>
/// Provides functionality to extract icons from file system paths and convert them to bitmap sources
/// </summary>
/// <param name="logger">The logger instance for error reporting</param>
/// <param name="extractor">The associated icon extractor service</param>
/// <param name="bitmapSourceBuilder">The bitmap source builder service</param>
public sealed class IconExtractor(
    ILogger<IconExtractor> logger,
    IAssociatedIconExtractor extractor,
    IBitmapSourceBuilder bitmapSourceBuilder) : IIconExtractor
{
    private const string FailedToExtractIconLogMessage = "Failed to extract icon for path: {Path}";
    private const string ExceptionOccuredLogMessage = "Error extracting icon for path: {Path}";
    
    /// <inheritdoc cref="IIconExtractor.FromPath"/>
    public BitmapSource? FromPath(string path)
    {
        try
        {
            var icon = extractor.Extract(path);
            if (icon is null)
            {
                logger.LogWarning(FailedToExtractIconLogMessage, path);
                return null;
            }
            
            return bitmapSourceBuilder.Build(icon);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ExceptionOccuredLogMessage, path);
            return null;
        }
    }
}