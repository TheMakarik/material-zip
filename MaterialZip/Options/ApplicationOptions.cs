using System.Globalization;
using MaterialZip.Model.Enums;
using MaterialZip.Options.Abstractions;

namespace MaterialZip.Options;

public class ApplicationOptions : IApplicationOptions
{
    public required CultureInfo Language { get; set; }
    public MaterialTheme Theme { get; set; }
    public MaterialColor PrimaryColor { get; set; }
    public MaterialColor SecondaryColor { get; set; }
    public required string LastDirectory { get; set; }
    public required string ResourcesLocation { get; set; }
}