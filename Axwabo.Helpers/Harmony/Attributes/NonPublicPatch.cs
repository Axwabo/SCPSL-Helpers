using System;
using Exiled.API.Features;
using HarmonyLib;

namespace Axwabo.Helpers.Harmony.Attributes {

    /// <summary>
    /// An attribute to mark patches that need to modify types inaccessible from the assembly.
    /// </summary>
    /// <seealso cref="HarmonyPatch"/>
    public class NonPublicPatch : HarmonyPatch {

        /// <summary>
        /// Creates a simple patch.
        /// </summary>
        /// <param name="typeColonMethodName">The path to the method.</param>
        /// <seealso cref="AccessTools.Method(string,System.Type[],System.Type[])">AccessTools.Method</seealso>
        public NonPublicPatch(string typeColonMethodName) {
            var method = AccessTools.Method(typeColonMethodName);
            info.declaringType = method.DeclaringType;
            info.methodName = method.Name;
        }

        /// <summary>
        /// Creates a simple patch with the given argument types.
        /// </summary>
        /// <param name="typeColonMethodName">The path to the method.</param>
        /// <param name="argumentTypes">The argument types of the method.</param>
        /// <seealso cref="AccessTools.Method(string,System.Type[],System.Type[])">AccessTools.Method</seealso>
        public NonPublicPatch(string typeColonMethodName, params Type[] argumentTypes) : this(typeColonMethodName) {
            info.argumentTypes = argumentTypes;
        }

    }

}
