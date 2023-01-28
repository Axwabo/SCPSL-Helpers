using CustomPlayerEffects;
using Exiled.API.Features;

namespace Axwabo.Helpers.PlayerInfo.Effect {

    /// <summary>
    /// An implementation of <see cref="EffectInfoBase"/> for storing information about an effect.
    /// </summary>
    public sealed class StandardEffectInfo : EffectInfoBase {

        /// <summary>
        /// Gets the effect information of the given <paramref name="type"/> on the <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the effect information from.</param>
        /// <param name="type">The type of the effect.</param>
        /// <returns>The information about the effect.</returns>
        public static StandardEffectInfo Get(Player player, EffectType type) => Get(player.GetEffect(type));

        /// <summary>
        /// Converts a <see cref="StatusEffectBase"/> to a <see cref="StandardEffectInfo"/>.
        /// </summary>
        /// <param name="effect">The effect to convert.</param>
        /// <returns>The information about the effect.</returns>
        /// <remarks>Explicitly casting a <see cref="StatusEffectBase"/> to a <see cref="StandardEffectInfo"/> will invoke this method.</remarks>
        public static StandardEffectInfo Get(StatusEffectBase effect) =>
            effect == null ? null : new StandardEffectInfo(effect.IsEnabled, EffectInstanceToEffectType(effect), effect.Duration, effect.Intensity);

        /// <summary>
        /// Creates a <see cref="StandardEffectInfo"/> instance.
        /// </summary>
        /// <param name="isEnabled">Whether the effect is enabled.</param>
        /// <param name="effectType">The type of the effect.</param>
        /// <param name="duration">The duration of the effect.</param>
        /// <param name="intensity">The intensity of the effect.</param>
        public StandardEffectInfo(bool isEnabled, EffectType effectType, float duration, byte intensity) : base(isEnabled, duration, intensity) => EffectType = effectType;

        /// <summary>The type of the effect.</summary>
        public EffectType EffectType { get; }

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            var effect = player.GetEffect(EffectType);
            if (effect == null)
                return;
            if (!IsEnabled) {
                effect.ServerDisable();
                return;
            }

            effect.IsEnabled = true;
            effect.ServerSetState(Intensity, Duration);
        }

        /// <summary>
        /// Converts a <see cref="StatusEffectBase"/> to a <see cref="StandardEffectInfo"/>.
        /// </summary>
        /// <param name="effect">The effect to convert.</param>
        /// <returns>The information about the effect.</returns>
        /// <seealso cref="Get(CustomPlayerEffects.StatusEffectBase)"/>
        public static explicit operator StandardEffectInfo(StatusEffectBase effect) => Get(effect);

    }

}
