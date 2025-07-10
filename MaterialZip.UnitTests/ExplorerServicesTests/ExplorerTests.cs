using FakeItEasy;
using MaterialZip.Model.Entities;
using MaterialZip.Services.ExplorerServices;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Serilog;

namespace MaterialZip.UnitTests.ExplorerServicesTests;

[TestFixture]
public class ExplorerTests
{

   private IExplorer _explorer;
   private ILowLevelExplorer _lowLevelExplorer; 
   private ILogger _logger;
   private FileEntity _directory;
   private FileEntity _file;
   
   [SetUp]
   public void SetUp()
   {
      _lowLevelExplorer = A.Fake<ILowLevelExplorer>();
      _logger = A.Fake<ILogger>();
      _directory = new FileEntity("/dummy-directory", IsDirectory: true);
      _file = new FileEntity("/dummy-file.exe", IsDirectory: false);
      _explorer = new Explorer(_lowLevelExplorer, _logger);
   }

   [Test]
   public void GetLogicalDrives_Always_InvokeGetLogicalDrivesFromProxy()
   {
      //arrange
   
      //act
      _explorer.GetDirectoryContent(_directory);
      //assert
      A.CallTo(() => _lowLevelExplorer.GetDirectories(A<string>._)).MustHaveHappened(); 
   }

   [Test]
   public void GetLogicalDrives_Always_ReturnFileEntityAsDirectory()
   {
      //arrange
      var lowLevelExplorer = A.Fake<ILowLevelExplorer>();
      A.CallTo(() => lowLevelExplorer.GetDirectories(A<string>._))
         .Returns(["/dummy-dir"]);
      var explorer = new Explorer(lowLevelExplorer, _logger); 
      //act
      var directories = explorer.GetDirectoryContent(_directory);
      //assert
      Assert.That(directories.Count, Is.EqualTo(directories.Count(d => d.IsDirectory))) ;
   }

   [Test]
   public void GetDirectoryContent_Always_InvokesGetFilesFromProxy()
   {
      //arrange
      
      //act
      _explorer.GetDirectoryContent(_directory);
      //assert
      A.CallTo(() => _lowLevelExplorer.GetFiles(A<string>._)).MustHaveHappened();
      
   }
   
   [Test]
   public void GetDirectoryContent_Always_InvokesGetDirectoriesFromProxy()
   {
      //arrange
      
      //act
      _explorer.GetDirectoryContent(_directory);
      //assert
      A.CallTo(() => _lowLevelExplorer.GetDirectories(A<string>._)).MustHaveHappened();
      
   }
   
   [Test]
   public void GetDirectoryContent_FileEntityIsNotDirectory_InvokesLogWarning()
   {
      //arrange
      var logger = A.Fake<ILogger>();
      var explorer = new Explorer(_lowLevelExplorer, logger);
      //act
      explorer.GetDirectoryContent(_file);
      //assert
      A.CallTo(() => logger.Warning(A<string>._, A<string>._)).MustHaveHappened();
   }
   
   [Test]
   public void GetDirectoryContent_FileEntityIsNotDirectory_ReturnsEmptyCollection()
   {
      //arrange
      
      //act
      var result = _explorer.GetDirectoryContent(_file);
      //assert
      Assert.That(result, Is.Empty);
   }
   
   [TestCase(5)]
   public void GetDirectoryContent_Always_ReturnDirectoriesAsDirectories(int directoryLength)
   {
      //arrange
      var directories = GetDummyDirectories(directoryLength);
      var files = GetDummyFiles(5);
      A.CallTo(() => _lowLevelExplorer.GetDirectories(A<string>._)).Returns(directories);
      A.CallTo(() => _lowLevelExplorer.GetFiles(A<string>._)).Returns(files);
      var explorer = new Explorer(_lowLevelExplorer, _logger);
      //act
      var result = explorer.GetDirectoryContent(_directory);
      //assert
      Assert.That(directories.Length, Is.EqualTo(result.Count(e => e.IsDirectory)));
   }

   [TestCase(5)]
   public void GetDirectoryContent_Always_ReturnFilesAsNotDirectories(int filesLength)
   {
      //arrange
      var directories = GetDummyDirectories(5);
      var files = GetDummyFiles(filesLength);
      A.CallTo(() => _lowLevelExplorer.GetDirectories(A<string>._)).Returns(directories);
      A.CallTo(() => _lowLevelExplorer.GetFiles(A<string>._)).Returns(files);
      var explorer = new Explorer(_lowLevelExplorer, _logger);
      //act
      var result = explorer.GetDirectoryContent(_directory);
      //assert
      Assert.That(files.Length, Is.EqualTo(result.Count(e => e.IsDirectory)));
   }


   private string[] GetDummyDirectories(int length)
   {
      return GetDummyStringArray(length, "/dummy-directory", string.Empty);
   }

   private string[] GetDummyFiles(int length)
   {
      return GetDummyStringArray(length, "/dummy-directory/file", "exe");
   }


   private string[] GetDummyStringArray(int length, string arrayStartPattern, string arrayEndPattern)
   {
      var array = new string[length];
      for (int i = 0; i < length; i++)
      {
         array[i] = $"{arrayStartPattern}{i}{arrayEndPattern}";
      }

      return array;
   }
   
}