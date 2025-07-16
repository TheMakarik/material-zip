using FakeItEasy;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.UnitTests.Core.Stubs;

namespace MaterialZip.UnitTests.ConfigurationServicesTests;

[TestFixture]
public class LastDirectoryGetterTests
{
    private IApplicationConfigurationManager _applicationConfigurationManager;

    [SetUp]
    public void SetUp()
    {
        _applicationConfigurationManager = A.Fake<IApplicationConfigurationManager>();
    }

    [Test]
    public void LastDirectory_Always_GetsLastDirectoryFromConfiguration()
    {
        //arrange
        var directory = FileEntityFactory.CreateDirectory().Path;
        A.CallTo(() => _applicationConfigurationManager.LastDirectory).Returns(directory);
        var lastDirectoryGetter = new LastDirectoryGetter(_applicationConfigurationManager);
        //act
        var result = lastDirectoryGetter.LastDirectory;
        //assert
        Assert.That(result, Is.EqualTo(directory));
    }
    
}