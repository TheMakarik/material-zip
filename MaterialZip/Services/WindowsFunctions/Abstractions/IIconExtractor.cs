using System.Drawing;
using System.Windows.Media.Imaging;
using MaterialZip.Model.Entities;

namespace MaterialZip.Services.WindowsFunctions.Abstractions;

public interface IIconExtractor
{
    public BitmapSource? FromPath(string path); 
}