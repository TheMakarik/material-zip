using MaterialZip.Services.ConfigurationServices.Abstractions;

namespace MaterialZip.Services.ConfigurationServices;

public class LastDirectoryChanger(IApplicationConfigurationManager configurationManager) : ILastDirectoryChanger
{
    public void ChangeLastDirectory(string newDirectoryPath)
    {
        configurationManager.LastDirectory = newDirectoryPath;
    }
}