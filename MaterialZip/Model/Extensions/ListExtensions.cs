using System.Collections.Generic;

namespace MaterialZip.Model.Extensions;

/// <summary>
/// Represent <see cref="List{T}"/> extensions class
/// </summary>
public static class ListExtensions
{
    /// <summary>
    /// Get new list of index range <see cref="list"/>
    /// </summary>
    /// <param name="list"><see cref="List{T}"/> instance</param>
    /// <param name="startIndex">index where sublist starts</param>
    /// <param name="endIndex">index where sublist ends</param>
    /// <typeparam name="T">Generic of <see cref="List{T}"/></typeparam>
    /// <returns>Sub list of index range <see cref="startIndex"/> and <see cref="endIndex"/></returns>
    public static List<T> SubList<T>(this List<T> list, int startIndex, int endIndex)
    {
        // Stackoverflow question solved https://stackoverflow.com/questions/79696604/how-to-get-sub-list-in-c-sharp
        return list.GetRange(startIndex, endIndex - startIndex);
    }

}