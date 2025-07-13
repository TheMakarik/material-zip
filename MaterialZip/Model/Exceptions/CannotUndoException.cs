namespace MaterialZip.Model.Exceptions;

/// <summary>
/// Throws when you cannot make undo in your explorer history
/// </summary>
/// <param name="message">The message that describes the error</param>
public sealed class CannotUndoException(string message) : Exception(message);
