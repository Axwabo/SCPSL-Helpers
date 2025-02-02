namespace Axwabo.Helpers.Harmony.Attributes;

/// <summary>
/// An attribute to mark patches to be applied on constructors.
/// </summary>
/// <seealso cref="HarmonyPatch"/>
public class ConstructorPatch : HarmonyPatch
{

    /// <summary>
    /// Creates a simple patch.
    /// </summary>
    /// <param name="type">The type's constructor to patch.</param>
    /// <seealso cref="AccessTools.Method(string,System.Type[],System.Type[])">AccessTools.Method</seealso>
    public ConstructorPatch(Type type)
    {
        info.declaringType = type;
        info.methodType = MethodType.Constructor;
    }

    /// <summary>
    /// Creates a simple patch with the given argument types.
    /// </summary>
    /// <param name="type">The type's constructor to patch.</param>
    /// <param name="argumentTypes">The argument types of the constructor.</param>
    /// <seealso cref="AccessTools.Constructor">AccessTools.Constructor</seealso>
    public ConstructorPatch(Type type, params Type[] argumentTypes)
    {
        info.declaringType = type;
        info.methodType = MethodType.Constructor;
        info.argumentTypes = argumentTypes;
    }

    /// <summary>
    /// Creates a simple patch.
    /// </summary>
    /// <param name="typeName">The full name of the type.</param>
    public ConstructorPatch(string typeName) : this(AccessTools.TypeByName(typeName))
    {
    }

    /// <summary>
    /// Creates a simple patch with the given argument types.
    /// </summary>
    /// <param name="typeName">The full name of the type.</param>
    /// <param name="argumentTypes">The argument types of the constructor.</param>
    public ConstructorPatch(string typeName, params Type[] argumentTypes) : this(AccessTools.TypeByName(typeName), argumentTypes)
    {
    }

}
