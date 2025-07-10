namespace MaterialZip.Model.Exceptions;

/// <summary>
/// Throws when you tried to get value from empty history collection
/// </summary>
/// <param name="message">The message that describes the error</param>
public class EmptyHistoryException(string message) : Exception(message);
