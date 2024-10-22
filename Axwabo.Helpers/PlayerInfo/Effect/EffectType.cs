using System;

#pragma warning disable CS1591
namespace Axwabo.Helpers.PlayerInfo.Effect;

/// <summary>
/// All effect types in the game.
/// </summary>
/// <seealso cref="EffectInfoBase.EffectTypeToSystemType"/>
/// <seealso cref="EffectInfoBase.EffectInstanceToEffectType"/>
public enum EffectType
{

    None,
    AmnesiaItems,
    AmnesiaVision,
    AntiScp207,
    Asphyxiated,
    Bleeding,
    Blinded,
    BodyshotReduction,
    Burned,
    CardiacArrest,
    Concussed,
    Corroding,
    DamageReduction,
    Deafened,
    Decontaminating,
    Disabled,
    Ensnared,
    Exhausted,
    Flashed,
    Hemorrhage,
    Hypothermia,
    InsufficientLighting,
    Invigorated,
    Invisible,
    MovementBoost,
    Poisoned,
    RainbowTaste,
    Scanned,
    Scp1853,
    Scp207,
    SeveredHands,
    Sinkhole,
    SoundtrackMute,
    SpawnProtected,
    Stained,
    Traumatized,
    Vitality,
    PocketCorroding,
    Ghostly,
    MarshmallowEffect,

    Metal,

    OrangeCandy,

    OrangeWitness,

    Strangled,

    SugarCrave,

    SugarRush,

    TraumatizedByEvil,

    Prismatic,

    SlowMetabolism,

    Spicy,

    SugarHigh,

    SilentWalk,

    [Obsolete("No longer part of the game.")]
    BecomingFlamingo,

    [Obsolete("No longer part of the game.")]
    Scp559Effect,

    [Obsolete("No longer part of the game.")]
    Scp956Target,

    [Obsolete("No longer part of the game.")]
    Snowed,

    FogControl,

    Slowness,

    TemporaryBypass

}
