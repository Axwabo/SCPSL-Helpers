using System;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>
    /// Calls the getter method for the given <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The information about the property.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that gets the value of the property.</returns>
    /// <seealso cref="Call(System.Reflection.MethodInfo)"/>
    /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="property"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the property is write-only.</exception>
    public static CodeInstruction Get(PropertyInfo property)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));
        return property.CanRead ? Call(property.GetMethod) : throw new InvalidOperationException($"Property {property.Name} is write-only.");
    }

    /// <summary>
    /// Calls the getter method for the given property.
    /// </summary>
    /// <param name="type">The type of the object containing the property.</param>
    /// <param name="propertyName">The name of the property to get.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that gets the value of the property.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the property was not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the property is write-only.</exception>
    public static CodeInstruction Get(Type type, string propertyName) => Get(AccessTools.Property(type, propertyName) ?? throw new ArgumentException($"No property found for type={type.FullName}, name={propertyName}"));

    /// <summary>
    /// Calls the getter method for the given property.
    /// </summary>
    /// <param name="propertyName">The name of the property to get.</param>
    /// <typeparam name="T">The type of the object containing the property.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that gets the value of the property.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the property was not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the property is write-only.</exception>
    /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
    public static CodeInstruction Get<T>(string propertyName) => Get(typeof(T), propertyName);

    /// <summary>
    /// Calls the setter method for the given <paramref name="property"/>.
    /// </summary>
    /// <param name="property">The information about the property.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that sets the value of the property.</returns>
    /// <seealso cref="Call(System.Reflection.MethodInfo)"/>
    /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="property"/> is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the property is read-only.</exception>
    public static CodeInstruction Set(PropertyInfo property)
    {
        if (property == null)
            throw new ArgumentNullException(nameof(property));
        return property.CanWrite ? Call(property.SetMethod) : new CodeInstruction(OpCodes.Stfld, property);
    }

    /// <summary>
    /// Calls the setter method for the given property.
    /// </summary>
    /// <param name="type">The type of the object containing the property.</param>
    /// <param name="propertyName">The name of the property to set.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that sets the value of the property.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the property was not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the property is read-only.</exception>
    public static CodeInstruction Set(Type type, string propertyName) => Set(AccessTools.Property(type, propertyName) ?? throw new ArgumentException($"No property found for type={type.FullName}, name={propertyName}"));

    /// <summary>
    /// Calls the setter method for the given property.
    /// </summary>
    /// <param name="propertyName">The name of the property to set.</param>
    /// <typeparam name="T">The type of the object containing the property.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that sets the value of the property.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the property was not found.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the property is read-only.</exception>
    /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
    public static CodeInstruction Set<T>(string propertyName) => Set(typeof(T), propertyName);

}
