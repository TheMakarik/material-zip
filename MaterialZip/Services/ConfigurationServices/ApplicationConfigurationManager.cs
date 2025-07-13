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

    /// <inheritdoc cref="IApplicationConfigurationManager.Theme"/>
    public required MaterialTheme Theme
    {
        get => options.Value.Theme;
        set => options.Value.Theme = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.PrimaryColor"/>
    public required MaterialColor PrimaryColor
    {
        get => options.Value.PrimaryColor;
        set => options.Value.PrimaryColor = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.SecondaryColor"/>
    public required MaterialColor SecondaryColor
    {
        get => options.Value.SecondaryColor;
        set => options.Value.SecondaryColor = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.LastDirectory"/>
    public required string LastDirectory
    {
        get => options.Value.LastDirectory;
        set => options.Value.LastDirectory = value;
    }

    /// <inheritdoc cref="IApplicationConfigurationManager.ResourcesLocation"/>
    public required string ResourcesLocation
    {
        get => options.Value.ResourcesLocation;
        set => options.Value.ResourcesLocation = value;
    }
}