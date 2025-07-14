using FakeItEasy;
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
    public void GetLogicalDrives_Always_ReturnFileEntityWithIsDirectoryAsTrue()
    {
        //arrange
        var dummyDirectories = FileEntityFactory.CreateDirectories(4)
            .Select(p => p.Path)
            .ToArray();
        A.CallTo(() => _lowLevelExplorer.GetLogicalDrives())
            .Returns(dummyDirectories);
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = explorer.GetLogicalDrives();
        //assert
        Assert.IsTrue(result.All(e => e.IsDirectory));
    }

    [Test]
    public void GetLogicalDrives_Always_InvokeLogDebug()
    {
        //arrange
        A.CallTo(() => _logger.Debug(A<string>._)).DoesNothing();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = explorer.GetLogicalDrives();
        //assert
        A.CallTo(() => _logger.Debug(A<string>._, A<string[]>._)).MustHaveHappened();
    }

    [Test]
    public void GetDirectoryContent_WithCorrectArgs_InvokesGetFilesFromLowLevelExplorer()
    {
        //arrange
        var dummyDirectory = FileEntityFactory.CreateDirectory();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = explorer.GetDirectoryContent(dummyDirectory);
        //assert
        A.CallTo(() => _lowLevelExplorer.GetFiles(dummyDirectory.Path)).MustHaveHappened();
    }

    [Test]
    public void GetDirectoryContent_WithCorrectArgs_InvokesGetDirectoriesFromLowLevelExplorer()
    {
        //arrange
        var dummyDirectory = FileEntityFactory.CreateDirectory();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = explorer.GetDirectoryContent(dummyDirectory);
        //assert
        A.CallTo(() => _lowLevelExplorer.GetDirectories(dummyDirectory.Path)).MustHaveHappened();
    }

    [Test]
    public void GetDirectoryContent_FileEntityAsFile_ReturnEmptyCollection()
    {
        //arrange
        var dummyFile = FileEntityFactory.CreateFile();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = explorer.GetDirectoryContent(dummyFile);
        //assert
        CollectionAssert.IsEmpty(result);
    }
    
    [Test]
    public void GetDirectoryContent_FileEntityAsFile_InvokesLogWarning()
    {
        //arrange
        var dummyFile = FileEntityFactory.CreateFile();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = explorer.GetDirectoryContent(dummyFile);
        //assert
        A.CallTo(() => _logger.Warning(A<string>._, A<string>._)).MustHaveHappened();
    }

    [Test]
    public void GetDirectoryContent_WithCorrectArgs_InvokesLogDebug()
    {
        //arrange
        var dummyDirectory = FileEntityFactory.CreateDirectory();
        var explorer = new Explorer(_lowLevelExplorer, _logger);
        //act
        var result = explorer.GetDirectoryContent(dummyDirectory);
        //assert
        A.CallTo(() => _logger.Debug(A<string>._, A<string>._)).MustHaveHappened();
    }
    
    [TestCase(4, 4)]
    public void GetDirectoryContent_WithCorrectArgs_ReturnsFilesAsNotDirectory(int directoriesCount, int filesCount)
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
        var result = explorer.GetDirectoryContent(dummyDirectory);
        //assert
        Assert.That(result.Count(e => !e.IsDirectory), Is.EqualTo(filesCount));
    }
    
    [TestCase(4, 4)]
    public void GetDirectoryContent_WithCorrectArgs_ReturnsDirectoriesAsDirectory(int directoriesCount, int filesCount)
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
        var result = explorer.GetDirectoryContent(dummyDirectory);
        //assert
        Assert.That(result.Count(e => e.IsDirectory), Is.EqualTo(directoriesCount));
    }
}