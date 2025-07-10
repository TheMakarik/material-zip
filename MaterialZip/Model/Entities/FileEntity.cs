namespace MaterialZip.Model.Entities;

/// <summary>
/// Represents a default file entity with predefined boolean <see cref="IsDirectory"/> to do not check it every time when it need
/// </summary>
/// <param name="Path">Path to the entity</param>
/// <param name="IsDirectory">Is this entity represents directory</param>
/// <remarks>
/// This record in arguments must be called like "directory" if it needs to be  directory, "file" for file or "entity" if does not matter what type entity has
/// </remarks>
public record struct FileEntity(string Path, bool IsDirectory);