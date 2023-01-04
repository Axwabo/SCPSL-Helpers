using Mirror;
using PlayerRoles.PlayableScps.Subroutines;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct CooldownInfo {

        public readonly double Snapshot;

        public readonly double InitialUse;

        public readonly double NextUse;

        public CooldownInfo(double initialUse, double nextUse) {
            InitialUse = initialUse;
            NextUse = nextUse;
            Snapshot = NetworkTime.time;
        }

        public void ApplyTo(AbilityCooldown cooldown) {
            var offset = NetworkTime.time - Snapshot;
            cooldown.NextUse = NextUse + offset;
            cooldown.InitialTime = InitialUse + offset;
        }

        public static implicit operator CooldownInfo(AbilityCooldown cooldown) => new(cooldown.InitialTime, cooldown.NextUse);

    }

}
