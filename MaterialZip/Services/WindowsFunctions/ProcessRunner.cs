using System.Diagnostics;
using MaterialZip.Services.WindowsFunctions.Abstractions;

namespace MaterialZip.Services.WindowsFunctions;

/// <summary>
/// Represent default proxy of <see cref="Process.Start(string)"/>, <see cref="Process.Start(string, IEnumerable{string})"/> and <see cref="Process.Start(ProcessStartInfo)"/> methods
/// </summary>
public class ProcessRunner : IProcessRunner
{
    /// <inheritdoc cref="IProcessRunner.Run(string)"/>
    public Process Run(string path)
    { 
        return Process.Start(path);
    }

    /// <inheritdoc cref="IProcessRunner.Run(ProcessStartInfo)"/>
    public Process? Run(ProcessStartInfo processStartInfo)
    {
        return Process.Start(processStartInfo);
    }

    /// <inheritdoc cref="IProcessRunner.Run(string, string[])"/>
    public Process Run(string path, string[] args)
    {
        return Process.Start(path, args);
    }
}