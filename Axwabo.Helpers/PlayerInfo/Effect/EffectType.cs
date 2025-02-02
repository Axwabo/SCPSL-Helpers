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

    [Obsolete("Blinded has been renamed to Blurred. The Blindness effect fully blacks out the screen.")]
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

    [Obsolete("No longer part of the game.")]
    Metal,

    [Obsolete("No longer part of the game.")]
    OrangeCandy,

    [Obsolete("No longer part of the game.")]
    OrangeWitness,

    Strangled,

    [Obsolete("No longer part of the game.")]
    SugarCrave,

    [Obsolete("No longer part of the game.")]
    SugarRush,

    [Obsolete("No longer part of the game.")]
    TraumatizedByEvil,

    [Obsolete("No longer part of the game.")]
    Prismatic,

    [Obsolete("No longer part of the game.")]
    SlowMetabolism,

    [Obsolete("No longer part of the game.")]
    Spicy,

    [Obsolete("No longer part of the game.")]
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

    Blindness,
    Blurred,
    PitDeath,
    Scp1344,
    SeveredEyes

}
