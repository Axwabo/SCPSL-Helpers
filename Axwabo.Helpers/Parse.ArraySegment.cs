using System;
using PlayerRoles;

namespace Axwabo.Helpers;

public static partial class Parse
{

    /// <summary>
    /// Attempts to parse the given string as an integer.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="result">The result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully.</returns>
    public static bool ParseInt(this ArraySegment<string> segment, out int result, int index = 0) => Int(segment.At(index), out result);

    /// <summary>
    /// Attempts to parse the given string as an integer, and checks if it is within the given range.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool ParseInt(this ArraySegment<string> segment, ValueRange<int> range, out int result, int index = 0) => Int(segment.At(index), range, out result);

    /// <summary>
    /// Attempts to parse the given string as a float.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="result">The result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully.</returns>
    public static bool ParseFloat(this ArraySegment<string> segment, out float result, int index = 0) => Float(segment.At(index), out result);

    /// <summary>
    /// Attempts to parse the given string as a float, and checks if it is within the given range.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool ParseFloat(this ArraySegment<string> segment, ValueRange<float> range, out float result, int index = 0) => Float(segment.At(index), range, out result);

    /// <summary>
    /// Attempts to parse the given string as a byte.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="result">The result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully.</returns>
    public static bool ParseByte(this ArraySegment<string> segment, out byte result, int index = 0) => Byte(segment.At(index), out result);

    /// <summary>
    /// Attempts to parse the given string as a byte, and checks if it is within the given range.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool ParseByte(this ArraySegment<string> segment, ValueRange<byte> range, out byte result, int index = 0) => Byte(segment.At(index), range, out result);

    /// <summary>
    /// Attempts to parse the given string as an enum value, ignoring case.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="result">The result.</param>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully.</returns>
    [Obsolete("Use ParseEnumIgnoreCase instead.")]
    public static bool EnumIgnoreCase<T>(this ArraySegment<string> segment, out T result, int index = 0) where T : struct
        => ParseEnumIgnoreCase(segment, out result, index);

    /// <summary>
    /// Attempts to parse the given string as an enum value, ignoring case, and checks if it is within the given range.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    [Obsolete("Use ParseEnumIgnoreCase instead.")]
    public static bool EnumIgnoreCase<T>(this ArraySegment<string> segment, ValueRange<T> range, out T result, int index = 0) where T : struct, IComparable
        => ParseEnumIgnoreCase(segment, range, out result, index);

    /// <summary>
    /// Attempts to parse the given string as an enum value, ignoring case.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="result">The result.</param>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully.</returns>
    public static bool ParseEnumIgnoreCase<T>(this ArraySegment<string> segment, out T result, int index = 0) where T : struct => EnumIgnoreCase(segment.At(index), out result);

    /// <summary>
    /// Attempts to parse the given string as an enum value, ignoring case, and checks if it is within the given range.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool ParseEnumIgnoreCase<T>(this ArraySegment<string> segment, ValueRange<T> range, out T result, int index = 0) where T : struct, IComparable
        => EnumIgnoreCase(segment.At(index), range, out result);

    /// <summary>
    /// Attempts to parse the given string as an <see cref="ItemType"/>, ignoring case.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="result">The result.</param>
    /// <param name="includeNone">Whether to include <see cref="ItemType.None"/> as a valid result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully and the result is not <see cref="ItemType.None"/>.</returns>
    public static bool ParseItem(this ArraySegment<string> segment, out ItemType result, int index = 0, bool includeNone = false) => Item(segment.At(index), out result, includeNone);

    /// <summary>
    /// Attempts to parse the given string as an <see cref="ItemType"/>, ignoring case, and checks if it is within the given range.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool ParseItem(this ArraySegment<string> segment, ValueRange<ItemType> range, out ItemType result, int index = 0) => Item(segment.At(index), range, out result);

    /// <summary>
    /// Attempts to parse the given string as a <see cref="RoleTypeId"/>, ignoring case.
    /// </summary>
    /// <param name="segment">The ArraySegment containing the string.</param>
    /// <param name="result">The result.</param>
    /// <param name="includeNone">Whether to include <see cref="RoleTypeId.None"/> as a valid result.</param>
    /// <param name="index">The index of the string to parse in the ArraySegment.</param>
    /// <returns>Whether the string was parsed successfully and the result is not <see cref="RoleTypeId.None"/>.</returns>
    public static bool ParseRole(this ArraySegment<string> segment, out RoleTypeId result, int index = 0, bool includeNone = false) => Role(segment.At(index), out result, includeNone);

}
