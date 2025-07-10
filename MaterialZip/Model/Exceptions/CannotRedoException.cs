namespace MaterialZip.Model.Exceptions;

/// <summary>
/// Throws when you cannot make redo in your explorer history
/// </summary>
/// <param name="message">The message that describes the error</param>
public class CannotRedoException(string message) : Exception(message);
