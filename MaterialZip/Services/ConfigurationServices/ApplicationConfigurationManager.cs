using System.Globalization;
using MaterialZip.Model.Enums;
using MaterialZip.Options;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Options;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Represent a default application proxy between <see cref="IOptionsSnapshot{ApplicationOptions}"/> and <see cref="Services"/> for testability
/// </summary>
/// <param name="options"><see cref="IOptionsSnapshot{ApplicationOptions}"/> instance</param>
public sealed class ApplicationConfigurationManager(IOptionsSnapshot<ApplicationOptions> options)
    : IApplicationConfigurationManager
{
    /// <inheritdoc cref="IApplicationConfigurationManager.Language"/>
    public required CultureInfo Language
    {
        get => options.Value.Language;
        set => options.Value.Language = value;
    }
    
    /// <inheritdoc cref="IApplicationConfigurationManager.Color"/>
    public required MaterialColor Color
    {
        get => options.Value.Color;
        set => options.Value.Color = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.HoverColorHex"/>
    public string HoverColorHex  
    { 
        get => options.Value.HoverColorHex;
        set => options.Value.HoverColorHex = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.GitHubSourceLink"/>
    public required string GitHubSourceLink 
    { 
        get => options.Value.GitHubSourceLink;
        set => options.Value.GitHubSourceLink = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.LastDirectory"/>
    public required string LastDirectory
    {
        get => options.Value.LastDirectory;
        set => options.Value.LastDirectory = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManagers.WindowsExplorerPathInWindows"/>
    public string WindowsExplorerPathInWindows
    {
        get => options.Value.WindowsExplorerPathInWindows; 
        set => options.Value.WindowsExplorerPathInWindows = value;
    }
    
    /// <inheritdoc cref="IApplicationConfigurationManager.LogsPath"/>
    public string LogsPath 
    {  
        get => options.Value.LogsPath;
        set => options.Value.LogsPath = value; 
    }
}