using CustomPlayerEffects;
using Exiled.API.Enums;
using Exiled.API.Features;

namespace Axwabo.Helpers.PlayerInfo.Effect {

    public sealed class StandardEffectInfo : EffectInfoBase {

        public static StandardEffectInfo Get(Player player, EffectType type) => Get(player.GetEffect(type), type);

        public static StandardEffectInfo Get(PlayerEffect effect, EffectType type) => effect == null || !effect.IsEnabled ? null : new StandardEffectInfo(effect.Duration, effect.Intensity, type);

        public EffectType EffectType { get; }

        public StandardEffectInfo(float duration, byte intensity, EffectType effectType) : base(duration, intensity) => EffectType = effectType;

        public override void ApplyTo(Player player) {
            var effect = player.GetEffect(EffectType);
            player.EnableEffect(EffectType, Duration);
            effect.Intensity = Intensity;
        }

    }

}
