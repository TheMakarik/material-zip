using MaterialDesignThemes.Wpf;
using MaterialZip.Model.Enums;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.Services.ConfigurationServices.Abstractions;

namespace MaterialZip.UnitTests.ConfigurationServicesTests;

[TestFixture]
public class ThemeConvertorTests
{
    private IThemeConvertor _themeConvertor;

    [SetUp]
    public void SetUp()
    {
        _themeConvertor = new ThemeConvertor();
    }

    [Test]
    public void ToBaseTheme_WithDarkTheme_ReturnBaseThemeDark()
    {
        //arrange
        var theme = MaterialTheme.Dark;
        //act
        var result = _themeConvertor.ToBaseTheme(theme);
        //assert 
        Assert.That(result, Is.EqualTo(BaseTheme.Dark));
    }
    
    [Test]
    public void ToBaseTheme_WithLightTheme_ReturnBaseThemeLight()
    {
        //arrange
        var theme = MaterialTheme.Light;
        //act
        var result = _themeConvertor.ToBaseTheme(theme);
        //assert
        Assert.That(result, Is.EqualTo(BaseTheme.Light));
    }
}

