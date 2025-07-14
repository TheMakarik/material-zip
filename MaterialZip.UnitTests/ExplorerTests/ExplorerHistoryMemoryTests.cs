using FakeItEasy;
using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ExplorerServices;
using MaterialZip.Services.ExplorerServices.Abstractions;
using MaterialZip.UnitTests.Core.Stubs;
using Serilog;

namespace MaterialZip.UnitTests;

[TestFixture]
public class ExplorerHistoryMemoryTests
{
    private ILogger _logger;

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger>();
    }


    [Test]
    public void HistoryList_WhenInstanceHaveJustCreated_Empty()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        //act

        //assert
        CollectionAssert.IsEmpty(memory.HistoryList);
    }

    [Test]
    public void Index_WhenInstanceHaveJustCreated_IsMinusOne()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        //act

        //assert
        Assert.That(memory.Index, Is.EqualTo(-1));
    }

    [Test]
    public void Index_AfterAddingEntity_MustIncrement()
    {
        //arrange
        const int exceptedIndex = 0;
        var memory = new ExplorerHistoryMemory(_logger);
        var directory = FileEntityFactory.CreateDirectory();
        //act
        memory.CurrentDirectory = directory;
        //assert
        Assert.That(memory.Index, Is.EqualTo(exceptedIndex));
    }

    [Test]
    public void CurrentEntity_AfterAddingEntity_ReturnsThisEntity()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directory = FileEntityFactory.CreateDirectory();
        //act
        memory.CurrentDirectory = directory;
        //assert
        Assert.That(memory.CurrentDirectory, Is.EqualTo(directory));
    }

    [Test]
    public void HistoryList_AfterAddingEntity_ContainThisEntitiy()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directory = FileEntityFactory.CreateDirectory();
        //act
        memory.CurrentDirectory = directory;
        //assert
        CollectionAssert.Contains(memory.HistoryList, directory);
    }

    [Test]
    public void CurrentEntity_AfterAddingEntity_InvokesLogDebug()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directory = FileEntityFactory.CreateDirectory();
        //act
        memory.CurrentDirectory = directory;
        //assert
        A.CallTo(() => _logger.Debug(A<string>._, A<string>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void CurrentEntity_HistoryListIsEmpty_ThrowsException()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        //assert and act
        Assert.Throws<EmptyHistoryException>(() =>
        {
            var result = memory.CurrentDirectory;
        });
    }

    [Test]
    public void CurrentEntity_HistoryListIsEmpty_InvokeLogFatal()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        //act
        try
        {
            var result = memory.CurrentDirectory;
        }
        catch (EmptyHistoryException e)
        {
        }
        //assert
        A.CallTo(() => _logger.Fatal(A<string>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void CurrentDirectory_AfterAddingDirectory_LogDebugTwice()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directory = FileEntityFactory.CreateDirectory();
        //act
        memory.CurrentDirectory = directory;
        var result = memory.CurrentDirectory;
        //assert
        A.CallTo(() => _logger.Debug(A<string>._, A<string>._)).MustHaveHappenedTwiceExactly();
    }
    
    [Test]
    public void HistoryList_AfterAddingThreeEntities_ContainsAllInOrder()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(3);
        //act
        AddRangeToHistory(directories, memory);
        //assert
        Assert.That(memory.HistoryList, Is.EqualTo(directories));
    }

    [Test]
    public void Index_AfterAddingFiveEntities_EqualsLastIndex()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(5);
        //act
        AddRangeToHistory(directories, memory);
        //assert
        Assert.That(memory.Index, Is.EqualTo(directories.Length - 1));
    }

    [Test]
    public void CurrentDirectory_AfterUndoAndAdd_CutsHistoryList()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(3);
        AddRangeToHistory(directories[..1], memory);
        memory.Index = 0; // Simulate undo
        //act
        memory.CurrentDirectory = directories.Last();
        //assert
        Assert.That(memory.HistoryList, Is.EqualTo(new[] { directories[0], directories[2] }));
    }

    [Test]
    public void CurrentDirectory_AfterUndoAndAdd_UpdatesIndexCorrectly()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(3);
        AddRangeToHistory(directories[..1], memory);
        memory.Index = 0; //simulate undo
        //act
        memory.CurrentDirectory = directories.Last();
        //assert
        Assert.That(memory.Index, Is.EqualTo(1));
    }

    [Test]
    public void CurrentDirectory_AfterMultipleOperations_LogsCorrectNumberOfTimes()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(3);
        //act
        AddRangeToHistory(directories[..1], memory);
        memory.Index = 0; // Undo
        memory.CurrentDirectory = directories.Last();
        var result = memory.CurrentDirectory;
        //assert
        A.CallTo(() => _logger.Debug(A<string>._, A<string>._))
            .MustHaveHappened(3, Times.Exactly);
    }

    [Test]
    public void HistoryList_AfterInitialization_HasInitialCapacity()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        //act
        var capacity = memory.HistoryList.Capacity;
        //assert
        Assert.That(capacity, Is.EqualTo(10));
    }

    [Test]
    public void CurrentDirectory_AfterUndo_ReturnsCorrectEntity()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(2);
        AddRangeToHistory(directories, memory);
        memory.Index = 0; //simulate undo
        //act
        var result = memory.CurrentDirectory;
        //assert
        Assert.That(result, Is.EqualTo(directories[0]));
    }

    [Test]
    public void CurrentDirectory_GetInRedoPossibleState_DoesNotModifyHistory()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(2);
        AddRangeToHistory(directories, memory);
        memory.Index = 0; //undo 
        //act
        var result = memory.CurrentDirectory;
        //assert
        Assert.That(memory.HistoryList, Has.Count.EqualTo(2));
    }

    [Test]
    public void AddDirectory_WhenHistoryFull_ExpandsCapacity()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(15);
        //act
        AddRangeToHistory(directories, memory);
        //assert
        Assert.That(memory.HistoryList.Capacity, Is.GreaterThanOrEqualTo(15));
    }

    [Test]
    public void Index_AfterMultipleUndoAndRedo_StaysConsistent()
    {
        //arrange
        var memory = new ExplorerHistoryMemory(_logger);
        var directories = FileEntityFactory.CreateDirectories(4);
        //act
        AddRangeToHistory(directories, memory);
        memory.Index = 1; //undo twice
        memory.Index = 3; //redo twice
        //assert
        Assert.That(memory.Index, Is.EqualTo(3));
    }

    private void AddRangeToHistory(FileEntity[] entities, IExplorerHistoryMemory memory)
    {
        foreach (var directory in entities)
            memory.CurrentDirectory = directory;
    }
}