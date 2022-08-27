using System;
using System.Linq;
using System.Reflection;
using static Axwabo.Helpers.Config.Translations.AttributeHandler;

namespace Axwabo.Helpers.Config.Translations {

    /// <summary>
    /// Helper methods for using the <see cref="TranslationRegistry{T}"/>.
    /// </summary>
    public static class TranslationHelper {

        /// <summary>
        /// The non-generic type of the registry.
        /// </summary>
        public static readonly Type TranslationRegistryType = typeof(TranslationHelper).Assembly.GetType("Axwabo.Helpers.Config.Translations.TranslationRegistry`1");

        #region Translating

        /// <inheritdoc cref="TranslationRegistry{T}.TryGetTranslation"/>
        /// <typeparam name="T">The type of the translation key.</typeparam>
        public static bool TryGetTranslation<T>(this T key, out string translation) where T : Enum => TranslationRegistry<T>.TryGetTranslation(key, out translation);

        /// <inheritdoc cref="TranslationRegistry{T}.Translate(T)"/>
        /// <typeparam name="T">The type of the translation key.</typeparam>
        public static string Translate<T>(this T key) where T : Enum => TranslationRegistry<T>.Translate(key);

        /// <inheritdoc cref="TranslationRegistry{T}.Translate(T, object)"/>
        /// <typeparam name="T">The type of the translation key.</typeparam>
        public static string Translate<T>(this T key, object arg0) where T : Enum => TranslationRegistry<T>.Translate(key, arg0);

        /// <inheritdoc cref="TranslationRegistry{T}.Translate(T, object, object)"/>
        /// <typeparam name="T">The type of the translation key.</typeparam>
        public static string Translate<T>(this T key, object arg0, object arg1) where T : Enum => TranslationRegistry<T>.Translate(key, arg0, arg1);

        /// <inheritdoc cref="TranslationRegistry{T}.Translate(T, object, object, object)"/>
        /// <typeparam name="T">The type of the translation key.</typeparam>
        public static string Translate<T>(this T key, object arg0, object arg1, object arg2) where T : Enum => TranslationRegistry<T>.Translate(key, arg0, arg1, arg2);

        /// <inheritdoc cref="TranslationRegistry{T}.Translate(T, object[])"/>
        /// <typeparam name="T">The type of the translation key.</typeparam>
        public static string Translate<T>(this T key, params object[] args) where T : Enum => TranslationRegistry<T>.Translate(key, args);

        /// <inheritdoc cref="TranslationRegistry{T}.RegisterTranslation"/>
        /// <typeparam name="T">The type of the translation key.</typeparam>

        #endregion

        #region Registering

        public static void RegisterTranslation<T>(this T key, string translation) where T : Enum => TranslationRegistry<T>.RegisterTranslation(key, translation);

        /// <summary>
        /// Registers all translations flagged with <see cref="TranslationAttribute"/> in an object of key type <typeparamref name="T"/>.
        /// </summary>
        /// <param name="containingClass">The object containing the translations.</param>
        /// <typeparam name="T">The type of the translation key.</typeparam>
        /// <returns>The number of translations registered.</returns>
        public static int RegisterAllTranslations<T>(object containingClass) where T : Enum {
            if (containingClass == null)
                return 0;
            var count = 0;
            foreach (var property in containingClass.GetType().GetProperties().Where(e => e.CanRead))
            foreach (var attribute in property.GetCustomAttributes(false))
                count += RegisterAttributeProperty<T>(attribute, containingClass, property);

            foreach (var field in containingClass.GetType().GetFields())
            foreach (var attribute in field.GetCustomAttributes(false))
                count += RegisterAttributeField<T>(attribute, containingClass, field);

            return count;
        }

        /// <summary>
        /// Registers all translations flagged with <see cref="TranslationAttribute"/> in an object.
        /// </summary>
        /// <param name="containingClass">The object containing the translations.</param>
        /// <returns>The number of translations registered.</returns>
        public static int RegisterAllTranslations(object containingClass) {
            if (containingClass == null)
                return 0;
            var count = 0;
            foreach (var property in containingClass.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(e => e.CanRead))
            foreach (var attribute in property.GetCustomAttributes(false))
                count += RegisterAttributeProperty(attribute, containingClass, property);

            foreach (var field in containingClass.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            foreach (var attribute in field.GetCustomAttributes(false))
                count += RegisterAttributeField(attribute, containingClass, field);

            return count;
        }

        /// <summary>
        /// Registers all static translations flagged with <see cref="TranslationAttribute"/> in the given type.
        /// </summary>
        /// <typeparam name="T">The type containing translations.</typeparam>
        /// <returns>The number of translations registered.</returns>
        public static int RegisterAllStaticTranslations<T>() => RegisterAllStaticTranslations(typeof(T));

        /// <summary>
        /// Registers all static translations flagged with <see cref="TranslationAttribute"/> in the given type.
        /// </summary>
        /// <param name="type">The type containing translations.</param>
        /// <returns>The number of translations registered.</returns>
        public static int RegisterAllStaticTranslations(Type type) {
            if (type == null)
                return 0;
            var count = 0;
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(e => e.CanRead))
            foreach (var attribute in property.GetCustomAttributes(false))
                count += RegisterAttributePropertyStatic(attribute, property);

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            foreach (var attribute in field.GetCustomAttributes(false))
                count += RegisterAttributeFieldStatic(attribute, field);

            return count;
        }

        #endregion

        #region Unregistering

        /// <inheritdoc cref="TranslationRegistry{T}.UnregisterTranslation"/>
        public static bool UnregisterTranslation<T>(this T key) where T : Enum => TranslationRegistry<T>.UnregisterTranslation(key);

        /// <summary>
        /// Unregisters all translations.
        /// </summary>
        /// <typeparam name="T">The type of the translation keys to clear.</typeparam>
        public static void UnregisterAllTranslations<T>() where T : Enum => TranslationRegistry<T>.Clear();

        /// <summary>
        /// Unregisters all translations contained in an object.
        /// </summary>
        /// <param name="containingClass">The object containing the translations.</param>
        /// <returns>The number of translations unregistered.</returns>
        public static int UnregisterAllTranslations(object containingClass) {
            if (containingClass == null)
                return 0;
            var count = 0;
            foreach (var property in containingClass.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            foreach (var attribute in property.GetCustomAttributes(false))
                count += UnregisterAttributeProperty(attribute, containingClass, property);

            foreach (var field in containingClass.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            foreach (var attribute in field.GetCustomAttributes(false))
                count += UnregisterAttributeField(attribute, containingClass, field);

            return count;
        }

        /// <summary>
        /// Unregisters all static translations in the given type.
        /// </summary>
        /// <typeparam name="T">The type containing translations.</typeparam>
        /// <returns>The number of translations unregistered.</returns>
        public static int UnregisterAllStaticTranslations<T>() => UnregisterAllStaticTranslations(typeof(T));

        /// <summary>
        /// Unregisters all static translations in the given type.
        /// </summary>
        /// <param name="type">The type containing translations.</param>
        /// <returns>The number of translations unregistered.</returns>
        private static int UnregisterAllStaticTranslations(Type type) {
            if (type == null)
                return 0;
            var count = 0;
            foreach (var property in type.GetProperties(BindingFlags.Public | BindingFlags.Static))
            foreach (var attribute in property.GetCustomAttributes(false))
                count += UnregisterAttributePropertyStatic(attribute, property);

            foreach (var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
            foreach (var attribute in field.GetCustomAttributes(false))
                count += UnregisterAttributeFieldStatic(attribute, field);

            return count;
        }

        #endregion

    }

}
