namespace Axwabo.Helpers.Config.Translations;

/// <summary>
/// A translation provider that uses a dictionary of translations using enums.
/// </summary>
/// <typeparam name="T">The enum type to use for the translation keys.</typeparam>
public static class TranslationRegistry<T> where T : Enum
{

    private static readonly Dictionary<T, string> Translations = new();

    /// <summary>
    /// Attempts to get the registered translation for the given enum value.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <param name="translation">The translation, if found.</param>
    /// <returns>Whether a translation with the given key was registered.</returns>
    public static bool TryGetTranslation(T key, out string translation) => Translations.TryGetValue(key, out translation);

    /// <summary>
    /// Translates the given enum value using registered translations.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <returns>The translation associated with the key.</returns>
    /// <remarks>Returns the key as a string, if the translation was not found.</remarks>
    public static string Translate(T key) => !TryGetTranslation(key, out var translation) ? key.ToString() : translation;

    /// <summary>
    /// Uses <see cref="string.Format(string,object)"/> to translate the given enum value using registered translations.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <param name="arg0">The argument to format the translation with.</param>
    /// <returns>The formatted translation associated with the key.</returns>
    public static string Translate(T key, object arg0) => !TryGetTranslation(key, out var translation) ? key.ToString() : string.Format(translation, arg0);

    /// <summary>
    /// Uses <see cref="string.Format(string,object,object)"/> to translate the given enum value using registered translations.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <param name="arg0">The first argument to format the translation with.</param>
    /// <param name="arg1">The second argument to format the translation with.</param>
    /// <returns>The formatted translation associated with the key.</returns>
    public static string Translate(T key, object arg0, object arg1) => !TryGetTranslation(key, out var translation) ? key.ToString() : string.Format(translation, arg0, arg1);

    /// <summary>
    /// Uses <see cref="string.Format(string,object,object,object)"/> to translate the given enum value using registered translations.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <param name="arg0">The first argument to format the translation with.</param>
    /// <param name="arg1">The second argument to format the translation with.</param>
    /// <param name="arg2">The third argument to format the translation with.</param>
    /// <returns>The formatted translation associated with the key.</returns>
    public static string Translate(T key, object arg0, object arg1, object arg2) => !TryGetTranslation(key, out var translation) ? key.ToString() : string.Format(translation, arg0, arg1, arg2);

    /// <summary>
    /// Uses <see cref="string.Format(string,object[])"/> to translate the given enum value using registered translations.
    /// </summary>
    /// <param name="key">The translation key.</param>
    /// <param name="args">The arguments to format the translation with.</param>
    /// <returns>The formatted translation associated with the key.</returns>
    public static string Translate(T key, params object[] args) => !TryGetTranslation(key, out var translation) ? key.ToString() : string.Format(translation, args);

    /// <summary>
    /// Registers a translation for the given enum value.
    /// </summary>
    /// <param name="key">The translation key to register.</param>
    /// <param name="translation">The translation to associate with the key.</param>
    public static void RegisterTranslation(T key, string translation) => Translations[key] = translation;

    /// <summary>
    /// Removes the registered translation for the given enum value.
    /// </summary>
    /// <param name="key">The translation key to remove.</param>
    /// <returns>Whether the translation with the given key was removed.</returns>
    public static bool UnregisterTranslation(T key) => Translations.Remove(key);

    /// <summary>Removes all registered translations.</summary>
    public static void Clear() => Translations.Clear();

}
