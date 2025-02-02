namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>
    /// Loads the specific local variable based on a LocalBuilder instance.
    /// </summary>
    /// <param name="local">The LocalBuilder that contains the variable's index.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the local variable.</returns>
    /// <seealso cref="OpCodes.Ldloc"/>
    /// <seealso cref="Ldloc(LocalBuilder)"/>
    public static CodeInstruction Load(this LocalBuilder local) => Ldloc(local);

    /// <summary>
    /// Stores the specific local variable based on a LocalBuilder instance.
    /// </summary>
    /// <param name="local">The LocalBuilder that contains the variable's index.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that stores the local variable.</returns>
    /// <seealso cref="OpCodes.Stloc"/>
    /// <seealso cref="Stloc(LocalBuilder)"/>
    public static CodeInstruction Set(this LocalBuilder local) => Stloc(local);

    /// <summary>
    /// Loads the address of a specific local variable based on a LocalBuilder instance.
    /// </summary>
    /// <param name="local">The LocalBuilder that contains the variable's index.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the address of local variable.</returns>
    /// <seealso cref="OpCodes.Ldloca"/>
    /// <seealso cref="Ldloca(LocalBuilder)"/>
    public static CodeInstruction LoadAddress(this LocalBuilder local) => Ldloca(local);

    /// <summary>
    /// Loads the integer value of the specified enum.
    /// </summary>
    /// <param name="e">The enum to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the enum.</returns>
    /// <seealso cref="Int"/>
    /// <seealso cref="LoadEnum"/>
    /// <seealso cref="OpCodes.Ldc_I4"/>
    public static CodeInstruction Load(this Enum e) => LoadEnum(e);

    /// <summary>
    /// Transfers control to a target instruction if the value is true, not null, or non-zero.
    /// </summary>
    /// <param name="label">The label of the instruction to jump to.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that jumps to the label if the value is truthy.</returns>
    /// <seealso cref="OpCodes.Brtrue"/>
    public static CodeInstruction True(this Label label) => new(OpCodes.Brtrue, label);

    /// <summary>
    /// Transfers control to a target instruction if value is false, a null reference, or zero.
    /// </summary>
    /// <param name="label">The label of the instruction to jump to.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that jumps to the label if the value is falsy.</returns>
    /// <seealso cref="OpCodes.Brfalse"/>
    public static CodeInstruction False(this Label label) => new(OpCodes.Brfalse, label);

    /// <summary>
    /// Unconditionally transfers control to a target instruction.
    /// </summary>
    /// <param name="label">The label of the instruction to jump to.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that jumps to the label.</returns>
    /// <seealso cref="OpCodes.Brfalse"/>
    public static CodeInstruction Jump(this Label label) => new(OpCodes.Br, label);

    /// <summary>
    /// Exits a protected region of code, unconditionally transferring control to a specific target instruction.
    /// </summary>
    /// <param name="label">The label of the instruction to jump to.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that leaves the current block and jumps to the label.</returns>
    /// <seealso cref="OpCodes.Leave"/>
    public static CodeInstruction Leave(this Label label) => new(OpCodes.Leave, label);

    /// <summary>
    /// Converts a metadata token to its runtime representation, pushing it onto the evaluation stack.
    /// </summary>
    /// <param name="info">The type, method or field to convert.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that converts the metadata token to its runtime representation.</returns>
    /// <seealso cref="OpCodes.Ldtoken"/>
    public static CodeInstruction Load(this MemberInfo info) => LoadToken(info);

    /// <summary>
    /// Creates a LocalBuilder of type <typeparamref name="T"/>.
    /// </summary>
    /// <param name="generator">The ILGenerator object.</param>
    /// <typeparam name="T">The type of the new local variable.</typeparam>
    /// <returns>A LocalBuilder of type <typeparamref name="T"/>.</returns>
    public static LocalBuilder Local<T>(this ILGenerator generator) => generator.DeclareLocal(typeof(T));

}
