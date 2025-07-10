using MaterialZip.Services.ConfigurationServices.Abstractions;

namespace MaterialZip.Services.ConfigurationServices;

public class LastDirectoryLoader(IApplicationConfigurationManager configurationManager) : ILastDirectoryLoader
{
    public string GetLastDirectory()
    {
        return configurationManager.LastDirectory;
    }
}