using InventorySystem.Items.Usables.Scp244.Hypothermia;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo.Effect;

/// <summary>
/// Contains information about a <see cref="Hypothermia"/> effect.
/// </summary>
/// <seealso cref="EffectInfoBase"/>
/// <seealso cref="StandardEffectInfo"/>
public sealed class HypothermiaInfo : EffectInfoBase
{

    /// <summary>
    /// Gets the <see cref="Hypothermia"/> information on the <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to get the effect information from.</param>
    /// <returns>The information about the effect.</returns>
    public static HypothermiaInfo Get(Player player) => (HypothermiaInfo) (player.GetEffect(EffectType.Hypothermia) as Hypothermia);

    /// <summary>
    /// Converts a <see cref="Hypothermia"/> effect to a <see cref="HypothermiaInfo"/>.
    /// </summary>
    /// <param name="hypothermia">The effect to convert.</param>
    /// <returns>The information about the effect.</returns>
    /// <remarks>Explicitly casting a <see cref="Hypothermia"/> effect to a <see cref="HypothermiaInfo"/> will invoke this method.</remarks>
    public static HypothermiaInfo Get(Hypothermia hypothermia)
    {
        if (hypothermia == null)
            return null;
        var duration = hypothermia.TimeLeft;
        var intensity = hypothermia.Intensity;
        var subEffects = hypothermia.SubEffects;
        if (subEffects == null)
            return null;
        var prevExposure = 0f;
        var damageCounter = 0f;
        var decreaseTimer = 0f;
        var humeBlocked = false;
        foreach (var effect in subEffects)
            switch (effect)
            {
                case AttackCooldownSubEffect attack:
                    prevExposure = attack._prevExpo;
                    break;
                case DamageSubEffect damage:
                    damageCounter = damage._damageCounter;
                    break;
                case HumeShieldSubEffect shield:
                    decreaseTimer = shield._decreaseTimer;
                    humeBlocked = shield.HumeShieldBlocked;
                    break;
            }

        return new HypothermiaInfo(hypothermia.IsEnabled, duration, intensity, prevExposure, damageCounter, decreaseTimer, humeBlocked);
    }

    /// <summary>
    /// Creates a <see cref="HypothermiaInfo"/> instance.
    /// </summary>
    /// <param name="isEnabled">Whether the effect is enabled.</param>
    /// <param name="duration">The duration of the effect.</param>
    /// <param name="intensity">The intensity of the effect.</param>
    /// <param name="exposure">The exposure to the fog in <see cref="AttackCooldownSubEffect"/> and <see cref="TemperatureSubEffect"/>.</param>
    /// <param name="damageCounter">The damage counter used in <see cref="DamageSubEffect"/>.</param>
    /// <param name="decreaseTimer">The Hume Shield decrease timer used in <see cref="HumeShieldSubEffect"/>.</param> 
    /// <param name="humeBlocked">Whether the Hume Shield is currently blocked.</param>
    public HypothermiaInfo(bool isEnabled, float duration, byte intensity, float exposure, float damageCounter, float decreaseTimer, bool humeBlocked) : base(isEnabled, duration, intensity)
    {
        Exposure = exposure;
        DamageCounter = damageCounter;
        DecreaseTimer = decreaseTimer;
        HumeBlocked = humeBlocked;
    }

    /// <summary>The exposure to the fog in <see cref="AttackCooldownSubEffect"/> and <see cref="TemperatureSubEffect"/>.</summary>
    public float Exposure { get; set; }

    /// <summary>The damage counter used in <see cref="DamageSubEffect"/>.</summary>
    public float DamageCounter { get; set; }

    /// <summary>The Hume Shield decrease timer used in <see cref="HumeShieldSubEffect"/>.</summary>
    public float DecreaseTimer { get; set; }

    /// <summary>Whether the Hume Shield is currently blocked.</summary>
    public bool HumeBlocked { get; set; }

    /// <inheritdoc />
    public override void ApplyTo(Player player)
    {
        var hypothermia = player.GetEffect(EffectType.Hypothermia) as Hypothermia;
        if (hypothermia == null)
            return;
        if (!IsEnabled)
        {
            hypothermia.IsEnabled = false;
            return;
        }

        hypothermia.IsEnabled = true;
        hypothermia.ServerChangeDuration(Duration);
        hypothermia.Intensity = Intensity;
        var subEffects = hypothermia.SubEffects;
        if (subEffects == null)
            return;
        foreach (var effect in subEffects)
            switch (effect)
            {
                case AttackCooldownSubEffect or TemperatureSubEffect:
                    effect.UpdateEffect(Exposure);
                    break;
                case DamageSubEffect damage:
                    damage._damageCounter = DamageCounter;
                    break;
                case HumeShieldSubEffect shield:
                    shield._decreaseTimer = DecreaseTimer;
                    shield.HumeShieldBlocked = HumeBlocked;
                    break;
            }
    }

    /// <summary>
    /// Converts a <see cref="Hypothermia"/> effect to a <see cref="HypothermiaInfo"/>.
    /// </summary>
    /// <param name="hypothermia">The effect to convert.</param>
    /// <returns>The information about the effect.</returns>
    /// <seealso cref="Get(InventorySystem.Items.Usables.Scp244.Hypothermia.Hypothermia)"/>
    public static explicit operator HypothermiaInfo(Hypothermia hypothermia) => Get(hypothermia);

}
