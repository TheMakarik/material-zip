using System.Drawing;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.Services.WindowsFunctions;

/// <inheritdoc cref="IAssociatedIconExtractor"/>
public class AssociatedIconExtractor : IAssociatedIconExtractor
{
    /// <inheritdoc/>
    public Icon? Extract(string filePath)
    {
        return Icon.ExtractAssociatedIcon(filePath);
    }
}
