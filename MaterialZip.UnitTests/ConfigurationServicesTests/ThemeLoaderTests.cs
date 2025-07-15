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
    private IThemeConvertor _themeConvertor;
    private IColorConvertor _colorConvertor;
    private IApplicationConfigurationManager _applicationConfigurationManager;
    private PaletteHelper _paletteHelper;

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger>();
        _themeConvertor = A.Fake<IThemeConvertor>();
        _colorConvertor = A.Fake<IColorConvertor>();
        _applicationConfigurationManager = A.Fake<IApplicationConfigurationManager>();
        _paletteHelper = A.Fake<PaletteHelper>();
    }

    [Test]
    public void LoadTheme_Always_GettingThemeFromConfiguration()
    {
        //arrange
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(() => _applicationConfigurationManager.Theme).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void LoadTheme_Always_GettingPrimaryColorFromConfiguration()
    {
        //arrange
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(() => _applicationConfigurationManager.PrimaryColor).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void LoadTheme_Always_GettingSecondaryColorFromConfiguration()
    {
        //arrange
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(() => _applicationConfigurationManager.SecondaryColor).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void LoadTheme_Always_SetThemeToPaletteHelper()
    {
        //arrange
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(() => _paletteHelper.SetTheme(A<Theme>._)).MustHaveHappenedOnceExactly();
    }

    [TestCase(MaterialTheme.Dark, BaseTheme.Dark)]
    [TestCase(MaterialTheme.Light, BaseTheme.Light)]
    public void LoadTheme_Always_MustConvertTheme(MaterialTheme theme, BaseTheme expected)
    {
        //arrange
        A.CallTo(() => _applicationConfigurationManager.Theme).Returns(theme);
        A.CallTo(() => _themeConvertor.ToBaseTheme(theme)).Returns(expected);
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(() => _paletteHelper
                .SetTheme(A<Theme>.That.Matches(t => t.GetBaseTheme() == expected)))
            .MustHaveHappenedOnceExactly(); 
    }

    [TestCase(MaterialColor.Amber)]
    public void LoadTheme_Always_MustConvertPrimaryColor(MaterialColor color)
    {
        //arrange
        var expected = Color.FromArgb(3, 32, 50, 50);
        A.CallTo(() => _applicationConfigurationManager.PrimaryColor).Returns(color);
        A.CallTo(() => _colorConvertor.ToWpfColor(color)).Returns(expected);
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(() => _paletteHelper
                .SetTheme(A<Theme>.That.Matches(t => t.PrimaryMid.Color == expected)))
            .MustHaveHappenedOnceExactly(); 
    }
    
    [TestCase(MaterialColor.Amber)]
    public void LoadTheme_Always_MustConvertSecondaryColor(MaterialColor color)
    {
        //arrange
        var expected = Color.FromArgb(3, 32, 50, 50);
        A.CallTo(() => _applicationConfigurationManager.SecondaryColor).Returns(color);
        A.CallTo(() => _colorConvertor.ToWpfColor(color)).Returns(expected);
        var themeLoader = CreateThemeLoader();
        //act
        themeLoader.LoadTheme();
        //assert
        A.CallTo(() => _paletteHelper
                .SetTheme(A<Theme>.That.Matches(t => t.SecondaryMid.Color == expected)))
            .MustHaveHappenedOnceExactly(); 
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
                .Where(call => call.Method.Name == "Information")
                .MustHaveHappened(3, Times.Exactly);
    }
    
    private ThemeLoader CreateThemeLoader() => new ThemeLoader(
        _themeConvertor,
        _colorConvertor, 
        _applicationConfigurationManager,
        _paletteHelper, 
        _logger);
}