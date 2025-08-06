using System.IO;

namespace MaterialZip.Extensions;

/// <summary>
/// Represents string extensions collection
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Replace alt <see cref="Path.AltDirectorySeparatorChar"/> to <see cref="Path.DirectorySeparatorChar"/> in <see cref="path"/>
    /// </summary>
    /// <param name="path">string to replace separators</param>
    /// <returns>string <see cref="path"/> with replaced separators</returns>
    public static string ReplaceAltDirectorySeparator(this string path)
        => path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
}