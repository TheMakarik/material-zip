using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using MaterialZip.Model.Extensions;

namespace MaterialZip.Benchmarks.Benchs;

[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class ListExtensionsSubListBench
{
    private List<int> _list = [3, 2, 5, 4, 2, 5, 6, 3, 1];
    private const int StartIndex = 0;
    private const int EndIndex = 5;
 
    
    [Benchmark]
    public void OldSubList()
    {
        var subList = _list.ToArray()[0..5].ToList();
    }
    
    [Benchmark]
    public void NewSubList()
    {
        var subList = _list.SubList(StartIndex, EndIndex);
    }
}