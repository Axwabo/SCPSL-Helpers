namespace Axwabo.Helpers.Harmony;

/// <summary>
/// A helper class to assist in creating <see cref="CodeInstruction">CodeInstructions</see>.
/// </summary>
public static partial class InstructionHelper
{

    /// <summary>Loads the argument at index 0 (this in non-static context) onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Ldarg_0"/>
    public static CodeInstruction This => new(OpCodes.Ldarg_0);

    /// <summary>Returns from the current method.</summary>
    /// <seealso cref="OpCodes.Ret"/>
    public static CodeInstruction Return => new(OpCodes.Ret);

    /// <summary>Copies the current topmost value on the evaluation stack, and then pushes the copy onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Dup"/>
    public static CodeInstruction Duplicate => new(OpCodes.Dup);

    /// <summary>Removes the value currently on top of the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Pop"/>
    public static CodeInstruction Pop => new(OpCodes.Pop);

    /// <summary>Pushes a null reference (type O) onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Ldnull"/>
    public static CodeInstruction Null => new(OpCodes.Ldnull);

    /// <summary>Fills space if opcodes are patched. No meaningful operation is performed although a processing cycle can be consumed.</summary>
    /// <seealso cref="OpCodes.Nop"/>
    public static CodeInstruction Nop => new(OpCodes.Nop);

    /// <summary>
    /// Pushes a new object reference to a string literal stored in the metadata.
    /// </summary>
    /// <param name="s">The string to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the string.</returns>
    public static CodeInstruction String(string s) => new(OpCodes.Ldstr, s);

    /// <summary>
    /// Converts a value type to an object reference (type O).
    /// </summary>
    /// <param name="type">The type of value to convert to an object.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that converts a value type to an object.</returns>
    /// <seealso cref="OpCodes.Box"/>
    public static CodeInstruction Box(Type type) => new(OpCodes.Box, type);

    /// <summary>
    /// Converts a value type to an object reference (type O).
    /// </summary>
    /// <typeparam name="T">The type of value to convert to an object.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that converts a value type to an object.</returns>
    /// <seealso cref="OpCodes.Box"/>
    public static CodeInstruction Box<T>() => Box(typeof(T));

    /// <summary>
    /// Converts the boxed representation of a value type to its unboxed form.
    /// </summary>
    /// <param name="type">The type of value to convert to.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that converts a boxed value type to its unboxed form.</returns>
    /// <seealso cref="OpCodes.Unbox"/>
    public static CodeInstruction Unbox(Type type) => new(OpCodes.Unbox, type);

    /// <summary>
    /// Converts the boxed representation of a value type to its unboxed form.
    /// </summary>
    /// <typeparam name="T">The type of value to convert to.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that converts a boxed value type to its unboxed form.</returns>
    /// <seealso cref="OpCodes.Unbox"/>
    public static CodeInstruction Unbox<T>() => Box(typeof(T));

    /// <summary>
    /// Attempts to cast an object passed by reference to the specified class.
    /// </summary>
    /// <param name="type">The type to cast to.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that converts the object to a different type.</returns>
    /// <seealso cref="OpCodes.Castclass"/>
    public static CodeInstruction Cast(Type type) => new(OpCodes.Castclass, type);

    /// <summary>
    /// Attempts to cast an object passed by reference to the specified class.
    /// </summary>
    /// <typeparam name="T">The type to cast to.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that converts the object to a different type.</returns>
    /// <seealso cref="OpCodes.Castclass"/>
    public static CodeInstruction Cast<T>() => Cast(typeof(T));

    /// <summary>
    /// Tests whether an object reference (type O) is an instance of a particular class.
    /// </summary>
    /// <param name="type">The type to check.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that checks for a particular class.</returns>
    public static CodeInstruction IsInstance(Type type) => new(OpCodes.Isinst, type);

    /// <summary>
    /// Tests whether an object reference (type O) is an instance of a particular class.
    /// </summary>
    /// <typeparam name="T">The type to check.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that checks for a particular class.</returns>
    /// <seealso cref="OpCodes.Isinst"/>
    public static CodeInstruction IsInstance<T>() => IsInstance(typeof(T));

    /// <summary>
    /// Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.
    /// </summary>
    /// <param name="info">The type, method or field to convert.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that converts the metadata token to its runtime representation.</returns>
    /// <seealso cref="OpCodes.Ldtoken"/>
    public static CodeInstruction LoadToken(MemberInfo info) => new(OpCodes.Ldtoken, info);

    /// <summary>
    /// Loads a type token for the specified type onto the evaluation stack.
    /// </summary>
    /// <typeparam name="T">The type to load.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the type token.</returns>
    /// <seealso cref="OpCodes.Ldtoken"/>
    /// <seealso cref="LoadToken"/>
    public static CodeInstruction LoadTypeToken<T>() => LoadToken(typeof(T));

    /// <summary>
    /// Copies the value type object pointed to by an address to the top of the evaluation stack.
    /// </summary>
    /// <param name="type">The type of value to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the value type.</returns>
    /// <seealso cref="OpCodes.Ldobj"/>
    public static CodeInstruction Ldobj(Type type) => new(OpCodes.Ldobj, type);

    /// <summary>
    /// Copies the value type object pointed to by an address to the top of the evaluation stack.
    /// </summary>
    /// <typeparam name="T">The type of value to load.</typeparam>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the value type.</returns>
    /// <seealso cref="OpCodes.Ldobj"/>
    public static CodeInstruction Ldobj<T>() => Ldobj(typeof(T));

}
