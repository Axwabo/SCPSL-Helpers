namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>
    /// Calls the (virtual) method indicated by the passed method descriptor.
    /// </summary>
    /// <param name="method">The information about the method.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that calls the method.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the supplied <paramref name="method"/> is null.</exception>
    public static CodeInstruction Call(MethodInfo method) => method == null
        ? throw new ArgumentNullException(nameof(method))
        : method.IsVirtual
            ? new CodeInstruction(OpCodes.Callvirt, method)
            : new CodeInstruction(OpCodes.Call, method);

    /// <summary>
    /// Calls the given static method based on a method group.
    /// </summary>
    /// <param name="method">The method to call.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that calls the method.</returns>
    public static CodeInstruction Call(Delegate method) => Call(method?.Method);

    /// <summary>
    /// Calls the (virtual) method indicated by the passed method descriptor.
    /// </summary>
    /// <param name="type">The type of the object containing the method.</param>
    /// <param name="methodName">The name of the method to call.</param>
    /// <param name="parameters">The type parameters of the method.</param>
    /// <param name="generics">The generics used to call the method.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that calls the method.</returns>
    /// <exception cref="ArgumentException">Thrown if the method was not found.</exception>
    public static CodeInstruction Call(Type type, string methodName, Type[] parameters = null, Type[] generics = null)
        => Call(AccessTools.Method(type, methodName, parameters, generics) ?? throw new ArgumentException($"No method found for type={type.FullName}, name={methodName}, parameters={parameters.Description()}, generics={generics.Description()}"));

    /// <summary>
    /// Calls the (virtual) method indicated by the passed method descriptor.
    /// </summary>
    /// <param name="methodName">The name of the method to call.</param>
    /// <param name="parameters">The type parameters of the method.</param>
    /// <param name="generics">The generics used to call the method.</param>
    /// <typeparam name="T">The type of the object containing the method.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that calls the method.</returns>
    /// <exception cref="ArgumentException">Thrown if the method was not found.</exception>
    /// <remarks>Can only be used with non-static objects because of the type parameter.</remarks>
    public static CodeInstruction Call<T>(string methodName, Type[] parameters = null, Type[] generics = null)
        => Call(typeof(T), methodName, parameters, generics);

    /// <summary>
    /// Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
    /// </summary>
    /// <param name="constructor">The information about the constructor.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that creates a new object.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the constructor was not found.</exception>
    /// <seealso cref="OpCodes.Newobj"/>
    public static CodeInstruction New(ConstructorInfo constructor)
    {
        if (constructor == null)
            throw new ArgumentNullException(nameof(constructor));
        return new CodeInstruction(OpCodes.Newobj, constructor);
    }

    /// <summary>
    /// Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
    /// </summary>
    /// <param name="type">The type of object to create.</param>
    /// <param name="parameters">The parameters of the constructor.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that creates a new object.</returns>
    /// <exception cref="ArgumentException">Thrown if the constructor was not found.</exception>
    /// <seealso cref="OpCodes.Newobj"/>
    public static CodeInstruction New(Type type, Type[] parameters = null)
        => New(AccessTools.Constructor(type, parameters) ?? throw new ArgumentException($"No constructor found for type={type.FullName}, parameters={parameters.Description()}"));

    /// <summary>
    /// Creates a new object or a new instance of a value type, pushing an object reference (type O) onto the evaluation stack.
    /// </summary>
    /// <param name="parameters">The parameters of the constructor.</param>
    /// <typeparam name="T">The type of object to create.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that creates a new object.</returns>
    /// <exception cref="ArgumentException">Thrown if the constructor was not found.</exception>
    /// <seealso cref="OpCodes.Newobj"/>
    public static CodeInstruction New<T>(Type[] parameters = null) => New(typeof(T), parameters);

}
