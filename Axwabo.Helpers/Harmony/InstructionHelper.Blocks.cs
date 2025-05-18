namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>
    /// Adds an exception block of type <paramref name="type"/> to the <paramref name="instruction"/>.
    /// </summary>
    /// <param name="instruction">The instruction to add the block to.</param>
    /// <param name="type">The type of block to add.</param>
    /// <returns>The <see cref="CodeInstruction"/> itself.</returns>
    /// <remarks>For a catch block, consider using the <see cref="Catch"/> method, allowing the specification of the exception type.</remarks>
    public static CodeInstruction WithBlock(this CodeInstruction instruction, ExceptionBlockType type)
        => instruction.WithBlocks(new ExceptionBlock(type));

    /// <summary>
    /// Begins a try block.
    /// </summary>
    /// <param name="instruction">The instruction to add the block to.</param>
    /// <returns>The <see cref="CodeInstruction"/> itself.</returns>
    public static CodeInstruction Try(this CodeInstruction instruction)
        => instruction.WithBlock(ExceptionBlockType.BeginExceptionBlock);

    /// <summary>
    /// Begins a catch block.
    /// </summary>
    /// <param name="instruction">The instruction to add the block to.</param>
    /// <param name="catchType">The type of exception to catch. If null, all exceptions will be caught.</param>
    /// <returns>The <see cref="CodeInstruction"/> itself.</returns>
    /// <remarks>
    /// A "catch-all" block only executes if the preceding catch blocks (if any) have not yet processed the exception.
    /// To end the current exception block (if no finally block is present after the catch block), call <see cref="EndException"/>
    /// </remarks>
    public static CodeInstruction Catch(this CodeInstruction instruction, Type catchType = null)
        => instruction.WithBlocks(new ExceptionBlock(ExceptionBlockType.BeginCatchBlock, catchType));

    /// <summary>
    /// Begins a finally block.
    /// </summary>
    /// <param name="instruction">The instruction to add the block to.</param>
    /// <returns>The <see cref="CodeInstruction"/> itself.</returns>
    public static CodeInstruction Finally(this CodeInstruction instruction)
        => instruction.WithBlock(ExceptionBlockType.BeginFinallyBlock);

    /// <summary>
    /// Closes the current exception block, terminating a catch or finally block.
    /// </summary>
    /// <param name="instruction">The instruction to add the block to.</param>
    /// <returns>The <see cref="CodeInstruction"/> itself.</returns>
    public static CodeInstruction EndException(this CodeInstruction instruction)
        => instruction.WithBlock(ExceptionBlockType.EndExceptionBlock);

    /// <summary>An <see cref="CodeInstruction">instruction</see> that ends the current finally block and closes the exception block.</summary>
    /// <seealso cref="OpCodes.Endfinally"/>
    /// <seealso cref="EndException"/>
    public static CodeInstruction EndFinally => new CodeInstruction(OpCodes.Endfinally).EndException();

}
