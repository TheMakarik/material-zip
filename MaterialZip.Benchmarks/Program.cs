using BenchmarkDotNet.Running;
using MaterialZip.Benchmarks.Benchs;

#if !DEBUG
//Run specific benchmark
BenchmarkRunner.Run<BestFileEntityBench>();
Console.ReadKey();
#endif