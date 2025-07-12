using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using MaterialZip.Benchmarks.Model.FileEntities;

namespace MaterialZip.Benchmarks.Benchs;

[MemoryDiagnoser]
[InProcess]
[Orderer(SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class BestFileEntityBench
{
    [Benchmark]
    public void FileEntityClass()
    {
        FileEntityClass[] drives = Directory.GetLogicalDrives()
            .Select(p => new FileEntityClass(p, true))
            .ToArray();
        var drivesContent = new List<FileEntityClass>();
        foreach (var drive in drives[..1])
        {
            var files = Directory.GetFiles(drive.Path)
                .Select(p => new FileEntityClass(p, false));
            var directories = Directory.GetDirectories(drive.Path)
                .Select(p => new FileEntityClass(p, false));
            
            drivesContent.AddRange(directories);
            drivesContent.AddRange(files);
        }
    }
    
    [Benchmark]
    public void FileEntityStruct()
    {
        FileEntityStruct[] drives = Directory.GetLogicalDrives()
            .Select(p => new FileEntityStruct(p, true))
            .ToArray();
        var drivesContent = new List<FileEntityStruct>();
        foreach (var drive in drives[..1])
        {
            var files = Directory.GetFiles(drive.Path)
                .Select(p => new FileEntityStruct(p, false));
            var directories = Directory.GetDirectories(drive.Path)
                .Select(p => new FileEntityStruct(p, false));
            
            drivesContent.AddRange(directories);
            drivesContent.AddRange(files);
        }
    }
    
    [Benchmark]
    public void FileEntityRecord()
    {
        FileEntityRecord[] drives = Directory.GetLogicalDrives()
            .Select(p => new FileEntityRecord(p, true))
            .ToArray();
        var drivesContent = new List<FileEntityRecord>();
        foreach (var drive in drives[..1])
        {
            var files = Directory.GetFiles(drive.Path)
                .Select(p => new FileEntityRecord(p, false));
            var directories = Directory.GetDirectories(drive.Path)
                .Select(p => new FileEntityRecord(p, false));
            
            drivesContent.AddRange(directories);
            drivesContent.AddRange(files);
        }
    }
    
    [Benchmark]
    public void FileEntityRecordStruct()
    {
        FileEntityRecordStruct[] drives = Directory.GetLogicalDrives()
            .Select(p => new FileEntityRecordStruct(p, true))
            .ToArray();
        var drivesContent = new List<FileEntityRecordStruct>();
        foreach (var drive in drives[..1])
        {
            var files = Directory.GetFiles(drive.Path)
                .Select(p => new FileEntityRecordStruct(p, false));
            var directories = Directory.GetDirectories(drive.Path)
                .Select(p => new FileEntityRecordStruct(p, false));
            
            drivesContent.AddRange(directories);
            drivesContent.AddRange(files);
        }
    }
}