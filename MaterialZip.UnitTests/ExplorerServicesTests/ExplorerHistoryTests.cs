using FakeItEasy;
using MaterialZip.Model.Entities;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ExplorerServices;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Serilog;

namespace MaterialZip.UnitTests.ExplorerServicesTests;

[TestFixture]
public class ExplorerHistoryTests
{
    private IExplorerHistory _history;
    private ILogger _logger;
    private readonly FileEntity _directory = new FileEntity("/dummy-directory", IsDirectory: true);

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger>();
        _history = new ExplorerHistory(_logger, new ExplorerHistoryMemory(_logger));
    }

    [Test]
    public void  CanRedo_WithoutElements_ReturnsFalse()
    {
        //arrange
        
        //act
        var result = _history.CanRedo;
        //assert
        Assert.IsFalse(result);
    }

    [Test]
    public void CanUndo_WithoutElements_ReturnsFalse()
    {
        //arrange
        
        //act
        var result = _history.CanUndo;
        //assert
        Assert.IsFalse(result);
    }

    [Test]
    public void CanUndo_AfterAddingOneElement_ReturnsFalse()
    {
        //arrange
        _history.CurrentDirectory = _directory;
        //act
        var result = _history.CanUndo;
        //assert
        Assert.IsFalse(result);
    }
    
    [TestCase(1)]
    [TestCase(20)]
    [TestCase(5)]
    [TestCase(12)]
    [TestCase(2)]
    public void CanRedo_AfterAddingValuesAndNoUndo_ReturnsFalse(int valuesCount)
    {
        //arrange
        AddValuesToHistory(valuesCount);
        //act
        var result = _history.CanRedo;
        //assert
        Assert.IsFalse(result);
    }

    [Test]
    public void CanUndo_AfterAddingTwoElements_ReturnsTrue()
    {
        //arrange
        AddValuesToHistory(2);
        //act
        var result = _history.CanUndo;
        //assert
        Assert.IsTrue(result);
    }

    [Test]
    public void CanRedo_AfterUndo_ReturnsTrue()
    {
        //arrange
        AddValuesToHistory(2);
        //act
        _history.Undo();
        var result = _history.CanRedo;
        //assert
        Assert.IsTrue(result);
    }

    [Test]
    public void Redo_EmptyHistory_ThrowsException()
    {
        //arrange
        
        //assert and act
        Assert.Throws<CannotRedoException>(code: () => _history.Redo());
    }
    
    [Test]
    public void Undo_EmptyHistory_ThrowsException()
    {
        //arrange
        
        //assert and act
        Assert.Throws<CannotUndoException>(code: () => _history.Undo());
    }

 
    
    private void AddValuesToHistory(int count)
    {
        for (int i = 0; i != count; i++)
            _history.CurrentDirectory = _directory;
    }
    
}