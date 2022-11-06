using System;
using System.Collections.Generic;
using System.Linq;
using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;
using InventorySystem.Items.Usables.Scp244.Hypothermia;

namespace Axwabo.Helpers.PlayerInfo.Effect {

    public abstract class EffectInfoBase {

        protected EffectInfoBase(float duration, byte intensity) {
            Duration = duration;
            Intensity = intensity;
        }

        public float Duration { get; }

        public byte Intensity { get; }

        public abstract void ApplyTo(Player player);

        public static EffectInfoBase FromEffect(PlayerEffect effect) => effect switch {
            Hypothermia h => (HypothermiaInfo) h,
            _ => StandardEffectInfo.Get(effect, GetEffectType(effect))
        };

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

        public static List<EffectInfoBase> EffectsToList(IEnumerable<PlayerEffect> effects) => effects?.Select(FromEffect).ToList();

    }

}
