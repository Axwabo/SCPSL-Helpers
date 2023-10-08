using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace Axwabo.Helpers;

/// <summary>
/// Extension methods for quicker use of Reflection.
/// </summary>
public static class ReflectionHelper
{

    #region Get-set explicit type

    /// <summary>
    /// Gets the value of the instance field with the specified name.
    /// </summary>
    /// <param name="type">The type of the object containing the field.</param>
    /// <param name="obj">The object to get the value from.</param>
    /// <param name="field">The name of the instance field.</param>
    /// <returns>The obtained object.</returns>
    public static object Get(this Type type, object obj, string field)
    {
        var fieldInfo = AccessTools.Field(type, field);
        return fieldInfo?.GetValue(obj);
    }

    /// <summary>
    /// Gets the value of the instance field with the specified name as type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="type">The type of the object containing the field.</param>
    /// <param name="obj">The object to get the value from.</param>
    /// <param name="field">The name of the instance field.</param>
    /// <typeparam name="T">The type of the field's value.</typeparam>
    /// <returns>The obtained <typeparamref name="T"/> type object.</returns>
    /// <exception cref="InvalidCastException">Thrown if the field's value is not of type <typeparamref name="T"/>.</exception>
    public static T Get<T>(this Type type, object obj, string field) => (T) Get(type, obj, field);

    /// <summary>
    /// Sets the value of the instance field with the specified name.
    /// </summary>
    /// <param name="type">The type of the object containing the field.</param>
    /// <param name="obj">The object to set the value on.</param>
    /// <param name="field">The name of the instance field.</param>
    /// <param name="value">The new value to set.</param>
    /// <returns>The object itself.</returns>
    public static object Set(this Type type, object obj, string field, object value)
    {
        var fieldInfo = AccessTools.Field(type, field);
        if (fieldInfo != null)
            fieldInfo.SetValue(obj, value);
        return obj;
    }

    /// <summary>
    /// Gets the value of the instance property with the specified name.
    /// </summary>
    /// <param name="type">The type of the object containing the property.</param>
    /// <param name="obj">The object to get the value from.</param>
    /// <param name="property">The name of the instance property.</param>
    /// <returns>The obtained object.</returns>
    public static object GetProp(this Type type, object obj, string property)
    {
        var getter = AccessTools.PropertyGetter(type, property);
        return getter == null ? null : getter.Invoke(obj, Array.Empty<object>());
    }

    /// <summary>
    /// Sets the value of the instance property with the specified name.
    /// </summary>
    /// <param name="type">The type of the object containing the property.</param>
    /// <param name="obj">The object to set the value on.</param>
    /// <param name="property">The name of the instance property.</param>
    /// <param name="value">The new value to set.</param>
    /// <returns>The object itself.</returns>
    public static object SetProp(this Type type, object obj, string property, object value)
    {
        var setter = AccessTools.PropertySetter(type, property);
        if (setter != null)
            setter.Invoke(obj, new[] {value});
        return obj;
    }

    #endregion

    #region Get-set with implicit type

    /// <summary>
    /// Gets the value of the instance field with the specified name.
    /// </summary>
    /// <param name="obj">The object to get the value from.</param>
    /// <param name="field">The name of the instance field.</param>
    /// <returns>The obtained object.</returns>
    public static object Get(this object obj, string field) => Get(obj.GetType(), obj, field);

    /// <summary>
    /// Gets the value of the instance field with the specified name as type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="obj">The object to get the value from.</param>
    /// <param name="field">The name of the instance field.</param>
    /// <typeparam name="T">The type of the field's value.</typeparam>
    /// <returns>The obtained <typeparamref name="T"/> type object.</returns>
    /// <exception cref="InvalidCastException">Thrown if the field's value is not of type <typeparamref name="T"/>.</exception>
    public static T Get<T>(this object obj, string field) => (T) Get(obj, field);

