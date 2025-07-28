namespace MaterialZip.Services.ConfigurationServices.Abstractions;

/// <summary>
/// Interface for getting hover button hex value
/// </summary>
public interface IHoverButtonHexGetter
{
    /// <summary>
    /// Get hex value for hover button effect
    /// </summary>
    /// <returns>Hover button effect hex value</returns>
    public string GetHoverButtonHex();
}