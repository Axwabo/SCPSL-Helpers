using Mirror;
using PlayerRoles.PlayableScps.Subroutines;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct CooldownInfo : IIsValid {

        public readonly double Snapshot;

        public readonly double InitialUse;

        public readonly double NextUse;

        /// <inheritdoc />
        public bool IsValid { get; }

        public CooldownInfo(double initialUse, double nextUse) {
            InitialUse = initialUse;
            NextUse = nextUse;
            Snapshot = NetworkTime.time;
            IsValid = true;
        }

        public void ApplyTo(AbilityCooldown cooldown) {
            if (!IsValid)
                return;
            var offset = NetworkTime.time - Snapshot;
            cooldown.NextUse = NextUse + offset;
            cooldown.InitialTime = InitialUse + offset;
        }

        public static implicit operator CooldownInfo(AbilityCooldown cooldown) => new(cooldown.InitialTime, cooldown.NextUse);

    }

}
