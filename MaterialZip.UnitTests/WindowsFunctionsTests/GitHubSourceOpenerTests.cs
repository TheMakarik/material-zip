using FakeItEasy;
using MaterialZip.Services.ConfigurationServices.Abstractions;
using MaterialZip.Services.WindowsFunctions;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.UnitTests.WindowsFunctionsTests;

[TestFixture]
public class GitHubSourceOpenerTests
{
    private IApplicationConfigurationManager _configuration;
    private IUrlOpener _urlOpener;

    [SetUp]
    public void SetUp()
    {
        _configuration = A.Fake<IApplicationConfigurationManager>();
        _urlOpener = A.Fake<IUrlOpener>();
     
    }

    [TestCase(true)]
    [TestCase(false)]
    public void TryOpen_Always_ReturnValueFromUrlOpener(bool resultFromUrlOpener)
    {
        //arrange
        A.CallTo(() => _urlOpener.TryOpen(A<string>._)).Returns(resultFromUrlOpener);
        var sourceOpener = new GitHubSourceOpener(_urlOpener, _configuration);
        //act
        var result = sourceOpener.TryOpen();
        //assert
        Assert.That(resultFromUrlOpener, Is.EqualTo(result));
    }

    [Test]
    public void TryOpen__Always_GetGitHubSourceFromConfiguration()
    {
        //arrange
        var sourceOpener = new GitHubSourceOpener(_urlOpener, _configuration);
        //act
        sourceOpener.TryOpen();
        //assert
        A.CallTo(() => _configuration.GitHubSourceLink).MustHaveHappenedOnceExactly();
    }
    
}