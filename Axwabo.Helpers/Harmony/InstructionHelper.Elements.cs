namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>Loads an object reference as a type O (object reference) onto the evaluation stack indirectly.</summary>
    /// <seealso cref="OpCodes.Ldind_Ref"/>
    public static CodeInstruction LdindRef => new(OpCodes.Ldind_Ref);

    /// <summary>Stores an object reference value at a supplied address.</summary>
    /// <seealso cref="OpCodes.Stind_Ref"/>
    public static CodeInstruction StindRef => new(OpCodes.Stind_Ref);

    /// <summary>Loads the element containing an object reference at a specified array index onto the top of the evaluation stack as type O (object reference).</summary>
    /// <seealso cref="OpCodes.Ldind_Ref"/>
    public static CodeInstruction LdelemRef => new(OpCodes.Ldelem_Ref);

    /// <summary>Replaces the array element at a given index with the object ref value (type O) on the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Ldind_Ref"/>
    public static CodeInstruction StelemRef => new(OpCodes.Stelem_Ref);

    /// <summary>Loads the element with type int32 at a specified array index onto the top of the evaluation stack as an int32.</summary>
    /// <seealso cref="OpCodes.Ldelem_I4"/>
    public static CodeInstruction LdelemI4 => new(OpCodes.Ldelem_I4);

    /// <summary>Replaces the array element at a given index with the int32 value on the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Ldelem_I4"/>
    public static CodeInstruction StelemI4 => new(OpCodes.Stelem_I4);

    /// <summary>Loads a value of type int32 as an int32 onto the evaluation stack indirectly.</summary>
    /// <seealso cref="OpCodes.Ldind_I4"/>
    public static CodeInstruction LdindI4 => new(OpCodes.Ldind_I4);

    /// <summary>Stores a value of type int32 at a supplied address.</summary>
    /// <seealso cref="OpCodes.Stind_I4"/>
    public static CodeInstruction StindI4 => new(OpCodes.Stind_I4);

    /// <summary>
    /// Loads the object or value type of type <typeparamref name="T"/> onto the evaluation stack at a supplied address.
    /// </summary>
    /// <typeparam name="T">The type of value to load.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the value.</returns>
    /// <seealso cref="LdindRef"/>
    /// <seealso cref="LdindI4"/>
    /// <seealso cref="Stind{T}"/>
    public static CodeInstruction Ldind<T>()
    {
        var type = typeof(T);
        return !type.IsValueType
            ? LdindRef
            : type == typeof(sbyte)
                ? new CodeInstruction(OpCodes.Ldind_I1)
                : type == typeof(short)
                    ? new CodeInstruction(OpCodes.Ldind_I2)
                    : type == typeof(int)
                        ? LdindI4
                        : type == typeof(long)
                            ? new CodeInstruction(OpCodes.Ldind_I8)
                            : type == typeof(float)
                                ? new CodeInstruction(OpCodes.Ldind_R4)
                                : type == typeof(double)
                                    ? new CodeInstruction(OpCodes.Ldind_R8)
                                    : type == typeof(byte)
                                        ? new CodeInstruction(OpCodes.Ldind_U1)
                                        : type == typeof(ushort)
                                            ? new CodeInstruction(OpCodes.Ldind_U2)
                                            : type == typeof(uint)
                                                ? new CodeInstruction(OpCodes.Ldind_U4)
                                                : new CodeInstruction(OpCodes.Ldobj); // beautiful btw
    }

    /// <summary>
    /// Stores the object or value type of type <typeparamref name="T"/> at a supplied address.
    /// </summary>
    /// <typeparam name="T">The type of value to store.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the value.</returns>
    /// <seealso cref="StindRef"/>
    /// <seealso cref="StindI4"/>
    /// <seealso cref="Ldind{T}"/>
    public static CodeInstruction Stind<T>()
    {
        var type = typeof(T);
        return !type.IsValueType
            ? StindRef
            : type == typeof(sbyte)
                ? new CodeInstruction(OpCodes.Stind_I1)
                : type == typeof(short)
                    ? new CodeInstruction(OpCodes.Stind_I2)
                    : type == typeof(int)
                        ? StindI4
                        : type == typeof(long)
                            ? new CodeInstruction(OpCodes.Stind_I8)
                            : type == typeof(float)
                                ? new CodeInstruction(OpCodes.Stind_R4)
                                : type == typeof(double)
                                    ? new CodeInstruction(OpCodes.Stind_R8)
                                    : new CodeInstruction(OpCodes.Stobj);
    }

    /// <summary>
    /// Loads the element with type <typeparamref name="T"/> at a specified array index onto the top of the evaluation stack as type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of value to load.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the value.</returns>
    /// <seealso cref="LdelemRef"/>
    /// <seealso cref="LdelemI4"/>
    /// <seealso cref="Stelem{T}"/>
    public static CodeInstruction Ldelem<T>()
    {
        var type = typeof(T);
        return !type.IsValueType
            ? LdelemRef
            : type == typeof(sbyte)
                ? new CodeInstruction(OpCodes.Ldelem_I1)
                : type == typeof(short)
                    ? new CodeInstruction(OpCodes.Ldelem_I2)
                    : type == typeof(int)
                        ? LdelemI4
                        : type == typeof(long)
                            ? new CodeInstruction(OpCodes.Ldelem_I8)
                            : type == typeof(float)
                                ? new CodeInstruction(OpCodes.Ldelem_R4)
                                : type == typeof(double)
                                    ? new CodeInstruction(OpCodes.Ldelem_R8)
                                    : type == typeof(byte)
                                        ? new CodeInstruction(OpCodes.Ldelem_U1)
                                        : type == typeof(ushort)
                                            ? new CodeInstruction(OpCodes.Ldelem_U2)
                                            : type == typeof(uint)
                                                ? new CodeInstruction(OpCodes.Ldelem_U4)
                                                : new CodeInstruction(OpCodes.Ldelem);
    }

    /// <summary>
    /// Replaces the array element at a given index with the value on the top of the evaluation stack.
    /// </summary>
    /// <typeparam name="T">The type of value to store.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the value.</returns>
    /// <seealso cref="StelemRef"/>
    /// <seealso cref="StelemI4"/>
    /// <seealso cref="Ldelem{T}"/>
    public static CodeInstruction Stelem<T>()
    {
        var type = typeof(T);
        return !type.IsValueType
            ? StelemRef
            : type == typeof(sbyte)
                ? new CodeInstruction(OpCodes.Stelem_I1)
                : type == typeof(short)
                    ? new CodeInstruction(OpCodes.Stelem_I2)
                    : type == typeof(int)
                        ? StelemI4
                        : type == typeof(long)
                            ? new CodeInstruction(OpCodes.Stelem_I8)
                            : type == typeof(float)
                                ? new CodeInstruction(OpCodes.Stelem_R4)
                                : type == typeof(double)
                                    ? new CodeInstruction(OpCodes.Stelem_R8)
                                    : new CodeInstruction(OpCodes.Stelem);
    }

    /// <summary>Pushes the number of elements of a zero-based, one-dimensional array onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Ldlen"/>
    public static CodeInstruction Ldlen => new(OpCodes.Ldlen);

    /// <summary>
    /// Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.
    /// </summary>
    /// <param name="type">The type of array to create.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that creates the array.</returns>
    /// <seealso cref="OpCodes.Newarr"/>
    public static CodeInstruction Newarr(Type type) => new(OpCodes.Newarr, type);

    /// <summary>
    /// Pushes an object reference to a new zero-based, one-dimensional array whose elements are of a specific type onto the evaluation stack.
    /// </summary>
    /// <typeparam name="T">The type of array to create.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that creates the array.</returns>
    /// <seealso cref="OpCodes.Newarr"/>
    public static CodeInstruction Newarr<T>() => Newarr(typeof(T));

}