    /// <summary>
    /// Sets the value of the instance field with the specified name.
    /// </summary>
    /// <param name="obj">The object to set the value on.</param>
    /// <param name="field">The name of the instance field.</param>
    /// <param name="value">The new value to set.</param>
    /// <returns>The object itself.</returns>
    public static T Set<T>(this T obj, string field, object value)
    {
        Set(obj.GetType(), obj, field, value);
        return obj;
    }

    /// <summary>
    /// Gets the value of the instance property with the specified name.
    /// </summary>
    /// <param name="obj">The object to get the value from.</param>
    /// <param name="property">The name of the instance property.</param>
    /// <returns>The obtained object.</returns>
    public static object GetProp(this object obj, string property) => GetProp(obj.GetType(), obj, property);

    /// <summary>
    /// Gets the value of the instance property with the specified name as type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="obj">The object to get the value from.</param>
    /// <param name="property">The name of the instance property.</param>
    /// <typeparam name="T">The type of the property's value.</typeparam>
    /// <returns>The obtained <typeparamref name="T"/> type object.</returns>
    /// <exception cref="InvalidCastException">Thrown if the property's value is not of type <typeparamref name="T"/>.</exception>
    public static T GetProp<T>(this object obj, string property) => (T) GetProp(obj, property);

    /// <summary>
    /// Sets the value of the instance property with the specified name.
    /// </summary>
    /// <param name="obj">The object to set the value on.</param>
    /// <param name="property">The name of the instance property.</param>
    /// <param name="value">The new value to set.</param>
    /// <typeparam name="T">The type of the instance object.</typeparam>
    /// <returns>The <typeparamref name="T"/> object itself.</returns>
    public static T SetProp<T>(this T obj, string property, object value)
    {
        SetProp(obj.GetType(), obj, property, value);
        return obj;
    }

    #endregion

    #region Instance calls

    /// <summary>
    /// Calls an instance method with the specified name.
    /// </summary>
    /// <param name="type">The type of the object containing the method.</param>
    /// <param name="obj">The object to call the method on.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <returns>The value the method returned.</returns>
    public static object Call(this Type type, object obj, string methodName, params object[] args)
    {
        var method = AccessTools.Method(type, methodName, args.Select(e => e.GetType()).ToArray());
        return method == null ? null : method.Invoke(obj, args);
    }

    /// <summary>
    /// Calls an instance method with the specified name of return type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="type">The type of the object containing the method.</param>
    /// <param name="obj">The object to call the method on.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <typeparam name="T">The type of the method's return value.</typeparam>
    /// <returns>The value the method returned.</returns>
    /// <exception cref="InvalidCastException">Thrown if the method's return value is not of type <typeparamref name="T"/>.</exception>
    public static T Call<T>(this Type type, object obj, string methodName, params object[] args) => (T) Call(type, obj, methodName, args);

    /// <summary>
    /// Calls an instance method with the specified name.
    /// </summary>
    /// <param name="obj">The object to call the method on.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <returns>The value the method returned.</returns>
    public static object Call(this object obj, string methodName, params object[] args) => Call(obj.GetType(), obj, methodName, args);

    /// <summary>
    /// Calls an instance method with the specified name of return type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="obj">The object to call the method on.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <typeparam name="T">The type of the method's return value.</typeparam>
    /// <returns>The value the method returned.</returns>
    /// <exception cref="InvalidCastException">Thrown if the method's return value is not of type <typeparamref name="T"/>.</exception>
    public static T Call<T>(this object obj, string methodName, params object[] args) => (T) Call(obj, methodName, args);

    #endregion

    #region Static get-set

    /// <summary>
    /// Gets the value of the static field with the specified name.
    /// </summary>
    /// <param name="type">The type containing the field.</param>
    /// <param name="field">The name of the field.</param>
    /// <returns>The value of the field.</returns>
    public static object StaticGet(this Type type, string field)
    {
        var fieldInfo = AccessTools.Field(type, field);
        return fieldInfo == null ? null : fieldInfo.GetValue(null);
    }

