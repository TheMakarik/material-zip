using System.Globalization;
using MaterialZip.Model.Enums;

namespace MaterialZip.Options.Abstractions;

public interface IApplicationOptions
{
    public CultureInfo Language { get; set; }
    public MaterialTheme Theme { get; set; }
    public MaterialColor PrimaryColor { get; set; }
    public MaterialColor SecondaryColor { get; set; }
    public string LastDirectory { get; set; }
    public string ResourcesLocation { get; set; }
}