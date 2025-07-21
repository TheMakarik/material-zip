using FakeItEasy;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ExplorerServices;
using MaterialZip.Services.ExplorerServices.Abstractions;
using MaterialZip.UnitTests.Core.Stubs;
using Serilog;

namespace MaterialZip.UnitTests.ExplorerTests;

[TestFixture]
public class ExplorerTests
{

    private ILogger _logger;
    private ILowLevelExplorer _lowLevelExplorer;

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger>();
        _lowLevelExplorer = A.Fake<ILowLevelExplorer>();
    }
    
    [Test]
    public async Task  GetLogicalDrives_Always_ReturnFileEntityWithIsDirectoryAsTrue()
    {
        //arrange
        var dummyDirectories = FileEntityFactory.CreateDirectories(4)
            .Select(p => p.Path)
            .ToArray();
        A.CallTo(() => _lowLevelExplorer.GetLogicalDrives())
            .Returns(dummyDirectories);
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = await explorer.GetLogicalDrivesAsync();
        //assert
        Assert.IsTrue(result.All(e => e.IsDirectory));
    }

    [Test]
    public async Task  GetLogicalDrives_Always_InvokeLogDebug()
    {
        //arrange
        A.CallTo(() => _logger.Debug(A<string>._)).DoesNothing();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = await explorer.GetLogicalDrivesAsync();
        //assert
        A.CallTo(_logger)
            .Where(f => f.Method.Name == nameof(_logger.Debug))
            .MustHaveHappened();
    }

    [Test]
    public async Task GetDirectoryContent_WithCorrectArgs_InvokesGetFilesFromLowLevelExplorer()
    {
        //arrange
        var dummyDirectory = FileEntityFactory.CreateDirectory();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = await explorer.GetDirectoryContentAsync(dummyDirectory);
        //assert
        A.CallTo(() => _lowLevelExplorer.GetFiles(dummyDirectory.Path)).MustHaveHappened();
    }

    [Test]
    public async Task GetDirectoryContent_WithCorrectArgs_InvokesGetDirectoriesFromLowLevelExplorer()
    {
        //arrange
        var dummyDirectory = FileEntityFactory.CreateDirectory();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = await explorer.GetDirectoryContentAsync(dummyDirectory);
        //assert
        A.CallTo(() => _lowLevelExplorer.GetDirectories(dummyDirectory.Path)).MustHaveHappened();
    }

    [Test]
    public async Task GetDirectoryContent_FileEntityAsFile_ThrowsException()
    {
        //arrange
        var dummyFile = FileEntityFactory.CreateFile();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act and assert
        Assert.ThrowsAsync<CannotGetFileContentException>(async () =>
        {
            var result = await explorer.GetDirectoryContentAsync(dummyFile);
        });
    }
    
    [Test]
    public async Task GetDirectoryContent_FileEntityAsFile_InvokesLogError()
    {
        //arrange
        var dummyFile = FileEntityFactory.CreateFile();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        try
        {
            var result = await explorer.GetDirectoryContentAsync(dummyFile);
        }
        catch (Exception e) { /*Ignored*/ }

        //assert
        A.CallTo( _logger)
            .Where(f => f.Method.Name == nameof(_logger.Error))
            .MustHaveHappened();
    }

    [Test]
    public  async Task GetDirectoryContent_WithCorrectArgs_InvokesLogDebug()
    {
        //arrange
        var dummyDirectory = FileEntityFactory.CreateDirectory();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = await explorer.GetDirectoryContentAsync(dummyDirectory);
        //assert
        A.CallTo(() => _logger.Debug(A<string>._, A<string>._)).MustHaveHappened();
    }
    
    [TestCase(4, 4)]
    public async Task GetDirectoryContent_WithCorrectArgs_ReturnsFilesAsNotDirectory(int directoriesCount, int filesCount)
    {
        //arrange
        var dummyDirectory = FileEntityFactory.CreateDirectory();
        var dummyFilesPath = FileEntityFactory.CreateFiles(filesCount)
            .Select(e => e.Path)
            .ToArray();
        var dummyDirectoriesPath = FileEntityFactory.CreateDirectories(directoriesCount)
            .Select(e => e.Path)
            .ToArray();
        A.CallTo(() => _lowLevelExplorer.GetFiles(dummyDirectory.Path)).Returns(dummyFilesPath);
        A.CallTo(() => _lowLevelExplorer.GetDirectories(dummyDirectory.Path)).Returns(dummyDirectoriesPath);
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = await explorer.GetDirectoryContentAsync(dummyDirectory);
        //assert
        Assert.That(result.Count(e => !e.IsDirectory), Is.EqualTo(filesCount));
    }
    
    [TestCase(4, 4)]
    public async Task GetDirectoryContent_WithCorrectArgs_ReturnsDirectoriesAsDirectory(int directoriesCount, int filesCount)
    {
        //arrange
        var dummyDirectory = FileEntityFactory.CreateDirectory();
        var dummyFilesPath = FileEntityFactory.CreateFiles(filesCount)
            .Select(e => e.Path)
            .ToArray();
        var dummyDirectoriesPath = FileEntityFactory.CreateDirectories(directoriesCount)
            .Select(e => e.Path)
            .ToArray();
        A.CallTo(() => _lowLevelExplorer.GetFiles(dummyDirectory.Path)).Returns(dummyFilesPath);
        A.CallTo(() => _lowLevelExplorer.GetDirectories(dummyDirectory.Path)).Returns(dummyDirectoriesPath);
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = await explorer.GetDirectoryContentAsync(dummyDirectory);
        //assert
        Assert.That(result.Count(e => e.IsDirectory), Is.EqualTo(directoriesCount));
    }
}