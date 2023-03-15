using System;
using HarmonyLib;

namespace Axwabo.Helpers.Harmony.Attributes;

/// <summary>
/// An attribute to mark patches that need to modify nested classes that are inaccessible from the assembly.
/// </summary>
/// <seealso cref="HarmonyPatch"/>
public class NestedClassPatch : HarmonyPatch
{

    /// <summary>
    /// Creates a simple patch.
    /// </summary>
    /// <param name="classPath">The path to the nested class, starting with the full name of the parent type.</param>
    /// <param name="methodName">The method to patch.</param>
    /// <seealso cref="GetNestedType(string[])" />
    public NestedClassPatch(string methodName, params string[] classPath)
    {
        info.declaringType = GetNestedType(classPath);
        info.methodName = methodName;
    }

    /// <summary>
    /// Creates a simple patch with the given argument types.
    /// </summary>
    /// <param name="classPath">The path to the nested class, starting with the full name of the parent type.</param>
    /// <param name="methodName">The method to patch.</param>
    /// <param name="argumentTypes">The argument types of the method.</param>
    /// <seealso cref="GetNestedType(string[])" />
    public NestedClassPatch(string methodName, Type[] argumentTypes, string[] classPath)
        : this(methodName, classPath) => info.argumentTypes = argumentTypes;

    /// <summary>
    /// Gets a nested type from the given class.
    /// </summary>
    /// <param name="parentClass">The class containing the nested type.</param>
    /// <param name="nestedClass">The name of the nested class.</param>
    /// <returns>The type of the nested class.</returns>
    public static Type GetNestedType(string parentClass, string nestedClass) => AccessTools.TypeByName(parentClass).GetNestedType(nestedClass);

    /// <summary>
    /// Gets a nested type based on a path.
    /// </summary>
    /// <param name="classPath">The path to the nested class, starting with the full name of the parent type.</param>
    /// <returns>The type of the nested class.</returns>
    /// <example>
    /// <code>
    /// GetNestedType(new[] {"MyAssembly.MyType", "MyNestedClass"});
    /// </code>
    /// </example>
    public static Type GetNestedType(string[] classPath)
    {
        var type = AccessTools.TypeByName(classPath[0]);
        for (var i = 1; i < classPath.Length; i++)
            type = type.GetNestedType(classPath[i]);
        return type;
    }

}
