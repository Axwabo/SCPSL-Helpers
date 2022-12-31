using PlayerRoles.PlayableScps.Subroutines;

namespace Axwabo.Helpers.PlayerInfo {

    public readonly struct CooldownInfo {

        public readonly double InitialUse;

        public readonly double NextUse;

        public CooldownInfo(double initialUse, double nextUse) {
            InitialUse = initialUse;
            NextUse = nextUse;
        }

        public void ApplyTo(AbilityCooldown cooldown) {
            cooldown.NextUse = NextUse;
            cooldown.InitialTime = InitialUse;
        }

        public static implicit operator CooldownInfo(AbilityCooldown cooldown) => new(cooldown.InitialTime, cooldown.NextUse);

    }

}
