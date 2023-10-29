using System;
using System.Reflection.Emit;
using HarmonyLib;

namespace Axwabo.Helpers.Harmony;

public static partial class InstructionHelper
{

    /// <summary>Pushes the integer value of 0 onto the evaluation stack as an <see cref="int">int32</see>.</summary>
    /// <seealso cref="OpCodes.Ldc_I4_0"/>
    /// <remarks>Has a false value upon checking a condition.</remarks>
    public static CodeInstruction Int0 => new(OpCodes.Ldc_I4_0);

    /// <summary>Pushes the integer value of 1 onto the evaluation stack as an <see cref="int">int32</see>.</summary>
    /// <seealso cref="OpCodes.Ldarg_0"/>
    /// <remarks>Has a true value upon checking a condition.</remarks>
    public static CodeInstruction Int1 => new(OpCodes.Ldc_I4_1);

    /// <summary>
    /// Pushes a supplied value of type int32 onto the evaluation stack as an <see cref="int">int32</see>.
    /// </summary>
    /// <param name="i">The integer to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the specific integer.</returns>
    /// <seealso cref="OpCodes.Ldc_I4"/>
    public static CodeInstruction Int(int i) => i switch
    {
        0 => Int0,
        1 => Int1,
        2 => new CodeInstruction(OpCodes.Ldc_I4_2),
        3 => new CodeInstruction(OpCodes.Ldc_I4_3),
        4 => new CodeInstruction(OpCodes.Ldc_I4_4),
        5 => new CodeInstruction(OpCodes.Ldc_I4_5),
        6 => new CodeInstruction(OpCodes.Ldc_I4_6),
        7 => new CodeInstruction(OpCodes.Ldc_I4_7),
        8 => new CodeInstruction(OpCodes.Ldc_I4_8),
        _ => new CodeInstruction(OpCodes.Ldc_I4, i)
    };

    /// <summary>Pushes the integer value of 0 onto the evaluation stack as an <see cref="long">int64</see>.</summary>
    public static CodeInstruction Long0 => new(OpCodes.Ldc_I8, 0L);

    /// <summary>
    /// Pushes a supplied value of type int64 onto the evaluation stack as an <see cref="long">int64</see>.
    /// </summary>
    /// <param name="l">The long to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the specific long.</returns>
    public static CodeInstruction Long(long l) => new(OpCodes.Ldc_I8, l);

    /// <summary>Pushes the float value of 0 onto the evaluation stack as a <see cref="float">float32</see>.</summary>
    /// <seealso cref="OpCodes.Ldc_R4"/>
    public static CodeInstruction Float0 => new(OpCodes.Ldc_R4, 0f);

    /// <summary>
    /// Pushes a supplied value of type <see cref="float">int32</see> onto the evaluation stack as type F (float).
    /// </summary>
    /// <param name="f">The float to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the specific float.</returns>
    /// <seealso cref="OpCodes.Ldc_R4"/>
    public static CodeInstruction Float(float f) => new(OpCodes.Ldc_R4, f);

    /// <summary>Pushes 0 of type float64 onto the evaluation stack as type F (float).</summary>
    /// <seealso cref="OpCodes.Ldc_R8"/>
    public static CodeInstruction Double0 => new(OpCodes.Ldc_R8, 0d);

    /// <summary>
    /// Pushes a supplied value of type float64 onto the evaluation stack as type F (float).
    /// </summary>
    /// <param name="d">The double to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the specific double.</returns>
    public static CodeInstruction Double(double d) => new(OpCodes.Ldc_R8, d);

    /// <summary>
    /// Loads the integer value of the specified enum.
    /// </summary>
    /// <param name="e">The enum to load.</param>
    /// <returns>An <see cref="CodeInstruction">instruction</see> that loads the enum.</returns>
    /// <seealso cref="Int"/>
    /// <seealso cref="OpCodes.Ldc_I4"/>
    public static CodeInstruction LoadEnum(Enum e) => Int(Convert.ToInt32(e));

}
