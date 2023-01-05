using System.Linq;
using Axwabo.Helpers.PlayerInfo.Containers;
using Exiled.API.Features;
using PlayerRoles.PlayableScps.Scp096;

namespace Axwabo.Helpers.PlayerInfo.Vanilla {

    public class Scp096Info : PlayerInfoBase {

        /// <summary>
        /// Creates an <see cref="Scp096Info"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>The information about SCP-096.</returns>
        public static Scp096Info Get(Player player) {
            var routines = Scp096SubroutineContainer.Get(player.RoleAs<Scp096Role>());
            if (!routines.IsValid)
                return null;
            var charge = routines.Charge;
            var rageManager = routines.RageManager;
            return new Scp096Info(
                routines.State._rageState,
                rageManager._enragedTimeLeft,
                rageManager.TotalRageTime,
                routines.TargetsTracker.Targets.ToArray(),
                charge.Duration,
                charge.Cooldown,
                BasicRoleInfo.Get(player)
            );
        }

        /// <summary>
        /// Checks if the given player is SCP-096.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-096.</returns>
        public static bool Is096(Player p) => p.RoleIs<Scp096Role>();

        public Scp096Info(Scp096RageState rageState, float rageTime, float totalRageTime, ReferenceHub[] targets, CooldownInfo chargeDuration, CooldownInfo chargeCooldown, BasicRoleInfo roleInfo) : base(roleInfo) {
            RageState = rageState;
            RageTime = rageTime;
            TotalRageTime = totalRageTime;
            Targets = targets;
            ChargeDuration = chargeDuration;
            ChargeCooldown = chargeCooldown;
        }

        #region Properties

        public Scp096RageState RageState { get; }

        public float RageTime { get; }

        public float TotalRageTime { get; }

        public ReferenceHub[] Targets { get; }

        public CooldownInfo ChargeDuration { get; }

        public CooldownInfo ChargeCooldown { get; }

        #endregion

        /// <inheritdoc/>
        public override void ApplyTo(Player player) {
            if (!player.IsConnected)
                return;
            base.ApplyTo(player);
            var routines = Scp096SubroutineContainer.Get(player.RoleAs<Scp096Role>());
            if (!routines.IsValid)
                return;

            var rageManager = routines.RageManager;
            rageManager._enragedTimeLeft = RageTime;
            rageManager.TotalRageTime = TotalRageTime;
            SetHumeShield(rageManager);

            var state = routines.State;
            state._rageState = RageState;

            var targetsTracker = routines.TargetsTracker;
            targetsTracker.Targets.AddRange(Targets);

            var charge = routines.Charge;
            ChargeCooldown.ApplyTo(charge.Cooldown);
            ChargeDuration.ApplyTo(charge.Duration);

            state.Sync();
            rageManager.Sync();
            targetsTracker.Sync();
            charge.Sync();
        }

        private void SetHumeShield(Scp096RageManager rageManager) {
            if (rageManager.ScpRole.StateController._rageState == RageState)
                return;
            if (RageState == Scp096RageState.Enraged) {
                rageManager.HumeShieldBlocked = true;
                rageManager._shieldController.AddBlocker(rageManager);
            } else
                rageManager.HumeShieldBlocked = false;
        }

    }

}
