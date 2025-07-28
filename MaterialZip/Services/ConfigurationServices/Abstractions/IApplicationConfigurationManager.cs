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
    
    /// <inheritdoc cref="IApplicationOptions.olor"/>
    public MaterialColor Color { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.HoverColorHex"/>
    public string HoverColorHex { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.GitHubSourceLink"/>
    public string GitHubSourceLink { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.LastDirectory"/>
    public string LastDirectory { get; set; }
    
    /// <inheritdoc cref="IApplicationOptions.ResourcesLocation"/>
    public string ResourcesLocation { get; set; }
}