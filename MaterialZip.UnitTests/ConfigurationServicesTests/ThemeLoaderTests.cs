using System.Windows.Media;
using FakeItEasy;
using MaterialDesignThemes.Wpf;
using MaterialZip.Model.Enums;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Serilog;

namespace MaterialZip.UnitTests.ConfigurationServicesTests;

[TestFixture]
public class ThemeLoaderTests
{
    private ILogger _logger;
    private IColorConvertor _colorConvertor;
    private IApplicationConfigurationManager _applicationConfigurationManager;
    private PaletteHelper _paletteHelper;

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger>();
        _colorConvertor = A.Fake<IColorConvertor>();
        _applicationConfigurationManager = A.Fake<IApplicationConfigurationManager>();
        _paletteHelper = A.Fake<PaletteHelper>();
        
        A.CallTo(() => _applicationConfigurationManager.Color).Returns(MaterialColor.Amber);
    }

    [Test]
    public void Constructor_Always_GettingColorFromConfiguration()
    {
        //arrange
        //act
        var themeLoader = CreateThemeLoader();
        //assert
        A.CallTo(() => _applicationConfigurationManager.Color).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void LoadTheme_Always_ConvertMaterialColorToWpfColor()
    {
        //arrange
        var materialColor = MaterialColor.Blue;
        var expectedColor = Colors.Blue;
        A.CallTo(() => _applicationConfigurationManager.Color).Returns(materialColor);
        A.CallTo(() => _colorConvertor.ToWpfColor(materialColor)).Returns(expectedColor);
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(() => _colorConvertor.ToWpfColor(materialColor)).MustHaveHappenedOnceExactly();
    }
    

    [Test]
    public void LoadTheme_Always_LogInformation3Times()
    {
        //arrange
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(_logger)
            .Where(call => call.Method.Name == nameof(_logger.Information))
            .MustHaveHappened(3, Times.Exactly);
    }
    
    private ThemeLoader CreateThemeLoader() => new ThemeLoader(
        _colorConvertor, 
        _applicationConfigurationManager,
        _paletteHelper, 
        _logger);
}