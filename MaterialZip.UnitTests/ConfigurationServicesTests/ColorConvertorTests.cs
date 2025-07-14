using System.Windows.Media;
using MaterialDesignColors;
using MaterialZip.Model.Enums;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.Services.ConfigurationServices.Abstractions;

namespace MaterialZip.UnitTests.ConfigurationServicesTests;

[TestFixture]
public class ColorConvertorTests
{
    private IColorConvertor _colorConvertor;
    private readonly Dictionary<MaterialColor, Color> _expectedValues = new()
    {
        { MaterialColor.Red, SwatchHelper.Lookup[MaterialDesignColor.Red] },
        { MaterialColor.Pink, SwatchHelper.Lookup[MaterialDesignColor.Pink] },
        { MaterialColor.Purple, SwatchHelper.Lookup[MaterialDesignColor.Purple] },
        { MaterialColor.DeepPurple, SwatchHelper.Lookup[MaterialDesignColor.DeepPurple] },
        { MaterialColor.Indigo, SwatchHelper.Lookup[MaterialDesignColor.Indigo] },
        { MaterialColor.Blue, SwatchHelper.Lookup[MaterialDesignColor.Blue] },
        { MaterialColor.LightBlue, SwatchHelper.Lookup[MaterialDesignColor.LightBlue] },
        { MaterialColor.Cyan, SwatchHelper.Lookup[MaterialDesignColor.Cyan] },
        { MaterialColor.Teal, SwatchHelper.Lookup[MaterialDesignColor.Teal] },
        { MaterialColor.Green, SwatchHelper.Lookup[MaterialDesignColor.Green] },
        { MaterialColor.LightGreen, SwatchHelper.Lookup[MaterialDesignColor.LightGreen] },
        { MaterialColor.Lime, SwatchHelper.Lookup[MaterialDesignColor.Lime] },
        { MaterialColor.Yellow, SwatchHelper.Lookup[MaterialDesignColor.Yellow] },
        { MaterialColor.Amber, SwatchHelper.Lookup[MaterialDesignColor.Amber] },
        { MaterialColor.Orange, SwatchHelper.Lookup[MaterialDesignColor.Orange] },
        { MaterialColor.DeepOrange, SwatchHelper.Lookup[MaterialDesignColor.DeepOrange] }
    };
    
   
    [SetUp]
    public void SetUp()
    {
        _colorConvertor = new ColorConvertor();
    }

    [TestCase(MaterialColor.Red)]
    [TestCase(MaterialColor.Amber)]
    [TestCase(MaterialColor.DeepPurple)]
    [TestCase(MaterialColor.Yellow)]
    [TestCase(MaterialColor.Green)]
    public void ToWpfColor_WithExistedColor_MustConvertSuccessfully(MaterialColor color)
    {
        //arrange
        
        //act
        var result = _colorConvertor.ToWpfColor(color);
        //assert
        Assert.That(result, Is.EqualTo(_expectedValues[color]));
    }
    
}