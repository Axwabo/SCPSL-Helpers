using System;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>
    /// Finds the value of a non-static field in the object whose reference is currently on the evaluation stack.<br/>
    /// Pushes the value of a static field onto the evaluation stack.
    /// </summary>
    /// <param name="field">The information about the field.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the field.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="field"/> is null.</exception>
    public static CodeInstruction Ldfld(FieldInfo field)
    {
        if (field == null)
            throw new ArgumentNullException(nameof(field));
        return field.IsStatic ? new CodeInstruction(OpCodes.Ldsfld, field) : new CodeInstruction(OpCodes.Ldfld, field);
    }

    /// <summary>
    /// Finds the value of a non-static field in the object whose reference is currently on the evaluation stack.<br/>
    /// Pushes the value of a static field onto the evaluation stack.
    /// </summary>
    /// <param name="type">The type of the object containing the field.</param>
    /// <param name="fieldName">The name of the field to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the field.</returns>
    /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
    public static CodeInstruction Ldfld(Type type, string fieldName) => Ldfld(AccessTools.Field(type, fieldName) ?? throw new ArgumentException($"No field found for type={type.FullName}, name={fieldName}"));

    /// <summary>
    /// Finds the value of a non-static field in the object whose reference is currently on the evaluation stack.
    /// </summary>
    /// <param name="fieldName">The name of the field to load.</param>
    /// <typeparam name="T">The type of the object containing the field.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the field.</returns>
    /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
    /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
    public static CodeInstruction Ldfld<T>(string fieldName) => Ldfld(typeof(T), fieldName);

    /// <summary>
    /// Replaces the value stored in the non-static field of an object reference or pointer with a new value.<br/>
    /// Replaces the value of a static field with a value from the evaluation stack.
    /// </summary>
    /// <param name="field">The information about the field.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the field.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="field"/> is null.</exception>
    public static CodeInstruction Stfld(FieldInfo field)
    {
        if (field == null)
            throw new ArgumentNullException(nameof(field));
        return field.IsStatic ? new CodeInstruction(OpCodes.Stsfld, field) : new CodeInstruction(OpCodes.Stfld, field);
    }

    /// <summary>
    /// Replaces the value stored in the non-static field of an object reference or pointer with a new value.<br/>
    /// Replaces the value of a static field with a value from the evaluation stack.
    /// </summary>
    /// <param name="type">The type of the object containing the field.</param>
    /// <param name="fieldName">The name of the field to store.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the field.</returns>
    /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
    public static CodeInstruction Stfld(Type type, string fieldName) => Stfld(AccessTools.Field(type, fieldName) ?? throw new ArgumentException($"No field found for type={type.FullName}, name={fieldName}"));

    /// <summary>
    /// Replaces the value stored in the non-static field of an object reference or pointer with a new value.<br/>
    /// </summary>
    /// <param name="fieldName">The name of the field to store.</param>
    /// <typeparam name="T">The type of the object containing the field.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the field.</returns>
    /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
    /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
    public static CodeInstruction Stfld<T>(string fieldName) => Stfld(typeof(T), fieldName);

    /// <summary>
    /// Finds the address of a field in the object whose reference is currently on the evaluation stack.
    /// </summary>
    /// <param name="field">The information about the field.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of field.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="field"/> is null.</exception>
    public static CodeInstruction Ldflda(FieldInfo field)
    {
        if (field == null)
            throw new ArgumentNullException(nameof(field));
        return field.IsStatic ? new CodeInstruction(OpCodes.Ldsflda, field) : new CodeInstruction(OpCodes.Ldflda, field);
    }

    /// <summary>
    /// Finds the address of a field in the object whose reference is currently on the evaluation stack.
    /// </summary>
    /// <param name="type">The type of the object containing the field.</param>
    /// <param name="fieldName">The name of the field to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of the field.</returns>
    /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
    public static CodeInstruction Ldflda(Type type, string fieldName) => Ldflda(AccessTools.Field(type, fieldName) ?? throw new ArgumentException($"No field found for type={type.FullName}, name={fieldName}"));

    /// <summary>
    /// Finds the address of a field in the object whose reference is currently on the evaluation stack.
    /// </summary>
    /// <param name="fieldName">The name of the field to load.</param>
    /// <typeparam name="T">The type of the object containing the field.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of field.</returns>
    /// <exception cref="ArgumentException">Thrown if the field was not found.</exception>
    /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
    public static CodeInstruction Ldflda<T>(string fieldName) => Ldflda(typeof(T), fieldName);

}
