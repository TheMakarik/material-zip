using FakeItEasy;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ExplorerServices;
using MaterialZip.Services.ExplorerServices.Abstractions;
using MaterialZip.UnitTests.Core.Stubs;
using Serilog;

namespace MaterialZip.UnitTests.ExplorerTests;

[TestFixture]
public class ExplorerHistoryTests
{
    private IExplorerHistoryMemory _memory;
    private ILogger _logger;

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger>();
        _memory = A.Fake<IExplorerHistoryMemory>();
    }

    [Test]
    public void CanRedo_AfterInstanceCreating_ReturnFalse()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        //act
        var result = history.CanRedo;
        //assert
        Assert.IsFalse(result);
    }

    [Test]
    public void CanUndo_AfterInstanceCreating_ReturnFalse()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        //act
        var result = history.CanUndo;
        //assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Undo_WhenCanUndoIsFalse_ThrowsException()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        //assert and act
        Assert.Throws<CannotUndoException>(() => history.Undo());
    }

    [Test]
    public void Redo_WhenCanRedoIsFalse_ThrowsException()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        //assert and act
        Assert.Throws<CannotRedoException>(() => history.Redo());
    }

    [Test]
    public void Undo_WhenCanUndoIsFalse_LogFatal()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        //act
        try
        {
            history.Undo();
        }
        catch (CannotUndoException)
        {
        }

        //assert
        A.CallTo(() => _logger.Fatal(A<string>._, An<int>._, An<int>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void Redo_WhenCanRedoIsFalse_LogFatal()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        //act
        try
        {
            history.Redo();
        }
        catch (CannotRedoException)
        {
        }

        //assert
        A.CallTo(() => _logger.Fatal(A<string>._, An<int>._, An<int>._)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void CanUndo_AfterAddingTwoElements_ReturnsTrue()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        A.CallTo(() => _memory.Index).Returns(1);
        //act
        var result = history.CanUndo;
        //assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Undo_WhenCanUndoIsTrue_DecreasesIndex()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        A.CallTo(() => _memory.Index).Returns(1);
        //act
        history.Undo();
        //assert
        A.CallToSet(() => _memory.Index).To(0).MustHaveHappenedOnceExactly();
    }
    
    [Test]
    public void CurrentDirectory_Get_ReturnsMemoryCurrentDirectory()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        var expected = FileEntityFactory.CreateDirectory();
        A.CallTo(() => _memory.CurrentDirectory).Returns(expected);
        //act
        var result = history.CurrentDirectory;
        //assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void CurrentDirectory_Set_UpdatesMemoryCurrentDirectory()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        var directory = FileEntityFactory.CreateDirectory();
        //act
        history.CurrentDirectory = directory;
        //assert
        A.CallToSet(() => _memory.CurrentDirectory).To(directory).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void Index_AfterMultipleOperations_ReflectsMemoryState()
    {
        //arrange
        var history = new ExplorerHistory(_logger, _memory);
        A.CallTo(() => _memory.Index).Returns(2);
        //act
        var canUndo = history.CanUndo;
        var canRedo = history.CanRedo;
        //assert
        Assert.Multiple(() =>
        {
            Assert.IsTrue(canUndo);
            Assert.That(canRedo, Is.EqualTo(_memory.Index + 1 < _memory.HistoryList.Count));
        });
    }
}