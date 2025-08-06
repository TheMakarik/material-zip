using System.Windows.Media;
using MaterialDesignColors;
using MaterialZip.Model.Enums;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using static MaterialZip.Model.Enums.MaterialColor;

namespace MaterialZip.Services.ConfigurationServices;

/// <summary>
/// Represent default convertor from <see cref="MaterialColor"/> to <see cref="Color"/>
/// </summary>
public sealed class ColorConvertor : IColorConvertor
{
    /// <inheritdoc cref="IColorConvertor.ToWpfColor"/>
    public Color ToWpfColor(MaterialColor color)
    {
        return  color switch
        {
            Red => SwatchHelper.Lookup[MaterialDesignColor.Red],
            Pink => SwatchHelper.Lookup[MaterialDesignColor.Pink],
            Purple => SwatchHelper.Lookup[MaterialDesignColor.Purple],
            DeepPurple => SwatchHelper.Lookup[MaterialDesignColor.DeepPurple],
            Indigo => SwatchHelper.Lookup[MaterialDesignColor.Indigo],
            Blue => SwatchHelper.Lookup[MaterialDesignColor.Blue],
            LightBlue => SwatchHelper.Lookup[MaterialDesignColor.LightBlue],
            Cyan => SwatchHelper.Lookup[MaterialDesignColor.Cyan],
            Teal => SwatchHelper.Lookup[MaterialDesignColor.Teal],
            Green => SwatchHelper.Lookup[MaterialDesignColor.Green],
            LightGreen => SwatchHelper.Lookup[MaterialDesignColor.LightGreen],
            Lime => SwatchHelper.Lookup[MaterialDesignColor.Lime],
            Yellow => SwatchHelper.Lookup[MaterialDesignColor.Yellow],
            Amber => SwatchHelper.Lookup[MaterialDesignColor.Amber],
            Orange => SwatchHelper.Lookup[MaterialDesignColor.Orange],
            DeepOrange => SwatchHelper.Lookup[MaterialDesignColor.DeepOrange],
            _ => throw new ArgumentOutOfRangeException(nameof(color), color, null)
        };
       
    }
}