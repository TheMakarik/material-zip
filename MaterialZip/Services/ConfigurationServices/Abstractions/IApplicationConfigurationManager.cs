using System.Globalization;
using MaterialZip.Model.Enums;

namespace MaterialZip.Services.ConfigurationServices.Abstractions;

public interface IApplicationConfigurationManager 
{
    public CultureInfo Language { get; set; }
    public MaterialTheme Theme { get; set; }
    public MaterialColor PrimaryColor { get; set; }
    public MaterialColor SecondaryColor { get; set; }
    public string LastDirectory { get; set; }
    public string ResourcesLocation { get; set; }
}