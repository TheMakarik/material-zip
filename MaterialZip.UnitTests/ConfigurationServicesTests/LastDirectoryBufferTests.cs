using FakeItEasy;
using MaterialZip.Model.Exceptions;
using MaterialZip.Services.ConfigurationServices;
using MaterialZip.UnitTests.Core.Stubs;
using Serilog;

namespace MaterialZip.UnitTests.ConfigurationServicesTests;

[TestFixture]
public class LastDirectoryBufferTests
{
     private ILogger _logger;
    private LastDirectoryBuffer _buffer;

    [SetUp]
    public void SetUp()
    {
        _logger = A.Fake<ILogger>();
        _buffer = new LastDirectoryBuffer(_logger);
    }

    [Test]
    public void IsBufferEmpty_AfterCreating_IsTrue()
    {
        //arrange
        
        //act

        //assert
        Assert.IsTrue(_buffer.IsBufferEmpty);
    }

    [Test]
    public void IsBufferEmpty_AfterAddingDirectory_IsFalse()
    {
        //arrange
        var directory = FileEntityFactory.CreateDirectory().Path;
        //act
        _buffer.ToBuffer(directory);
        //assert
        Assert.IsFalse(_buffer.IsBufferEmpty);
    }

    [Test]
    public void IsBufferEmpty_AfterAddingAngGettingDirectory_StillFalse()
    {
        //arrange
        var directory = FileEntityFactory.CreateDirectory().Path;
        //act
        _buffer.ToBuffer(directory);
        var result = _buffer.FromBuffer();
        //assert
        Assert.IsFalse(_buffer.IsBufferEmpty);
    }

    [Test]
    public void FromBuffer_BufferIsNotEmpty_ReturnsDirectoryThatWasAddedToBuffer()
    {
        //arrange
        var directory = FileEntityFactory.CreateDirectory().Path;
        //act
        _buffer.ToBuffer(directory);
        var result = _buffer.FromBuffer();
        //assert
        Assert.That(result, Is.EqualTo(directory));
    }

    [Test]
    public void FromBuffer_WhenBufferIsEmpty_ThrowsException()
    {
        //arrange
        
        //assert and act
        Assert.Throws<DirectoryInBufferNotFoundException>(() =>
        {
            var result = _buffer.FromBuffer();
        });
    }

    [Test]
    public void FromBuffer_WhenBufferIsNotEmpty_LogInformation()
    {
        //arrange
        var directory = FileEntityFactory.CreateDirectory().Path;
        //act
        _buffer.ToBuffer(directory);
        var result = _buffer.FromBuffer();
        //assert
        A.CallTo(_logger)
            .Where(f => f.Method.Name == nameof(_logger.Information))
            .MustHaveHappenedOnceExactly();
    }

    [Test]
    public void ToBuffer_Always_LogDebug()
    {
        //arrange
        var directory = FileEntityFactory.CreateDirectory().Path;
        //act
        _buffer.ToBuffer(directory);
        //assert
        A.CallTo(_logger)
            .Where(f => f.Method.Name == nameof(_logger.Debug))
            .MustHaveHappenedOnceExactly();
    }
}