using MaterialZip.Model.Entities;

namespace MaterialZip.UnitTests.Core.Stubs;

/// <summary>
/// Represent default <see cref="FileEntity"/> stub maker 
/// </summary>
public static class FileEntityFactory
{
    /// <summary>
    /// Create one <see cref="FileEntity"/> as directory
    /// </summary>
    /// <returns>Created directory</returns>
    public static FileEntity CreateDirectory()
        => new FileEntity("/dummy-directory", true);

    /// <summary>
    /// Create <see cref="FileEntity"/> as directory instance with specific count
    /// </summary>
    /// <param name="length">Count of instances</param>
    /// <returns>Array with <see cref="FileEntity"/> as directory</returns>
    public static FileEntity[] CreateDirectories(int length)
    {
        var result = new FileEntity[length];
        for (var i = 0; i < length; i++)
            result[i] = new FileEntity($"/dummy-directory{i}", true);
        return result;
    }
    
    /// <summary>
    /// Create one <see cref="FileEntity"/> as file
    /// </summary>
    /// <returns>Created file</returns>
    public static FileEntity CreateFile()
        => new FileEntity("/dummy-file", false);

    
    /// <summary>
    /// Create <see cref="FileEntity"/> as file instance with specific count
    /// </summary>
    /// <param name="length">Count of instances</param>
    /// <returns>Array with <see cref="FileEntity"/> as file</returns>
    public static FileEntity[] CreateFiles(int length)
    {
        var result = new FileEntity[length];
        for (var i = 0; i < length; i++)
            result[i] = new FileEntity($"/dummy-file{i}", false);
        return result;
    }
}