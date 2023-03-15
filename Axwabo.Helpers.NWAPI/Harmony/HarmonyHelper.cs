using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace Axwabo.Helpers.Harmony;

/// Helper class for executing Harmony patches.
public static class HarmonyHelper
{

    /// <summary>
    /// Patches a method inaccessible from the assembly.
    /// </summary>
    /// <param name="harmonyInstance">The instance to process the patch with.</param>
    /// <param name="typeColonMethodName">The path to the method.</param>
    /// <param name="prefix">The </param>
    /// <param name="postfix"></param>
    /// <param name="transpiler"></param>
    /// <param name="finalizer"></param>
    /// <returns></returns>
    /// <seealso cref="AccessTools.Method(System.Type,string,System.Type[],System.Type[])"/>
    public static MethodInfo Patch(this HarmonyLib.Harmony harmonyInstance, string typeColonMethodName, MethodInfo prefix = null, MethodInfo postfix = null, MethodInfo transpiler = null, MethodInfo finalizer = null) => harmonyInstance.Patch(AccessTools.Method(typeColonMethodName), prefix.ToHarmonyMethod(), postfix.ToHarmonyMethod(), transpiler.ToHarmonyMethod(), finalizer.ToHarmonyMethod());

    /// <summary>
    /// Wraps a <see cref="MethodInfo"/> object into a <see cref="HarmonyMethod"/> object.
    /// </summary>
    /// <param name="method">The method to convert.</param>
    /// <returns>The corresponding <see cref="HarmonyMethod"/>.</returns>
    public static HarmonyMethod ToHarmonyMethod(this MethodInfo method) => method == null ? null : new HarmonyMethod(method);

    /// <summary>
    /// Gets the full signatures of patched methods by the given <paramref name="instance"/>.
    /// </summary>
    /// <param name="instance">The <see cref="HarmonyLib.Harmony">Harmony instance</see> to get the methods from.</param>
    /// <returns>An enumerable of the full signatures.</returns>
    public static IEnumerable<string> PatchedMethods(this HarmonyLib.Harmony instance) =>
        instance.GetPatchedMethods()
            .Select(e =>
                $"{e.DeclaringType?.FullName ?? "???"}.{e.Name}"
                + $"({string.Join(", ", e.GetParameters().Select(p => p.ParameterType.FullName))})");

}
