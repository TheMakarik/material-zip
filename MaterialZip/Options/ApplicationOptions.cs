using System.Globalization;
using System.Text.Json.Serialization;
using MaterialZip.Model.Enums;
using MaterialZip.Options.Abstractions;

namespace MaterialZip.Options;

/// <summary>
/// Represent Application configuration instance 
/// </summary>
public class ApplicationOptions : IApplicationOptions
{
    /// <inheritdoc cref="IApplicationOptions.Language"/>
    public required CultureInfo Language { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.Theme"/>
    public MaterialTheme Theme { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.PrimaryColor"/>
    public MaterialColor PrimaryColor { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.SecondaryColor"/>
    public MaterialColor SecondaryColor { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.LastDirectory"/>
    public required string LastDirectory { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.ResourcesLocation"/>
    public required string ResourcesLocation { get; set; }
}