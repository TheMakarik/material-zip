using FakeItEasy;
using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ExplorerServices;
using Serilog;

namespace MaterialZip.UnitTests.ExplorerServicesTests;

[TestFixture]
public class ExplorerHistoryMemoryTests
{
   private ILogger _logger;
   private ExplorerHistoryMemory _memory;
  
   [SetUp]
   public void SetUp()
   {
      _logger = A.Fake<ILogger>();
      _memory = new ExplorerHistoryMemory(_logger);
   }
   
   [TestCase(2)]
   [TestCase(5)]
   [TestCase(10)]
   [TestCase(14)]
   [TestCase(30)]
   [TestCase(1)]
   public void CurrentDirectoryGetter_AfterAddingElements_ReturnListHistoryElementOfIndex(int elementsCount)
   {
      //arrange
      AddDirectories(elementsCount);
      //act
      var directoryFromCurrentDirectory = _memory.CurrentDirectory;
      var directoryFromIndex = _memory.HistoryList[_memory.Index];
      //assert
      Assert.That(directoryFromCurrentDirectory, Is.EqualTo(directoryFromIndex));
   }

   [Test]
   public void CurrentDirectoryGetter_NoAddingElements_ThrowsException()
   {
      //arrange

      //assert and act
      Assert.Throws<EmptyHistoryException>(() =>
      {
         var test = _memory.CurrentDirectory;
      });

   }
   
   [TestCase(1)]
   [TestCase(3)]
   [TestCase(5)]
   [TestCase(4)]
   [TestCase(2)]
   [TestCase(6)]
   public void CurrentDirectorySetter_AfterAddingValues_IncrementCurrentIndex(int valuesCount)
   {
      //arrange
      AddDirectories(valuesCount);
      //act
      var index = _memory.Index;
      //assert
      Assert.That(index, Is.EqualTo(-1 + valuesCount));
      
   }
   
   [TestCase(3)]
   [TestCase(5)]
   [TestCase(4)]
   [TestCase(6)]
   public void CurrentDirectorySetter_AfterAddingValues_CutHistoryListIfRedoWasDone(int valuesCount)
   {
      //arrange
      AddDirectories(valuesCount);
      //act
      //use ExplorerHistory for redo in production, but here is simulating
      var lastDirectory = _memory.HistoryList.Last();
      _memory.Index--;
      _memory.CurrentDirectory = new FileEntity("/", true);
      var newLastDirectory =_memory.HistoryList.Last();
      //assert
      Assert.That(newLastDirectory, Is.Not.EqualTo(lastDirectory));
   }

   private void AddDirectories(int elementsCount)
   {
      for (var i = 0; i != elementsCount; i++)
         _memory.CurrentDirectory =  new FileEntity($"/dummy-directory{i}", IsDirectory: true);
   }
}