using CustomPlayerEffects;
using InventorySystem.Items.MarshmallowMan;
using InventorySystem.Items.Usables.Scp244.Hypothermia;

namespace Axwabo.Helpers.PlayerInfo.Effect;

/// <summary>
/// A base class for storing information about a <see cref="StatusEffectBase"/>.
/// </summary>
/// <seealso cref="StandardEffectInfo"/>
public abstract class EffectInfoBase
{

    /// <summary>
    /// Creates a base object for storing information about an effect.
    /// </summary>
    /// <param name="isEnabled">Whether the effect is enabled.</param>
    /// <param name="duration">The duration of the effect.</param>
    /// <param name="intensity">The intensity of the effect.</param>
    protected EffectInfoBase(bool isEnabled, float duration, byte intensity)
    {
        IsEnabled = isEnabled;
        Duration = duration;
        Intensity = intensity;
    }

    /// <summary>Whether the effect is enabled.</summary>
    public bool IsEnabled { get; set; }

    /// <summary>The remaining time of the effect.</summary>
    public float Duration { get; set; }

    /// <summary>The intensity of the effect.</summary>
    public byte Intensity { get; set; }

    /// <summary>
    /// Applies the information to the effect on the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to apply the effect to.</param>
    public abstract void ApplyTo(Player player);

    /// <summary>
    /// Gets the information about a <see cref="StatusEffectBase"/>.
    /// </summary>
    /// <param name="effect">The effect to get the information from.</param>
    /// <returns>The information about the effect.</returns>
    public static EffectInfoBase FromEffect(StatusEffectBase effect) => effect switch
    {
        Hypothermia h => (HypothermiaInfo) h,
        _ => (StandardEffectInfo) effect
    };

    /// <summary>
    /// Converts a <see cref="StatusEffectBase"/> enumerable to an <see cref="EffectInfoBase"/> list.
    /// </summary>
    /// <param name="effects">The effects to convert.</param>
    /// <returns>The list of converted effects.</returns>
    public static List<EffectInfoBase> EffectsToList(IEnumerable<StatusEffectBase> effects) => effects?.Select(FromEffect).ToList();

    /// <summary>
    /// Converts the <see cref="Type"/> of an <see cref="StatusEffectBase">effect object</see> to an <see cref="EffectType"/>.
    /// </summary>
    /// <param name="effect">The effect to get the type of.</param>
    /// <returns>The type of the effect.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the effect type is unknown.</exception>
    public static EffectType EffectInstanceToEffectType(StatusEffectBase effect) => effect == null
        ? EffectType.None
        : effect switch
        {
            AmnesiaItems => EffectType.AmnesiaItems,
            AmnesiaVision => EffectType.AmnesiaVision,
            AntiScp207 => EffectType.AntiScp207,
            Asphyxiated => EffectType.Asphyxiated,
            BecomingFlamingo => EffectType.BecomingFlamingo,
            Bleeding => EffectType.Bleeding,
            BodyshotReduction => EffectType.BodyshotReduction,
            Burned => EffectType.Burned,
            CardiacArrest => EffectType.CardiacArrest,
            Concussed => EffectType.Concussed,
            Corroding => EffectType.Corroding,
            DamageReduction => EffectType.DamageReduction,
            Deafened => EffectType.Deafened,
            Decontaminating => EffectType.Decontaminating,
            Disabled => EffectType.Disabled,
            Ensnared => EffectType.Ensnared,
            Exhausted => EffectType.Exhausted,
            Flashed => EffectType.Flashed,
            Hemorrhage => EffectType.Hemorrhage,
            Hypothermia => EffectType.Hypothermia,
            Scp559Effect => EffectType.Scp559Effect,
            Scp956Target => EffectType.Scp956Target,
            Snowed => EffectType.Snowed,
            InsufficientLighting => EffectType.InsufficientLighting,
            Invigorated => EffectType.Invigorated,
            Invisible => EffectType.Invisible,
            MovementBoost => EffectType.MovementBoost,
            Poisoned => EffectType.Poisoned,
            RainbowTaste => EffectType.RainbowTaste,
            Scanned => EffectType.Scanned,
            Scp1853 => EffectType.Scp1853,
            Scp207 => EffectType.Scp207,
            SeveredHands => EffectType.SeveredHands,
            Sinkhole => EffectType.Sinkhole,
            SoundtrackMute => EffectType.SoundtrackMute,
            SpawnProtected => EffectType.SpawnProtected,
            Stained => EffectType.Stained,
            Traumatized => EffectType.Traumatized,
            Vitality => EffectType.Vitality,
            PocketCorroding => EffectType.PocketCorroding,
            Ghostly => EffectType.Ghostly,
            MarshmallowEffect => EffectType.MarshmallowEffect,
            Strangled => EffectType.Strangled,
            SilentWalk => EffectType.SilentWalk,
            FogControl => EffectType.FogControl,
            Slowness => EffectType.Slowness,
            Blindness => EffectType.Blindness,
            Blurred => EffectType.Blurred,
            PitDeath => EffectType.PitDeath,
            Scp1344 => EffectType.Scp1344,
            SeveredEyes => EffectType.SeveredEyes,
            _ => throw new InvalidOperationException("Unknown effect provided: " + effect.GetType().Name)
        };

