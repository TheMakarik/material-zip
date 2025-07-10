namespace MaterialZip.Services.ConfigurationServices.Abstractions;

public interface ILastDirectoryChanger
{
    public void ChangeLastDirectory(string newDirectoryPath);
}