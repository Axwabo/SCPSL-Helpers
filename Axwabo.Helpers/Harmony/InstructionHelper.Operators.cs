namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>Adds two values and pushes the result onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Add"/>
    public static CodeInstruction Add => new(OpCodes.Add);

    /// <summary>Subtracts one value from another and pushes the result onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Sub"/>
    public static CodeInstruction Subtract => new(OpCodes.Sub);

    /// <summary>Multiplies two values and pushes the result on the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Mul"/>
    public static CodeInstruction Multiply => new(OpCodes.Mul);

    /// <summary>Divides two values and pushes the result as a floating-point (type F) or quotient (type int32) onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Div"/>
    public static CodeInstruction Divide => new(OpCodes.Div);

    /// <summary>Divides two values and pushes the remainder onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Rem"/>
    public static CodeInstruction Modulo => new(OpCodes.Rem);

    /// <summary>Computes the bitwise complement of the integer value on top of the stack and pushes the result onto the evaluation stack as the same type.</summary>
    /// <seealso cref="OpCodes.Not"/>
    public static CodeInstruction BitwiseNot => new(OpCodes.Not);

    /// <summary>Computes the bitwise AND of two values and pushes the result onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.And"/>
    public static CodeInstruction BitwiseAnd => new(OpCodes.And);

    /// <summary>Computes the bitwise complement of the two integer values on top of the stack and pushes the result onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Xor"/>
    public static CodeInstruction BitwiseOr => new(OpCodes.Or);

    /// <summary>Computes the bitwise XOR of the top two values on the evaluation stack, pushing the result onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Xor"/>
    public static CodeInstruction BitwiseXor => new(OpCodes.Xor);

    /// <summary>
    /// Shifts an integer value to the left (in zeroes) by a specified number of bits, pushing the result onto the evaluation stack.
    /// </summary>
    /// <param name="bits">The number of bits to shift by.</param>
    /// <seealso cref="OpCodes.Shl"/>
    public static CodeInstruction ShiftLeft(int bits) => new(OpCodes.Shl, bits);

    /// <summary>
    /// Shifts an integer value (in sign) to the right by a specified number of bits, pushing the result onto the evaluation stack.
    /// </summary>
    /// <param name="bits">The number of bits to shift by.</param>
    /// <seealso cref="OpCodes.Shr"/>
    public static CodeInstruction ShiftRight(int bits) => new(OpCodes.Shr, bits);

    /// <summary>Compares two values.</summary>
    /// <seealso cref="OpCodes.Ceq"/>
    public static CodeInstruction Equality => new(OpCodes.Ceq);

    /// <summary>Compares two values. If the first value is greater than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Cgt"/>
    public static CodeInstruction GreaterThan => new(OpCodes.Cgt);

    /// <summary>Compares two values. If the first value is less than the second, the integer value 1 (int32) is pushed onto the evaluation stack; otherwise 0 (int32) is pushed onto the evaluation stack.</summary>
    /// <seealso cref="OpCodes.Clt"/>
    public static CodeInstruction LessThan => new(OpCodes.Clt);

}