    /// <summary>
    /// Gets the value of the static field with the specified name as type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="type">The type containing the field.</param>
    /// <param name="field">The name of the field.</param>
    /// <typeparam name="T">The type of the field's value.</typeparam>
    /// <returns>The value of the field.</returns>
    /// <exception cref="InvalidCastException">Thrown if the field's value is not of type <typeparamref name="T"/>.</exception>
    public static T StaticGet<T>(this Type type, string field) => (T) StaticGet(type, field);

    /// <summary>
    /// Sets the value of the static field with the specified name.
    /// </summary>
    /// <param name="type">The type containing the field.</param>
    /// <param name="field">The name of the field.</param>
    /// <param name="value">The new value to set.</param>
    public static void StaticSet(this Type type, string field, object value)
    {
        var fieldInfo = AccessTools.Field(type, field);
        if (fieldInfo != null)
            fieldInfo.SetValue(null, value);
    }

    #endregion

    #region Static property get-set

    /// <summary>
    /// Gets the value of the static property with the specified name.
    /// </summary>
    /// <param name="type">The type containing the property.</param>
    /// <param name="property">The name of the property.</param>
    /// <returns>The value of the property.</returns>
    public static object StaticGetProp(this Type type, string property)
    {
        var getter = AccessTools.PropertyGetter(type, property);
        return getter == null ? null : getter.Invoke(null, Array.Empty<object>());
    }

    /// <summary>
    /// Gets the value of the static property with the specified name as type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="type">The type containing the property.</param>
    /// <param name="property">The name of the property.</param>
    /// <typeparam name="T">The type of the property's value.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidCastException">Thrown if the property's value is not of type <typeparamref name="T"/>.</exception>
    public static T StaticGetProp<T>(this Type type, string property) => (T) StaticGetProp(type, property);

    /// <summary>
    /// Sets the value of the static property with the specified name.
    /// </summary>
    /// <param name="type">The type containing the property.</param>
    /// <param name="property">The name of the property.</param>
    /// <param name="value">The new value to set.</param>
    public static void StaticSetProp(this Type type, string property, object value)
    {
        var setter = AccessTools.PropertySetter(type, property);
        if (setter != null)
            setter.Invoke(null, new[] {value});
    }

    #endregion

    #region Static calls

    /// <summary>
    /// Calls a static method with the specified name.
    /// </summary>
    /// <param name="type">The type containing the method.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <returns>The value the method returned.</returns>
    public static object StaticCall(this Type type, string methodName, params object[] args)
    {
        var method = AccessTools.Method(type, methodName, args.Select(e => e.GetType()).ToArray());
        return method != null ? method.Invoke(null, args) : default;
    }

    /// <summary>
    /// Calls a static method with the specified name of return type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="type">The type containing the method.</param>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments to pass to the method.</param>
    /// <typeparam name="T">The type of the method's return value.</typeparam>
    /// <returns>The value the method returned.</returns>
    /// <exception cref="InvalidCastException">Thrown if the method's return value is not of type <typeparamref name="T"/>.</exception>
    public static T StaticCall<T>(this Type type, string methodName, params object[] args) => (T) StaticCall(type, methodName, args);

    #endregion

    #region Enumerable methods

    /// <summary>
    /// Maps an enumerable of an unknown type (e.g. nested private class) to an inner field of that type.
    /// </summary>
    /// <param name="obj">The object containing the enumerable of the unknown type.</param>
    /// <param name="fieldName">The name of the field in the object.</param>
    /// <param name="innerField">The name of the field in the unknown type.</param>
    /// <param name="mapper">A function to convert the objects to type <typeparamref name="T"/>.</param>
    /// <typeparam name="T">The type of the object to convert to.</typeparam>
    /// <returns>An enumerable of type <typeparamref name="T"/>.</returns>
    public static IEnumerable<T> MapUnknownTypeArray<T>(this object obj, string fieldName, string innerField, Func<object, T> mapper) => MapUnknownTypeArray(obj, fieldName, o => mapper(Get(o, innerField)));

