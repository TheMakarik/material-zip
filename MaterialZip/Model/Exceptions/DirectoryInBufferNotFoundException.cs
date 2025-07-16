using MaterialZip.Services.ConfigurationServices.Abstractions;

namespace MaterialZip.Model.Exceptions;

/// <summary>
/// Invokes if <see cref="ILastDirectoryBuffer"/> implementation cannot find buffer but trying to get it
/// </summary>
/// <param name="message">The message that describes the error</param>
public class DirectoryNotFoundException(string message) : Exception(message);