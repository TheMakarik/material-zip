using System.Drawing;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.Services.WindowsFunctions;

public class AssociatedIconExtractor : IAssociatedIconExtractor
{
    public Icon? Extract(string filePath)
    {
        return Icon.ExtractAssociatedIcon(filePath);
    }
}