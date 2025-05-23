﻿using PlayerRoles;

namespace Axwabo.Helpers;

/// <summary>Parser methods for common types.</summary>
public static partial class Parse
{

    /// <summary>
    /// Attempts to parse the given string as an integer.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully.</returns>
    public static bool Int(string value, out int result) => int.TryParse(value.Trim(), out result);

    /// <summary>
    /// Attempts to parse the given string as an integer, and checks if it is within the given range.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool Int(string value, ValueRange<int> range, out int result) => Int(value, out result) && range.IsWithinRange(result);

    /// <summary>
    /// Attempts to parse the given string as a float.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully.</returns>
    public static bool Float(string value, out float result) => float.TryParse(value.Trim(), out result);

    /// <summary>
    /// Attempts to parse the given string as a float, and checks if it is within the given range.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool Float(string value, ValueRange<float> range, out float result) => Float(value, out result) && range.IsWithinRange(result);

    /// <summary>
    /// Attempts to parse the given string as a byte.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully.</returns>
    public static bool Byte(string value, out byte result) => byte.TryParse(value.Trim(), out result);

    /// <summary>
    /// Attempts to parse the given string as a byte, and checks if it is within the given range.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool Byte(string value, ValueRange<byte> range, out byte result) => Byte(value, out result) && range.IsWithinRange(result);

    /// <summary>
    /// Attempts to parse the given string as an enum value, ignoring case.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result.</param>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <returns>Whether the string was parsed successfully.</returns>
    public static bool EnumIgnoreCase<T>(string value, out T result) where T : struct => Enum.TryParse(value.Trim(), true, out result);

    /// <summary>
    /// Attempts to parse the given string as an enum value, ignoring case, and checks if it is within the given range.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool EnumIgnoreCase<T>(string value, ValueRange<T> range, out T result) where T : struct, IComparable
        => EnumIgnoreCase(value, out result) && range.IsWithinRange(result);

    /// <summary>
    /// Attempts to parse the given string as an <see cref="ItemType"/>, ignoring case.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully and the result is not <see cref="ItemType.None"/>.</returns>
    public static bool Item(string value, out ItemType result) => EnumIgnoreCase(value, out result) && result != ItemType.None;

    /// <summary>
    /// Attempts to parse the given string as an <see cref="ItemType"/>, ignoring case.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result.</param>
    /// <param name="includeNone">Whether to include <see cref="ItemType.None"/> as a valid result.</param>
    /// <returns>Whether the string was parsed successfully and the result is not <see cref="ItemType.None"/>.</returns>
    public static bool Item(string value, out ItemType result, bool includeNone) => EnumIgnoreCase(value, out result) && (result != ItemType.None || includeNone);

    /// <summary>
    /// Attempts to parse the given string as an <see cref="ItemType"/>, ignoring case, and checks if it is within the given range.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="range">The range to check.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully and is within the given range.</returns>
    public static bool Item(string value, ValueRange<ItemType> range, out ItemType result) => Item(value, out result) && range.IsWithinRange(result);

    /// <summary>
    /// Attempts to parse the given string as a <see cref="RoleTypeId"/>, ignoring case.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result.</param>
    /// <returns>Whether the string was parsed successfully and the result is not <see cref="RoleTypeId.None"/>.</returns>
    public static bool Role(string value, out RoleTypeId result) => EnumIgnoreCase(value, out result) && result != RoleTypeId.None;

    /// <summary>
    /// Attempts to parse the given string as a <see cref="RoleTypeId"/>, ignoring case.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="result">The result.</param>
    /// <param name="includeNone">Whether to include <see cref="RoleTypeId.None"/> as a valid result.</param>
    /// <returns>Whether the string was parsed successfully and the result is not <see cref="RoleTypeId.None"/>.</returns>
    public static bool Role(string value, out RoleTypeId result, bool includeNone) => EnumIgnoreCase(value, out result) && (result != RoleTypeId.None || includeNone);

}