    /// <summary>
    /// Maps an enumerable of an unknown type (e.g. nested private class) to a known type.
    /// </summary>
    /// <param name="obj">The object containing the enumerable of the unknown type.</param>
    /// <param name="fieldName">The name of the field in the object.</param>
    /// <param name="mapper">A function to convert the objects to type <typeparamref name="T"/>.</param>
    /// <typeparam name="T">The type of the object to convert to.</typeparam>
    /// <returns>An enumerable of type <typeparamref name="T"/>.</returns>
    public static IEnumerable<T> MapUnknownTypeArray<T>(this object obj, string fieldName, Func<object, T> mapper) => (from object o in Get<IEnumerable>(obj, fieldName) select mapper(o)).ToList();

    /// <summary>
    /// Checks if every value in the enumerable is the same.
    /// </summary>
    /// <param name="enumerable">The enumerable to check.</param>
    /// <param name="value">The value to check for.</param>
    /// <param name="comparer">A custom comparer for the type of the value.</param>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <returns>If every item of the enumerable is the same.</returns>
    public static bool AllTheSame<T>(this IEnumerable<T> enumerable, out T value, IEqualityComparer<T> comparer = null)
    {
        value = default;
        var count = 0;
        foreach (var x in enumerable)
        {
            if (count != 0 && !(comparer ?? EqualityComparer<T>.Default).Equals(x, value))
                return false;
            count++;
            value = x;
        }

        return count is not 0;
    }

    /// <summary>
    /// Adds the elements of the specified collection to the <see cref="HashSet{T}" />.
    /// </summary>
    /// <param name="set">The set to add the items to.</param>
    /// <param name="collection">The collection whose elements should be added to the <see cref="HashSet{T}" />.</param>
    public static int AddRange<T>(this HashSet<T> set, IEnumerable<T> collection)
    {
        if (set == null || collection == null)
            return 0;
        var count = 0;
        foreach (var item in collection)
            if (set.Add(item))
                count++;
        return count;
    }

    /// <summary>
    /// Returns an empty enumerable if the supplied argument is null.
    /// </summary>
    /// <param name="enumerable">The enumerable to check.</param>
    /// <typeparam name="T">The type of the enumerable.</typeparam>
    /// <returns>An empty enumerable if the supplied argument is null; otherwise the supplied argument.</returns>
    public static IEnumerable<T> AsNonNullEnumerable<T>(this IEnumerable<T> enumerable) => enumerable ?? Enumerable.Empty<T>();

    #endregion

    #region Enums

    /// <summary>
    /// Gets the defined enums in the specified type.
    /// </summary>
    /// <param name="type">The type to get the enums from.</param>
    /// <typeparam name="T">The type of the enums.</typeparam>
    /// <returns>An enumerable of the enums.</returns>
    public static IEnumerable<T> Enums<T>(this Type type) => (T[]) Enum.GetValues(type);

    /// <summary>
    /// Gets the defined enums in the specified type.
    /// </summary>
    /// <param name="enumType">The type to get the enums from.</param>
    /// <typeparam name="T">The type of the enums.</typeparam>
    /// <returns>An enumerable of the enums.</returns>
    /// <remarks>Can be used like <see cref="Enums{T}(System.Type)"/>, but instead of passing in the type, it needs an enum value.</remarks>
    public static IEnumerable<T> Enums<T>(this T enumType) where T : Enum => (T[]) Enum.GetValues(enumType.GetType());

