using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>
    /// Finds the index of the instruction in the list which contains the specified label.
    /// </summary>
    /// <param name="instructions">The list of instructions to execute the search in.</param>
    /// <param name="label">The label to find.</param>
    /// <returns>The index of the instruction.</returns>
    /// <seealso cref="CodeInstruction"/>
    public static int IndexOfInstructionWithLabel(this List<CodeInstruction> instructions, Label label) => instructions.FindIndex(i => i.labels.Contains(label));

    /// <summary>
    /// Finds the index of the instruction which calls the specific method.
    /// </summary>
    /// <param name="list">The list of instructions to execute the search in.</param>
    /// <param name="method">The information about the method.</param>
    /// <param name="start">The starting index of the search.</param>
    /// <returns>The index of the instruction.</returns>
    public static int FindCall(this List<CodeInstruction> list, MethodInfo method, int start = 0) => FindCode(list, method.IsVirtual ? OpCodes.Callvirt : OpCodes.Call, i => i.operand as MethodInfo == method, start);

    /// <summary>
    /// Finds the index of the instruction which calls the specific method.
    /// </summary>
    /// <param name="list">The list of instructions to execute the search in.</param>
    /// <param name="type">The type of the object containing the method.</param>
    /// <param name="methodName">The name of the method to call.</param>
    /// <param name="parameters">The type parameters of the method.</param>
    /// <param name="generics">The generics used to call the method.</param>
    /// <param name="start">The starting index of the search.</param>
    /// <returns>The index of the instruction.</returns>
    /// <exception cref="ArgumentException">Thrown if the method was not found.</exception>
    public static int FindCall(this List<CodeInstruction> list, Type type, string methodName, Type[] parameters = null, Type[] generics = null, int start = 0) => FindCall(list, AccessTools.Method(type, methodName, parameters, generics) ?? throw new ArgumentException($"No method found for type={type.FullName}, name={methodName}, parameters={parameters.Description()}, generics={generics.Description()}"), start);

    /// <summary>
    /// Finds the index of the instruction which calls the specific method.
    /// </summary>
    /// <param name="list">The list of instructions to execute the search in.</param>
    /// <param name="methodName">The name of the method to call.</param>
    /// <param name="parameters">The type parameters of the method.</param>
    /// <param name="generics">The generics used to call the method.</param>
    /// <param name="start">The starting index of the search.</param>
    /// <typeparam name="T">The type of the object containing the method.</typeparam>
    /// <returns>The index of the instruction.</returns>
    /// <exception cref="ArgumentException">Thrown if the method was not found.</exception>
    public static int FindCall<T>(this List<CodeInstruction> list, string methodName, Type[] parameters = null, Type[] generics = null, int start = 0) => FindCall(list, typeof(T), methodName, parameters, generics, start);

    /// <summary>
    /// Finds the index of the instruction which loads the specific field.
    /// </summary>
    /// <param name="list">The list of instructions to execute the search in.</param>
    /// <param name="field">The information about the field.</param>
    /// <param name="start">The starting index of the search.</param>
    /// <returns>The index of the instruction.</returns>
    public static int FindField(this List<CodeInstruction> list, FieldInfo field, int start = 0) => FindCode(list, field.IsStatic ? OpCodes.Ldsfld : OpCodes.Ldfld, i => i.operand as FieldInfo == field, start);

    /// <summary>
    /// Finds the index of the instruction which loads the specific field.
    /// </summary>
    /// <param name="list">The list of instructions to execute the search in.</param>
    /// <param name="type">The type of the object containing the field.</param>
    /// <param name="fieldName">The name of the field to load.</param>
    /// <param name="start">The starting index of the search.</param>
    /// <returns>The index of the instruction.</returns>
    /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
    public static int FindField(this List<CodeInstruction> list, Type type, string fieldName, int start = 0) => FindField(list, AccessTools.Field(type, fieldName) ?? throw new ArgumentException($"No field found for type={type.FullName}, name={fieldName}"), start);

    /// <summary>
    /// Finds the index of the instruction which loads the specific field.
    /// </summary>
    /// <param name="list">The list of instructions to execute the search in.</param>
    /// <param name="fieldName">The name of the field to load.</param>
    /// <param name="start">The starting index of the search.</param>
    /// <typeparam name="T">The type of the object containing the field.</typeparam>
    /// <returns>The index of the instruction.</returns>
    /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
    /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
    public static int FindField<T>(this List<CodeInstruction> list, string fieldName, int start = 0) => FindField(list, typeof(T), fieldName, start);

    /// <summary>
    /// Finds the index of the instruction with a specific code and an optional check.
    /// </summary>
    /// <param name="list">The list of instructions.</param>
    /// <param name="code">The <see cref="OpCode"/> to find.</param>
    /// <param name="predicate">An additional check.</param>
    /// <param name="start">The starting index of the search.</param>
    /// <returns>The index of the instruction.</returns>
    public static int FindCode(this List<CodeInstruction> list, OpCode code, Predicate<CodeInstruction> predicate = null, int start = 0) => list.FindIndex(start, i => i.opcode == code && (predicate?.Invoke(i) ?? true));

}
