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
    
    /// <inheritdoc cref="IApplicationOptions.Color"/>
    public MaterialColor Color { get; set; }

    /// <inheritdoc cref="IApplicationOptions.GitHubSourceLink"/>
    public required string GitHubSourceLink { get; set; }

    /// <inheritdoc cref="IApplicationOptions.HoverColorHex"/>
    public required string HoverColorHex { get; set; }

    /// <inheritdoc cref="IApplicationOptions.LastDirectory"/>
    public required string LastDirectory { get; set; }

}