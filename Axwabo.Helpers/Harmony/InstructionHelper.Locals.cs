namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>
    /// Loads the local variable at a specific index onto the evaluation stack.
    /// </summary>
    /// <param name="index">The index of the local variable.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the local variable.</returns>
    /// <seealso cref="OpCodes.Ldloc"/>
    public static CodeInstruction Ldloc(int index) => index switch
    {
        0 => new CodeInstruction(OpCodes.Ldloc_0),
        1 => new CodeInstruction(OpCodes.Ldloc_1),
        2 => new CodeInstruction(OpCodes.Ldloc_2),
        3 => new CodeInstruction(OpCodes.Ldloc_3),
        _ => new CodeInstruction(OpCodes.Ldloc, index)
    };

    /// <summary>
    /// Pops the current value from the top of the evaluation stack and stores it in the local variable list at a specified index.
    /// </summary>
    /// <param name="index">The index of the local variable.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the local variable.</returns>
    /// <seealso cref="OpCodes.Stloc"/>
    public static CodeInstruction Stloc(int index) => index switch
    {
        0 => new CodeInstruction(OpCodes.Stloc_0),
        1 => new CodeInstruction(OpCodes.Stloc_1),
        2 => new CodeInstruction(OpCodes.Stloc_2),
        3 => new CodeInstruction(OpCodes.Stloc_3),
        _ => new CodeInstruction(OpCodes.Stloc, index)
    };

    /// <summary>
    /// Loads the specific local variable based on a LocalBuilder instance.
    /// </summary>
    /// <param name="local">The LocalBuilder that contains the variable's index.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the local variable.</returns>
    /// <seealso cref="OpCodes.Ldloc"/>
    public static CodeInstruction Ldloc(LocalBuilder local) => Ldloc(local.LocalIndex);

    /// <summary>
    /// Stores the specific local variable based on a LocalBuilder instance.
    /// </summary>
    /// <param name="local">The LocalBuilder that contains the variable's index.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the local variable.</returns>
    /// <seealso cref="OpCodes.Stloc"/>
    public static CodeInstruction Stloc(LocalBuilder local) => Stloc(local.LocalIndex);

    /// <summary>
    /// Loads the address of the local variable at a specific index onto the evaluation stack.
    /// </summary>
    /// <param name="index">The index of the local variable.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address the local variable.</returns>
    /// <seealso cref="OpCodes.Ldloca"/>
    public static CodeInstruction Ldloca(int index) => new(OpCodes.Ldloca, index);

    /// <summary>
    /// Loads the address of a specific local variable based on a LocalBuilder instance.
    /// </summary>
    /// <param name="local">The LocalBuilder that contains the variable's index.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of local variable.</returns>
    /// <seealso cref="OpCodes.Ldloca"/>
    public static CodeInstruction Ldloca(LocalBuilder local) => Ldloca(local.LocalIndex);

}