    /// <summary>
    /// Gets the defined enums in the specified type except the given enum.
    /// </summary>
    /// <param name="except">The enum to exclude.</param>
    /// <typeparam name="T">The type of the enums.</typeparam>
    /// <returns>An enumerable of the enums.</returns>
    public static IEnumerable<T> EnumsExcept<T>(this T except) where T : Enum => EnumsExcept(except, except);

    /// <summary>
    /// Gets the defined enums in the specified type except the given enum.
    /// </summary>
    /// <param name="enumType">An enum instance that specifies the type of the enum.</param>
    /// <param name="except">The enum to exclude.</param>
    /// <typeparam name="T">The type of the enums.</typeparam>
    /// <returns>An enumerable of the enums.</returns>
    /// <remarks>Can be used like <see cref="Enums{T}(System.Type)"/>, but instead of passing in the type, it needs some enum value.</remarks>
    public static IEnumerable<T> EnumsExcept<T>(this T enumType, T except) where T : Enum => Enums<T>(enumType.GetType()).Where(e => !e.Equals(except));

    #endregion

    #region Embedded resources

    /// <summary>
    /// Gets the common part of all resource paths in the assembly of the given type.
    /// </summary>
    /// <param name="type">A type in the assembly.</param>
    /// <returns>The base path to resources.</returns>
    public static string GetBaseResourcePath(this Type type) => GetBaseResourcePath(type.Assembly);

    /// <summary>
    /// Gets the common part of all resource paths in the given assembly.
    /// </summary>
    /// <param name="assembly">The assembly containing resources.</param>
    /// <returns>The base path to resources.</returns>
    public static string GetBaseResourcePath(this Assembly assembly)
    {
        var names = assembly.GetManifestResourceNames();
        if (names.Length is 0)
            return null;
        var common = "";
        var curDotIndex = new[] {0};
        while (names.Select(e => e.IndexOf('.', curDotIndex[0] + 1)).AllTheSame(out var index))
        {
            if (index < 0 || !names.Select(e => e.Substring(0, index)).AllTheSame(out var sub))
                break;
            curDotIndex[0] = index;
            common = sub;
        }

        return common;
    }

    /// <summary>
    /// Gets the common part of all resource paths in the calling assembly.
    /// </summary>
    /// <returns>The base path to resources.</returns>
    /// <seealso cref="GetBaseResourcePath(System.Reflection.Assembly)"/>
    public static string GetBaseResourcePath() => GetBaseResourcePath(Assembly.GetCallingAssembly());

    /// <summary>
    /// Gets the resource stream embedded within the assembly from the given path, without needing the base path.
    /// </summary>
    /// <param name="type">A type in the assembly.</param>
    /// <param name="path">The path of the resource relative to the <see cref="GetBaseResourcePath(System.Type)">base path</see>.</param>
    /// <returns>A stream to access the resource.</returns>
    /// <seealso cref="GetBaseResourcePath(System.Type)"/>
    public static Stream GetEmbeddedResourceByName(this Type type, string path) => GetEmbeddedResourceByName(type.Assembly, path);

    /// <summary>
    /// Gets the resource stream embedded within the assembly from the given path, without needing the base path.
    /// </summary>
    /// <param name="assembly">The assembly containing the resource.</param>
    /// <param name="path">The path of the resource relative to the <see cref="GetBaseResourcePath(System.Type)">base path</see>.</param>
    /// <returns>A stream to access the resource.</returns>
    /// <seealso cref="GetBaseResourcePath(System.Reflection.Assembly)"/>
    public static Stream GetEmbeddedResourceByName(this Assembly assembly, string path)
    {
        var basePath = GetBaseResourcePath(assembly);
        return GetEmbeddedResource(assembly, $"{(string.IsNullOrEmpty(basePath) ? "" : $"{basePath}.")}{path}");
    }

