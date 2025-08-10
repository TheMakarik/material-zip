using System.Globalization;
using System.Runtime.CompilerServices;
using MaterialZip.Model.Enums;
using MaterialZip.Options;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Represent a default application proxy between <see cref="IOptionsSnapshot{ApplicationOptions}"/> and <see cref="Services"/> for testability
/// </summary>
/// <param name="options"><see cref="IOptionsSnapshot{ApplicationOptions}"/> instance</param>
public sealed class ApplicationConfigurationManager(IOptionsMonitor<ApplicationOptions> options, ILogger<ApplicationConfigurationManager> logger)
    : IApplicationConfigurationManager
{
    private const string PropertyWasLoadedFromConfiguration
        = "{property} was loaded from configuration with value {value}";
    
    /// <inheritdoc cref="IApplicationConfigurationManager.Language"/>
    public required CultureInfo Language
    {
        get => Get(options.CurrentValue.Language);
        set => options.CurrentValue.Language = value;
    }
    
    /// <inheritdoc cref="IApplicationConfigurationManager.Color"/>
    public required MaterialColor Color
    {
        get => Get(options.CurrentValue.Color);
        set => options.CurrentValue.Color = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.HoverColorHex"/>
    public string HoverColorHex  
    { 
        get => Get(options.CurrentValue.HoverColorHex);
        set => options.CurrentValue.HoverColorHex = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.GitHubSourceLink"/>
    public required string GitHubSourceLink 
    { 
        get => Get(options.CurrentValue.GitHubSourceLink);
        set => options.CurrentValue.GitHubSourceLink = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.LastDirectory"/>
    public required string LastDirectory
    {
        get => Get(options.CurrentValue.LastDirectory);
        set => options.CurrentValue.LastDirectory = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.WindowsExplorerPathInWindows"/>
    public string WindowsExplorerPathInWindows
    {
        get => Get(options.CurrentValue.WindowsExplorerPathInWindows); 
        set => options.CurrentValue.WindowsExplorerPathInWindows = value;
    }
    
    /// <inheritdoc cref="IApplicationConfigurationManager.LogsPath"/>
    public string LogsPath 
    {  
        get => Get(options.CurrentValue.LogsPath);
        set => options.CurrentValue.LogsPath = value; 
    }

    private T Get<T>(T value, [CallerMemberName] string? nameofValue = null)
    {
        logger.LogInformation(PropertyWasLoadedFromConfiguration, nameofValue, value);
        return value;
    }
}