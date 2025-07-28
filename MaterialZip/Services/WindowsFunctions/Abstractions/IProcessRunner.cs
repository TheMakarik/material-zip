using System.Diagnostics;

namespace MaterialZip.Services.WindowsFunctions.Abstractions;

/// <summary>
/// Represent default proxy abstractions of <see cref="Process.Start(string)"/>, <see cref="Process.Start(string, IEnumerable{string})"/> and <see cref="Process.Start(ProcessStartInfo)"/> methods
/// </summary>
public interface IProcessRunner
{
    /// <inheritdoc cref="Process.Start(string)"/>
    public Process Run(string path);
    
    /// <inheritdoc cref="Process.Start(ProcessStartInfo)"/>
    public Process? Run(ProcessStartInfo processStartInfo);
    
    /// <inheritdoc cref="Process.Start(string, IEnumerable{string})"/>
    public Process Run(string path, string[] args);
}