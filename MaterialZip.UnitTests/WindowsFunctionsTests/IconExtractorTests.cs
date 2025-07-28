using System.Drawing;
using System.Windows.Media.Imaging;
using FakeItEasy;
using MaterialZip.Services.WindowsFunctions;
using MaterialZip.Services.WindowsFunctions.Abstractions;
using MaterialZip.UnitTests.Core.Stubs;
using Serilog;

namespace MaterialZip.UnitTests.WindowsFunctionsTests;

[TestFixture]
public class IconExtractorTests
{
    private IAssociatedIconExtractor _extractor;
    private ILogger _logger;
    private IBitmapSourceBuilder _builder;

    [SetUp]
    public void SetUp()
    {
        _extractor = A.Fake<IAssociatedIconExtractor>();
        _logger = A.Fake<ILogger>();
        _builder = A.Fake<IBitmapSourceBuilder>();
    }


    [Test]
    public void FromPath_WithCorrectPath_ReturnBitmapFromIcon()
    {
        //arrange
        var bitmap = A.Dummy<BitmapSource>();
        var path = FileEntityFactory.CreateFile().Path;
        var dummyIcon = Icon.FromHandle(IntPtr.MinValue);
        A.CallTo(() => _extractor.Extract(path)).Returns(dummyIcon);
        A.CallTo(() => _builder.Build(dummyIcon)).Returns(bitmap);
        var iconExtractor = new IconExtractor(_logger, _extractor, _builder);
        //act
        var expectedBitMap = iconExtractor.FromPath(path);
        //assert
        Assert.That(bitmap, Is.EqualTo(expectedBitMap));
    }

    [Test]
    public void FromPath_IconIsNull_ReturnNull()
    {
        //arrange
        var path = FileEntityFactory.CreateFile().Path;
        A.CallTo(() => _extractor.Extract(path)).Returns(null);
        var iconExtractor = new IconExtractor(_logger, _extractor, _builder);
        //act
        var bitmapSource = iconExtractor.FromPath(path);
        //assert
        Assert.IsNull(bitmapSource);
    }

    [Test]
    public void FromPath_IconIsNull_LogWarning()
    {
        //arrange
        var path = FileEntityFactory.CreateFile().Path;
        A.CallTo(() => _extractor.Extract(path)).Returns(null);
        var iconExtractor = new IconExtractor(_logger, _extractor, _builder);
        //act
        var bitmapSource = iconExtractor.FromPath(path);
        //assert
        A.CallTo(_logger)
            .Where(f => f.Method.Name == nameof(_logger.Warning))
            .MustHaveHappenedOnceExactly();
    }
    
    [Test]
    public void FromPath_ExceptionOccurred_LogError()
    {
        //arrange
        var path = FileEntityFactory.CreateFile().Path;
        A.CallTo(() => _extractor.Extract(path)).Throws<Exception>();
        var iconExtractor = new IconExtractor(_logger, _extractor, _builder);
        //act
        var bitmapSource = iconExtractor.FromPath(path);
        //assert
        A.CallTo(_logger)
            .Where(f => f.Method.Name == nameof(_logger.Error))
            .MustHaveHappenedOnceExactly();
    }
    
    [Test]
    public void FromPath_ExceptionOccured_ReturnNull()
    {
        //arrange
        var path = FileEntityFactory.CreateFile().Path;
        A.CallTo(() => _extractor.Extract(path)).Throws<Exception>();
        var iconExtractor = new IconExtractor(_logger, _extractor, _builder);
        //act
        var bitmapSource = iconExtractor.FromPath(path);
        //assert
        Assert.IsNull(bitmapSource);
    }
    
}