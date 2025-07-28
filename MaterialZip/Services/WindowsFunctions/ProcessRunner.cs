using System.Diagnostics;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.Services.WindowsFunctions;

/// <summary>
/// Represent default proxy of <see cref="Process.Start(string)"/>, <see cref="Process.Start(string, IEnumerable{string})"/> and <see cref="Process.Start(ProcessStartInfo)"/> methods
/// </summary>
public class ProcessRunner : IProcessRunner
{
    public Process Run(string path)
    { 
        return Process.Start(path);
    }

    public Process? Run(ProcessStartInfo processStartInfo)
    {
        return Process.Start(processStartInfo);
    }

    public Process Run(string path, string[] args)
    {
        return Process.Start(path, args);
    }
}