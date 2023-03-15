using System;

namespace Axwabo.Helpers;

/// <summary>
/// A range of values with a start and end value.
/// </summary>
/// <typeparam name="T">The type of the values. Must implement <see cref="IComparable"/>.</typeparam>
public struct ValueRange<T> where T : IComparable
{

    #region Create

    /// <summary>
    /// Creates a new range with the given start value.
    /// </summary>
    /// <param name="start">The start value.</param>
    /// <returns>The created range.</returns>
    public static ValueRange<T> StartOnly(T start) => new()
    {
        StartSpecified = true,
        Start = start
    };

    /// <summary>
    /// Creates a new range with the given end value.
    /// </summary>
    /// <param name="end">The end value.</param>
    /// <returns>The created range.</returns>
    public static ValueRange<T> EndOnly(T end) => new()
    {
        EndSpecified = true,
        End = end
    };

    /// <summary>
    /// Creates a new range with the given start and end values.
    /// </summary>
    /// <param name="start">The start value.</param>
    /// <param name="end">The end value.</param>
    /// <returns>The created range.</returns>
    public static ValueRange<T> Create(T start, T end) => new()
    {
        StartSpecified = true,
        Start = start,
        EndSpecified = true,
        End = end
    };

    #endregion

    #region Parse

    private const string Separator = "..";

    /// <summary>
    /// Attempts to parse a range from a string.
    /// </summary>
    /// <param name="value"></param>
    /// <param name="valueParser"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    /// <remarks>This method does not throw a <see cref="FormatException"/> upon failure as opposed to <see cref="Parse"/>.</remarks>
    /// <seealso cref="TryParseDelegate{T}"/>
    public static bool TryParse(string value, TryParseDelegate<T> valueParser, out ValueRange<T> range)
        => TryParseInternal(value, valueParser, out range, false);

    /// <summary>
    /// Parses a range from a string.
    /// </summary>
    /// <param name="value">The string to parse.</param>
    /// <param name="valueParser">The parser to use for the values.</param>
    /// <returns>The parsed range.</returns>
    /// <exception cref="FormatException">Thrown if the string is not a valid range or the value(s) could not be parsed using the <see cref="TryParseDelegate{T}"/>.</exception>
    /// <seealso cref="TryParse"/>
    /// <seealso cref="TryParseDelegate{T}"/>
    public static ValueRange<T> Parse(string value, TryParseDelegate<T> valueParser)
        => TryParseInternal(value, valueParser, out var range, true)
            ? range
            : throw new FormatException($"Invalid range: {value}");

    private static bool TryParseInternal(string value, TryParseDelegate<T> valueParser, out ValueRange<T> range, bool throwOnInvalid)
    {
        if (value.Contains(Separator))
            return TryParseWithRange(value, valueParser, out range, throwOnInvalid);
        if (!valueParser(value, out var parsed))
        {
            if (throwOnInvalid)
                throw new FormatException($"Invalid range value (type {typeof(T).FullName}): {value}");
            range = default;
            return false;
        }

        range = new ValueRange<T>
        {
            StartSpecified = true,
            EndSpecified = true,
            Start = parsed,
            End = parsed
        };
        return true;
    }

    private static bool TryParseWithRange(string value, TryParseDelegate<T> valueParser, out ValueRange<T> range, bool throwOnInvalid)
    {
        T start = default;
        T end = default;
        var startSet = false;
        var endSet = false;
        if (value.StartsWith(Separator))
            endSet = ParseValue(value.Substring(2), nameof(end), valueParser, throwOnInvalid, out end);
        else if (value.EndsWith(Separator))
            startSet = ParseValue(value.Substring(0, value.Length - 2), nameof(start), valueParser, throwOnInvalid, out start);
        else
        {
            var splitIndex = value.IndexOf(Separator, StringComparison.Ordinal);
            startSet = ParseValue(value.Substring(0, splitIndex), nameof(start), valueParser, throwOnInvalid, out start);
            endSet = ParseValue(value.Substring(splitIndex + 2), nameof(end), valueParser, throwOnInvalid, out end);
        }

        if (!startSet && !endSet)
        {
            range = default;
            return false;
        }

        range = new ValueRange<T>
        {
            StartSpecified = startSet,
            EndSpecified = endSet,
            Start = start,
            End = end
        };
        return true;
    }

    private static bool ParseValue(string value, string variableName, TryParseDelegate<T> valueParser, bool throwOnInvalid, out T start)
    {
        var startSet = valueParser(value, out start);
        if (!startSet && throwOnInvalid)
            throw new FormatException($"Invalid value for {variableName} (type {typeof(T).FullName}): {value}");
        return startSet;
    }

    #endregion

    #region Members

    /// <summary>The start of the range.</summary>
    public T Start;

    /// <summary>The end of the range.</summary>
    public T End;

    /// <summary>Whether the start of the range is specified.</summary>
    public bool StartSpecified;

    /// <summary>Whether the end of the range is specified.</summary>
    public bool EndSpecified;

    /// <summary>
    /// Determines whether the given item is within the range.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns><see langword="true" /> if the value is within range; otherwise, <see langword="false" />.</returns>
    public bool IsWithinRange(T item) => (!StartSpecified || item.CompareTo(Start) >= 0) && (!EndSpecified || item.CompareTo(End) <= 0);

    #endregion

}