    /// <summary>
    /// Gets the resource stream embedded within the calling assembly from the given path, without needing the base path.
    /// </summary>
    /// <param name="path">The path of the resource relative to the <see cref="GetBaseResourcePath(System.Type)">base path</see>.</param>
    /// <returns>A stream to access the resource.</returns>
    /// <seealso cref="GetBaseResourcePath(System.Reflection.Assembly)"/>
    /// <seealso cref="GetEmbeddedResourceByName(System.Reflection.Assembly, string)"/>
    public static Stream GetEmbeddedResourceByName(string path) => GetEmbeddedResourceByName(Assembly.GetCallingAssembly(), path);

    /// <summary>
    /// Gets the resource stream embedded within the assembly from the given path.
    /// </summary>
    /// <param name="type">A type in the assembly.</param>
    /// <param name="fullPath">The full path to the resource including the base namespace.</param>
    /// <returns>A stream to access the resource.</returns>
    /// <seealso cref="GetBaseResourcePath(System.Type)"/>
    public static Stream GetEmbeddedResource(this Type type, string fullPath) => GetEmbeddedResource(type.Assembly, fullPath);

    /// <summary>
    /// Gets the resource stream embedded within the assembly from the given path.
    /// </summary>
    /// <param name="assembly">The assembly containing the resource.</param>
    /// <param name="fullPath">The full path to the resource including the base namespace.</param>
    /// <returns>A stream to access the resource.</returns>
    /// <seealso cref="GetBaseResourcePath(System.Reflection.Assembly)"/>
    public static Stream GetEmbeddedResource(this Assembly assembly, string fullPath) => assembly.GetManifestResourceStream(fullPath);

    /// <summary>
    /// Gets the resource stream embedded within the calling assembly from the given path.
    /// </summary>
    /// <param name="fullPath">The full path to the resource including the base namespace.</param>
    /// <returns>A stream to access the resource.</returns>
    /// <seealso cref="GetBaseResourcePath(System.Reflection.Assembly)"/>
    /// <seealso cref="GetEmbeddedResource(System.Reflection.Assembly, string)"/>
    public static Stream GetEmbeddedResource(string fullPath) => GetEmbeddedResource(Assembly.GetCallingAssembly(), fullPath);

    /// <summary>
    /// Gets the first resource stream embedded within the assembly which contains the given file name.
    /// </summary>
    /// <param name="type">A type in the assembly.</param>
    /// <param name="fileName">A file name to search for.</param>
    /// <returns>A stream to access the resource containing the given <paramref name="fileName"/>.</returns>
    public static Stream FindEmbeddedResource(this Type type, string fileName) => GetEmbeddedResource(type.Assembly, fileName);

    /// <summary>
    /// Gets the first resource stream embedded within the assembly which contains the given file name.
    /// </summary>
    /// <param name="assembly">The assembly containing the resource.</param>
    /// <param name="fileName">A file name to search for.</param>
    /// <returns>A stream to access the resource containing the given <paramref name="fileName"/>.</returns>
    public static Stream FindEmbeddedResource(this Assembly assembly, string fileName)
    {
        var lower = fileName.ToLowerInvariant();
        return assembly.GetManifestResourceNames()
            .Where(name => name.ToLowerInvariant().Contains(lower))
            .Select(assembly.GetManifestResourceStream).FirstOrDefault();
    }

    /// <summary>
    /// Gets the first resource stream embedded within the calling assembly which contains the given file name.
    /// </summary>
    /// <param name="fileName">A file name to search for.</param>
    /// <returns>A stream to access the resource containing the given <paramref name="fileName"/>.</returns>
    public static Stream FindEmbeddedResource(string fileName) => FindEmbeddedResource(Assembly.GetCallingAssembly(), fileName);

    /// <summary>
    /// Wraps the given <paramref name="stream"/> into a <see cref="BinaryReader"/>.
    /// </summary>
    /// <param name="stream">The stream to convert.</param>
    /// <returns>A binary reader containing the stream.</returns>
    public static BinaryReader Binary(this Stream stream) => stream != null ? new BinaryReader(stream) : null;

    #endregion

}
