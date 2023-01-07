using Mirror;
using PlayerRoles.PlayableScps.Subroutines;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    /// <summary>
    /// A struct containing relative time information about an <see cref="AbilityCooldown"/>.
    /// </summary>
    public readonly struct CooldownInfo : IIsValid {

        /// <summary>
        /// The network time this struct was created.
        /// </summary>
        public readonly double Snapshot;

        /// <summary>
        /// The initial usage of the ability.
        /// </summary>
        public readonly double InitialUse;

        /// <summary>
        /// The next possible usage of the ability.
        /// </summary>
        public readonly double NextUse;

        /// <inheritdoc />
        public bool IsValid { get; }

        /// <summary>
        /// Creates a new <see cref="CooldownInfo"/> instance.
        /// </summary>
        /// <param name="initialUse">The initial usage of the ability.</param>
        /// <param name="nextUse">The next possible usage of the ability.</param>
        public CooldownInfo(double initialUse, double nextUse) {
            InitialUse = initialUse;
            NextUse = nextUse;
            Snapshot = NetworkTime.time;
            IsValid = true;
        }

        /// <summary>
        /// Applies the relative time information to the given <see cref="AbilityCooldown"/>.
        /// </summary>
        /// <param name="cooldown">The cooldown to apply the information to.</param>
        public void ApplyTo(AbilityCooldown cooldown) {
            if (!IsValid)
                return;
            var offset = NetworkTime.time - Snapshot;
            cooldown.NextUse = NextUse + offset;
            cooldown.InitialTime = InitialUse + offset;
        }

        /// <summary>
        /// Converts an <see cref="AbilityCooldown"/> to a <see cref="CooldownInfo"/>.
        /// </summary>
        /// <param name="cooldown">The cooldown to convert.</param>
        /// <returns>The converted cooldown.</returns>
        public static implicit operator CooldownInfo(AbilityCooldown cooldown) => new(cooldown.InitialTime, cooldown.NextUse);

    }

}
