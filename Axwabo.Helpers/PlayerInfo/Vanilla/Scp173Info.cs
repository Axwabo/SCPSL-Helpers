using Axwabo.Helpers.PlayerInfo.Containers;
using Exiled.API.Features;
using Mirror;
using PlayerRoles.PlayableScps.Scp173;

namespace Axwabo.Helpers.PlayerInfo.Vanilla {

    /// <summary>
    /// Contains information about SCP-173.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    /// <seealso cref="PlayerInfoBase"/>
    /// <seealso cref="Scp173Role"/>
    public sealed class Scp173Info : PlayerInfoBase {

        /// <summary>
        /// Creates an <see cref="Scp173Info"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>The information about SCP-173.</returns>
        public static Scp173Info Get(Player player) {
            var routines = Scp173SubroutineContainer.Get(player.RoleAs<Scp173Role>());
            if (!routines.IsValid)
                return null;
            var time = NetworkTime.time;
            var blink = routines.BlinkTimer;
            var breakneck = routines.BreakneckSpeeds;
            return new Scp173Info(
                blink._totalCooldown,
                blink._initialStopTime - time,
                blink._endSustainTime - time,
                breakneck.IsActive,
                breakneck._disableTime - breakneck.Elapsed,
                breakneck.Cooldown,
                routines.Tantrum.Cooldown,
                BasicRoleInfo.Get(player)
            );
        }

        /// <summary>
        /// Checks if the given player is SCP-173.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-173.</returns>
        public static bool Is173(Player p) => p.RoleIs<Scp173Role>();

        public Scp173Info(float totalBlinkCooldown,
            double blinkInitialStop,
            double blinkEndSustainTime,
            bool breakneckSpeedsActive,
            float breakneckSpeedsRemainingTime,
            CooldownInfo breakneckSpeedsCooldown,
            CooldownInfo tantrumCooldown,
            BasicRoleInfo basicRoleInfo) : base(basicRoleInfo) {
            TotalBlinkCooldown = totalBlinkCooldown;
            BlinkInitialStop = blinkInitialStop;
            BlinkEndSustainTime = blinkEndSustainTime;
            BreakneckSpeedsActive = breakneckSpeedsActive;
            BreakneckSpeedsRemainingTime = breakneckSpeedsRemainingTime;
            BreakneckSpeedsCooldown = breakneckSpeedsCooldown;
            TantrumCooldown = tantrumCooldown;
        }

        #region Properties

        public float TotalBlinkCooldown { get; }

        public double BlinkInitialStop { get; }

        public double BlinkEndSustainTime { get; }

        public bool BreakneckSpeedsActive { get; }

        public float BreakneckSpeedsRemainingTime { get; }

        public CooldownInfo BreakneckSpeedsCooldown { get; }

        public CooldownInfo TantrumCooldown { get; }

        #endregion

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            if (!player.IsConnected)
                return;
            base.ApplyTo(player);
            var routines = Scp173SubroutineContainer.Get(player.RoleAs<Scp173Role>());
            if (!routines.IsValid)
                return;

            var time = NetworkTime.time;

            var blink = routines.BlinkTimer;
            blink._totalCooldown = TotalBlinkCooldown;
            blink._initialStopTime = BlinkInitialStop + time;
            blink._endSustainTime = BlinkEndSustainTime + time;

            var breakneck = routines.BreakneckSpeeds;
            breakneck.IsActive = BreakneckSpeedsActive;
            if (BreakneckSpeedsActive && BreakneckSpeedsRemainingTime > 0) {
                breakneck._duration.Restart();
                breakneck._disableTime = BreakneckSpeedsRemainingTime;
            }

            BreakneckSpeedsCooldown.ApplyTo(breakneck.Cooldown);

            var tantrum = routines.Tantrum;
            TantrumCooldown.ApplyTo(tantrum.Cooldown);

            blink.Sync();
            breakneck.Sync();
            tantrum.Sync();
        }

    }

}
