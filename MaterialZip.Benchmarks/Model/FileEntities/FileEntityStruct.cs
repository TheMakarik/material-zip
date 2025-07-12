namespace MaterialZip.Benchmarks.Model.FileEntities;

public readonly struct FileEntityStruct(string path, bool isDirectory)
{ 
    public string Path { get; } = path;
    public bool IsDirectory { get; } = isDirectory;
}