    /// <summary>
    /// Converts the <see cref="EffectType"/> to a <see cref="Type"/>.
    /// </summary>
    /// <param name="effectType">The effect type to convert.</param>
    /// <returns>The type of the effect.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the effect type is unknown.</exception>
    public static Type EffectTypeToSystemType(EffectType effectType) => effectType switch
    {
        EffectType.None => null,
        EffectType.AmnesiaItems => typeof(AmnesiaItems),
        EffectType.AmnesiaVision => typeof(AmnesiaVision),
        EffectType.AntiScp207 => typeof(AntiScp207),
        EffectType.Asphyxiated => typeof(Asphyxiated),
        EffectType.Bleeding => typeof(Bleeding),
#pragma warning disable CS0618
        EffectType.Blinded => typeof(Blurred),
#pragma warning restore CS0618
        EffectType.BodyshotReduction => typeof(BodyshotReduction),
        EffectType.Burned => typeof(Burned),
        EffectType.CardiacArrest => typeof(CardiacArrest),
        EffectType.Concussed => typeof(Concussed),
        EffectType.Corroding => typeof(Corroding),
        EffectType.DamageReduction => typeof(DamageReduction),
        EffectType.Deafened => typeof(Deafened),
        EffectType.Decontaminating => typeof(Decontaminating),
        EffectType.Disabled => typeof(Disabled),
        EffectType.Ensnared => typeof(Ensnared),
        EffectType.Exhausted => typeof(Exhausted),
        EffectType.Flashed => typeof(Flashed),
        EffectType.Hemorrhage => typeof(Hemorrhage),
        EffectType.Hypothermia => typeof(Hypothermia),
        EffectType.InsufficientLighting => typeof(InsufficientLighting),
        EffectType.Invigorated => typeof(Invigorated),
        EffectType.Invisible => typeof(Invisible),
        EffectType.MovementBoost => typeof(MovementBoost),
        EffectType.Poisoned => typeof(Poisoned),
        EffectType.RainbowTaste => typeof(RainbowTaste),
        EffectType.Scanned => typeof(Scanned),
        EffectType.Scp1853 => typeof(Scp1853),
        EffectType.Scp207 => typeof(Scp207),
        EffectType.SeveredHands => typeof(SeveredHands),
        EffectType.Sinkhole => typeof(Sinkhole),
        EffectType.SoundtrackMute => typeof(SoundtrackMute),
        EffectType.SpawnProtected => typeof(SpawnProtected),
        EffectType.Stained => typeof(Stained),
        EffectType.Traumatized => typeof(Traumatized),
        EffectType.Vitality => typeof(Vitality),
        EffectType.PocketCorroding => typeof(PocketCorroding),
        EffectType.Ghostly => typeof(Ghostly),
        EffectType.MarshmallowEffect => typeof(MarshmallowEffect),
        EffectType.Strangled => typeof(Strangled),
        EffectType.SilentWalk => typeof(SilentWalk),
        EffectType.FogControl => typeof(FogControl),
        EffectType.Slowness => typeof(Slowness),
        EffectType.Blindness => typeof(Blindness),
        EffectType.Blurred => typeof(Blurred),
        EffectType.PitDeath => typeof(PitDeath),
        EffectType.Scp1344 => typeof(Scp1344),
        EffectType.SeveredEyes => typeof(SeveredEyes),
        EffectType.BecomingFlamingo => typeof(BecomingFlamingo),
        EffectType.Scp559Effect => typeof(Scp559Effect),
        EffectType.Scp956Target => typeof(Scp956Target),
        EffectType.Snowed => typeof(Snowed),
        EffectType.Scp1344Detected => typeof(Scp1344Detected),
        _ => throw new ArgumentOutOfRangeException(nameof(effectType), effectType, "Unknown effect type " + effectType)
    };

}
