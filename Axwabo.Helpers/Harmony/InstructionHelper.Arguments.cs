namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>
    /// Loads an argument (referenced by a specified index value) onto the stack.
    /// </summary>
    /// <param name="index">The index of the argument.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the argument.</returns>
    /// <remarks>
    /// In instance methods, arg 0 is "this", 1 is the first argument, 2 is the second argument, and so on.
    /// </remarks>
    /// <seealso cref="OpCodes.Ldarg"/>
    public static CodeInstruction Ldarg(int index) => index switch
    {
        0 => This,
        1 => new CodeInstruction(OpCodes.Ldarg_1),
        2 => new CodeInstruction(OpCodes.Ldarg_2),
        3 => new CodeInstruction(OpCodes.Ldarg_3),
        _ => new CodeInstruction(OpCodes.Ldarg, index)
    };

    /// <summary>
    /// Stores the value on top of the evaluation stack in the argument slot at a specified index.
    /// </summary>
    /// <param name="index">The index of the argument.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the argument.</returns>
    /// <remarks>
    /// In instance methods, arg 0 is "this", 1 is the first argument, 2 is the second argument, and so on.
    /// </remarks>
    /// <seealso cref="OpCodes.Starg"/>
    public static CodeInstruction Starg(int index) => new(OpCodes.Starg, index);

    /// <summary>
    /// Loads an argument's address onto the evaluation stack.
    /// </summary>
    /// <param name="index">The index of the argument.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address the argument.</returns>
    /// <remarks>
    /// In instance methods, arg 0 is "this", 1 is the first argument, 2 is the second argument, and so on.
    /// </remarks>
    /// <seealso cref="OpCodes.Ldarga"/>
    public static CodeInstruction Ldarga(int index) => new(OpCodes.Ldarga, index);

}
