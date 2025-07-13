using BenchmarkDotNet.Running;
using MaterialZip.Benchmarks.Benchs;

#if !DEBUG
//Run specific benchmark
BenchmarkRunner.Run<EnumConvertingBench>();
Console.ReadKey();
#endif