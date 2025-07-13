using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using MaterialDesignColors;
using static MaterialZip.Model.Enums.MaterialColor;

namespace MaterialZip.Benchmarks.Benchs;

[MemoryDiagnoser]
[InProcess]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class EnumConvertingBench
{
    [Benchmark]
    public void WithReflection()
    {
        var primaryColor = Enum.Parse(typeof(PrimaryColor), Amber.ToString()) as PrimaryColor?;
    }
    
    [Benchmark]
    public void WithSwitch()
    {
        var color = Amber;

        PrimaryColor? primaryColor = color switch
        {
            Red => PrimaryColor.Red,
            Pink => PrimaryColor.Pink,
            Purple => PrimaryColor.Purple,
            DeepPurple => PrimaryColor.DeepPurple,
            Indigo => PrimaryColor.Indigo,
            Blue => PrimaryColor.Blue,
            LightBlue => PrimaryColor.LightBlue,
            Cyan => PrimaryColor.Cyan,
            Teal => PrimaryColor.Teal,
            Green => PrimaryColor.Green,
            LightGreen => PrimaryColor.LightGreen,
            Lime => PrimaryColor.Lime,
            Yellow => PrimaryColor.Yellow,
            Amber => PrimaryColor.Amber,
            Orange => PrimaryColor.Orange,
            DeepOrange => PrimaryColor.DeepOrange,
            _ => null
        };
    }
}