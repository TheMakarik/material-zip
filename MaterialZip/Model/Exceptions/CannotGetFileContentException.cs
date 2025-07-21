using MaterialZip.Services.ExplorerServices;

namespace MaterialZip.Model.Exceptions;

/// <summary>
/// Exception will throw if you tried to use <see cref="Explorer.GetDirectoryContentAsync"/> with file
/// </summary>
/// <param name="message"></param>
public class CannotGetFileContentException(string message) : Exception(message);
