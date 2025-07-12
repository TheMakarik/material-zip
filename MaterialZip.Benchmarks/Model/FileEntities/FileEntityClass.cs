namespace MaterialZip.Benchmarks.Model.FileEntities;

public class FileEntityClass(string path, bool isDirectory)
{
    public string Path { get; } = path;
    public bool IsDirectory { get; } = isDirectory;
}