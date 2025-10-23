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

    BecomingFlamingo,

    Scp559Effect,

    Scp956Target,

    Snowed,

    FogControl,

    Slowness,

    Blindness,
    Blurred,
    PitDeath,
    Scp1344,
    SeveredEyes,
    Scp1344Detected,

    Fade,
    Lightweight,
    HeavyFooted,
    Scp1576,

    TemporaryBypass,
    NightVision,

    WhiteCandy

}
