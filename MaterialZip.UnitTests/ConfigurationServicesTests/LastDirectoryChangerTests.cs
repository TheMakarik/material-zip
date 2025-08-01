using FakeItEasy;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.UnitTests.Core.Stubs;
using Microsoft.Extensions.Logging;
using ILogger = Serilog.ILogger;

namespace MaterialZip.UnitTests.ConfigurationServicesTests;

[TestFixture]
public class LastDirectoryChangerTests
{
    private IApplicationConfigurationManager _applicationConfigurationManager;
    private ILogger<LastDirectoryChanger> _logger
        ;

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger<LastDirectoryChanger>>();
        _applicationConfigurationManager = A.Fake<IApplicationConfigurationManager>();
    }

    [Test]
    public void ChangeLastDirectory_Always_CallToApplicationConfigurationManagerLastDirectorySetter()
    {
        //arrange
        var lastDirectoryChanger = new LastDirectoryChanger(_applicationConfigurationManager, _logger);
        var dummyPath = FileEntityFactory.CreateDirectory().Path;
        //act
        lastDirectoryChanger.ChangeLastDirectory(dummyPath);
        //assert
        A.CallToSet(() => _applicationConfigurationManager.LastDirectory);
    }

    [Test]
    public void ChangeLastDirectory_Always_InvokeLogInformation()
    {
        //arrange
        var lastDirectoryChanger = new LastDirectoryChanger(_applicationConfigurationManager, _logger);
        var dummyPath = FileEntityFactory.CreateDirectory().Path;
        //act
        lastDirectoryChanger.ChangeLastDirectory(dummyPath);
        //assert
        A.CallTo(_logger)
            .Where(f => f.Method.Name == nameof(_logger.Log))
            .MustHaveHappenedOnceExactly();
    }
    
}