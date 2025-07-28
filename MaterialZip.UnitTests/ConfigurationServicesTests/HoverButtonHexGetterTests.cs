using FakeItEasy;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using Serilog;

namespace MaterialZip.UnitTests.ConfigurationServicesTests;

[TestFixture]
public class HoverButtonEffectGetterTests
{
    private const string ExpectedHex = "#FF0000";
    private ILogger _logger;
    private IApplicationConfigurationManager _applicationConfigurationManager;
    private HoverButtonHexGetter _hoverButtonEffectGetter;

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger>();
        _applicationConfigurationManager = A.Fake<IApplicationConfigurationManager>();
        _hoverButtonEffectGetter = new HoverButtonHexGetter(_applicationConfigurationManager, _logger);
        A.CallTo(() => _applicationConfigurationManager.HoverColorHex).Returns(ExpectedHex);
    }

    [Test]
    public void GetHoverButtonHex_Always_GettingHoverColorHexFromConfiguration()
    {
        //arrange
        //act
        var result = _hoverButtonEffectGetter.GetHoverButtonHex();
        //assert
        A.CallTo(() => _applicationConfigurationManager.HoverColorHex).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void GetHoverButtonHex_Always_LogDebugWithHexValue()
    {
        //arrange
        //act
        _hoverButtonEffectGetter.GetHoverButtonHex();
        //assert
        A.CallTo(_logger)
            .Where(call => call.Method.Name == nameof(_logger.Debug))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void GetHoverButtonHex_Always_ReturnConfigurationValue()
    {
        //arrange

        //act
        var result = _hoverButtonEffectGetter.GetHoverButtonHex();
        //assert
        Assert.That(result, Is.EqualTo(ExpectedHex));
    }
}