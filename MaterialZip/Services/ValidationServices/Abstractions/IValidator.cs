namespace MaterialZip.Services.ValidationServices.Abstractions;

/// <summary>
/// Represent default abstraction for every validators
/// </summary>
public interface IValidator
{ 
    /// <summary>
    /// Validate something
    /// </summary>
    /// <param name="value">Value to validate</param>
    /// <typeparam name="T">Validate type</typeparam>
    /// <returns>Validating result</returns>
    public bool IsValid<T>(T value);
}