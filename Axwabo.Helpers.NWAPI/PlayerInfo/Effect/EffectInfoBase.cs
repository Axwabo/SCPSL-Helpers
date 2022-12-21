using System;
using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using InventorySystem.Items.Usables.Scp244.Hypothermia;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo.Effect {

    /// <summary>
    /// A base class for storing information about a <see cref="PlayerEffect"/>.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    public abstract class EffectInfoBase {

        /// <summary>
        /// Creates a base object for storing information about an effect.
        /// </summary>
        /// <param name="isEnabled">Whether the effect is enabled.</param>
        /// <param name="duration">The duration of the effect.</param>
        /// <param name="intensity">The intensity of the effect.</param>
        protected EffectInfoBase(bool isEnabled, float duration, byte intensity) {
            IsEnabled = isEnabled;
            Duration = duration;
            Intensity = intensity;
        }

        /// <summary>
        /// Whether the effect is enabled.
        /// </summary>
        public bool IsEnabled { get; }

        /// <summary>
        /// The remaining time of the effect.
        /// </summary>
        public float Duration { get; }

        /// <summary>
        /// The intensity of the effect.
        /// </summary>
        public byte Intensity { get; }

        /// <summary>
        /// Applies the information to the effect on the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to apply the effect to.</param>
        public abstract void ApplyTo(Player player);

        /// <summary>
        /// Gets the information about a <see cref="PlayerEffect"/>.
        /// </summary>
        /// <param name="effect">The effect to get the information from.</param>
        /// <returns>The information about the effect.</returns>
        public static EffectInfoBase FromEffect(PlayerEffect effect) => effect switch {
            Hypothermia h => (HypothermiaInfo) h,
            _ => (StandardEffectInfo) effect
        };

        /// <summary>
        /// Converts a <see cref="PlayerEffect"/> enumerable to an <see cref="EffectInfoBase"/> list.
        /// </summary>
        /// <param name="effects">The effects to convert.</param>
        /// <returns>The list of converted effects.</returns>
        public static List<EffectInfoBase> EffectsToList(IEnumerable<PlayerEffect> effects) => effects?.Select(FromEffect).ToList();

        /// <summary>
        /// Converts the <see cref="Type"/> of an <see cref="PlayerEffect">effect object</see> to an <see cref="EffectType"/>.
        /// </summary>
        /// <param name="effect">The effect to get the type of.</param>
        /// <returns>The type of the effect.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the effect type is unknown.</exception>
        public static EffectType GetEffectType(PlayerEffect effect) => effect switch {
            Amnesia => EffectType.Amnesia,
            Asphyxiated => EffectType.Asphyxiated,
            Bleeding => EffectType.Bleeding,
            Blinded => EffectType.Blinded,
            Burned => EffectType.Burned,
            Concussed => EffectType.Concussed,
            Corroding => EffectType.Corroding,
            Deafened => EffectType.Deafened,
            Decontaminating => EffectType.Decontaminating,
            Disabled => EffectType.Disabled,
            Ensnared => EffectType.Ensnared,
            Exhausted => EffectType.Exhausted,
            Flashed => EffectType.Flashed,
            Hemorrhage => EffectType.Hemorrhage,
            Invigorated => EffectType.Invigorated,
            BodyshotReduction => EffectType.BodyshotReduction,
            Poisoned => EffectType.Poisoned,
            Scp207 => EffectType.Scp207,
            Invisible => EffectType.Invisible,
            SinkHole => EffectType.SinkHole,
            Visuals939 => EffectType.Visuals939,
            DamageReduction => EffectType.DamageReduction,
            MovementBoost => EffectType.MovementBoost,
            RainbowTaste => EffectType.RainbowTaste,
            SeveredHands => EffectType.SeveredHands,
            Stained => EffectType.Stained,
            Visuals173Blink => EffectType.Visual173Blink,
            Vitality => EffectType.Vitality,
            Hypothermia => EffectType.Hypothermia,
            Scp1853 => EffectType.Scp1853,
            _ => throw new InvalidOperationException("Unknown effect provided")
        };

    }

}
