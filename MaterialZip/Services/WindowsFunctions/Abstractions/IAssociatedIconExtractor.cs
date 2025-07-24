using System.Drawing;

namespace MaterialZip.Services.WindowsFunctions.Abstractions;

public interface IAssociatedIconExtractor
{
    public Icon? Extract(string filePath);
}