using System.Globalization;
using MaterialZip.Model.Enums;
using MaterialZip.Options.Abstractions;
using Microsoft.Extensions.Options;

namespace MaterialZip.Services.ConfigurationServices.Abstractions;

/// <summary>
/// Represent default abstraction of the <see cref="IOptions{ApplicationOptions}"/> for testability improving
/// </summary>
public interface IApplicationConfigurationManager 
{
    /// <inheritdoc cref="IApplicationOptions.Language"/>
    public CultureInfo Language { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.Theme"/>
    public MaterialTheme Theme { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.PrimaryColor"/>
    public MaterialColor PrimaryColor { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.SecondaryColor"/>
    public MaterialColor SecondaryColor { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.LastDirectory"/>
    public string LastDirectory { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.ResourcesLocation"/>
    public string ResourcesLocation { get; set; }
}