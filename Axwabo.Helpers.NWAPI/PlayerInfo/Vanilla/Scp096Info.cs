using System.Linq;
using Axwabo.Helpers.PlayerInfo.Containers;
using PlayerRoles.PlayableScps.Scp096;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo.Vanilla {

    /// <summary>
    /// Contains information about SCP-096.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    /// <seealso cref="PlayerInfoBase"/>
    /// <seealso cref="Scp096Role"/>
    public sealed class Scp096Info : PlayerInfoBase {

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
                routines.RageCycle._activationTime,
                BasicRoleInfo.Get(player)
            );
        }

        /// <summary>
        /// Checks if the given player is SCP-096.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-096.</returns>
        public static bool Is096(Player p) => p.RoleIs<Scp096Role>();

        /// <summary>
        /// Creates a new <see cref="Scp096Info"/> instance.
        /// </summary>
        /// <param name="rageState">The current Rage state of SCP-096.</param>
        /// <param name="rageTimeLeft">The time left until SCP-096 is no longer enraged.</param>
        /// <param name="totalRageTime">The total time SCP-096 is enraged.</param>
        /// <param name="targets">The current targets of SCP-096.</param>
        /// <param name="chargeDuration">The duration of the Charge ability.</param>
        /// <param name="chargeCooldown">The cooldown of the Charge ability.</param>
        /// <param name="rageActivationTime">Time until Rage can be activated.</param>
        /// <param name="roleInfo">Basic information about the player.</param>
        public Scp096Info(Scp096RageState rageState, float rageTimeLeft, float totalRageTime, ReferenceHub[] targets, CooldownInfo chargeDuration, CooldownInfo chargeCooldown, CooldownInfo rageActivationTime, BasicRoleInfo roleInfo) : base(roleInfo) {
            RageState = rageState;
            RageTimeLeft = rageTimeLeft;
            TotalRageTime = totalRageTime;
            Targets = targets;
            ChargeDuration = chargeDuration;
            ChargeCooldown = chargeCooldown;
            RageActivationTime = rageActivationTime;
        }

        #region Properties

        /// <summary>The current Rage state of SCP-096.</summary>
        public Scp096RageState RageState { get; }

        /// <summary>The time left until SCP-096 is no longer enraged.</summary>
        public float RageTimeLeft { get; }

        /// <summary>The total time SCP-096 is enraged.</summary>
        public float TotalRageTime { get; }

        /// <summary>The current targets of SCP-096.</summary>
        public ReferenceHub[] Targets { get; }

        /// <summary>The duration of the Charge ability.</summary>
        public CooldownInfo ChargeDuration { get; }

        /// <summary>The cooldown of the Charge ability.</summary>
        public CooldownInfo ChargeCooldown { get; }

        /// <summary>Time until Rage can be activated.</summary>
        public CooldownInfo RageActivationTime { get; }

        #endregion

        /// <inheritdoc/>
        public override void ApplyTo(Player player) {
            if (!player.IsConnected())
                return;
            base.ApplyTo(player);
            var routines = Scp096SubroutineContainer.Get(player.RoleAs<Scp096Role>());
            if (!routines.IsValid)
                return;

            var rageManager = routines.RageManager;
            rageManager._enragedTimeLeft = RageTimeLeft;
            rageManager.TotalRageTime = TotalRageTime;
            SetHumeShield(rageManager);

            var state = routines.State;
            state._rageState = RageState;

            var targetsTracker = routines.TargetsTracker;
            targetsTracker.Targets.AddRange(Targets);

            var charge = routines.Charge;
            ChargeCooldown.ApplyTo(charge.Cooldown);
            ChargeDuration.ApplyTo(charge.Duration);

            var rageCycle = routines.RageCycle;
            RageActivationTime.ApplyTo(rageCycle._activationTime);

            state.Sync();
            rageManager.Sync();
            targetsTracker.Sync();
            charge.Sync();
            rageCycle.Sync();
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
