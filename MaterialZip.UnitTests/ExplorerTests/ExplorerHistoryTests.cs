using FakeItEasy;
using MaterialZip.Services.ExplorerServices.Abstractions;
using Serilog;

namespace MaterialZip.UnitTests;

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
    public void ()
    {
        //arrange
        
        //act

        //assert

    }
    
}