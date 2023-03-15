namespace Axwabo.Helpers;

/// <summary>
/// A method that attempts to parse a value from a string.
/// </summary>
/// <typeparam name="T">The type of the value to parse.</typeparam>
public delegate bool TryParseDelegate<T>(string value, out T result);
