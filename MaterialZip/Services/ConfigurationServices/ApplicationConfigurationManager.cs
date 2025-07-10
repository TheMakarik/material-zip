using System.Globalization;
using MaterialZip.Model.Enums;
using MaterialZip.Options;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Microsoft.Extensions.Options;

namespace MaterialZip.Services.ConfigurationServices;

public class ApplicationConfigurationManager(IOptionsSnapshot<ApplicationOptions> options)
    : IApplicationConfigurationManager
{
    public required CultureInfo Language
    {
        get => options.Value.Language;
        set => options.Value.Language = value;
    }

    public required MaterialTheme Theme
    {
        get => options.Value.Theme;
        set => options.Value.Theme = value;
    }

    public required MaterialColor PrimaryColor
    {
        get => options.Value.PrimaryColor;
        set => options.Value.PrimaryColor = value;
    }

    public required MaterialColor SecondaryColor
    {
        get => options.Value.SecondaryColor;
        set => options.Value.SecondaryColor = value;
    }

    public required string LastDirectory
    {
        get => options.Value.LastDirectory;
        set => options.Value.LastDirectory = value;
    }

    public required string ResourcesLocation
    {
        get => options.Value.ResourcesLocation;
        set => options.Value.ResourcesLocation = value;
    }
}