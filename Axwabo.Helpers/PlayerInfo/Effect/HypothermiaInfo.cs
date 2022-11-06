using Exiled.API.Enums;
using Exiled.API.Features;
using InventorySystem.Items.Usables.Scp244.Hypothermia;

namespace Axwabo.Helpers.PlayerInfo.Effect {

    public sealed class HypothermiaInfo : EffectInfoBase {

        public static HypothermiaInfo Get(Player player) => (HypothermiaInfo) (player.GetEffect(EffectType.Hypothermia) as Hypothermia);

        public float PreviousExposure { get; }

        public float DamageCounter { get; }

        public float DecreaseTimer { get; }

        public bool HumeBlocked { get; }

        public HypothermiaInfo(float duration, byte intensity, float previousExposure, float damageCounter, float decreaseTimer, bool humeBlocked) : base(duration, intensity) {
            PreviousExposure = previousExposure;
            DamageCounter = damageCounter;
            DecreaseTimer = decreaseTimer;
            HumeBlocked = humeBlocked;
        }

        public override void ApplyTo(Player player) {
            var hypothermia = player.GetEffect(EffectType.Hypothermia) as Hypothermia;
            if (hypothermia == null)
                return;
            hypothermia.IsEnabled = true;
            hypothermia.ServerChangeDuration(Duration);
            hypothermia.Intensity = Intensity;
            var subEffects = hypothermia.Get<HypothermiaSubEffectBase[]>("_subEffects");
            if (subEffects == null)
                return;
            foreach (var effect in subEffects)
                switch (effect) {
                    case AttackCooldownSubEffect attack:
                        attack.Set("_prevExpo", PreviousExposure);
                        break;
                    case DamageSubEffect damage:
                        damage.Set("_damageCounter", DamageCounter);
                        break;
                    case HumeShieldSubEffect shield:
                        shield.Set("_decreaseTimer", DecreaseTimer);
                        shield.Set("_humeBlocked", HumeBlocked);
                        break;
                }
        }

        public static explicit operator HypothermiaInfo(Hypothermia hypothermia) {
            if (hypothermia == null)
                return null;
            var duration = hypothermia.TimeLeft;
            var intensity = hypothermia.Intensity;
            var subEffects = hypothermia.Get<HypothermiaSubEffectBase[]>("_subEffects");
            if (subEffects == null)
                return null;
            var prevExposure = 0f;
            var damageCounter = 0f;
            var decreaseTimer = 0f;
            var humeBlocked = false;
            foreach (var effect in subEffects)
                switch (effect) {
                    case AttackCooldownSubEffect attack:
                        prevExposure = attack.Get<float>("_prevExpo");
                        break;
                    case DamageSubEffect damage:
                        damageCounter = damage.Get<float>("_damageCounter");
                        break;
                    case HumeShieldSubEffect shield:
                        decreaseTimer = shield.Get<float>("_decreaseTimer");
                        humeBlocked = shield.Get<bool>("_humeBlocked");
                        break;
                }

            return new HypothermiaInfo(duration, intensity, prevExposure, damageCounter, decreaseTimer, humeBlocked);
        }

    }

}
