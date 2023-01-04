using System.Linq;
using Axwabo.Helpers.PlayerInfo.Containers;
using PlayerRoles.PlayableScps.Scp049;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo.Vanilla {

    /// <summary>
    /// Contains information about SCP-049.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    /// <seealso cref="PlayerInfoBase"/>
    /// <seealso cref="Scp049Role"/>
    public sealed class Scp049Info : PlayerInfoBase {

        /// <summary>
        /// Creates an <see cref="Scp049Info"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>The information about SCP-049.</returns>
        public static Scp049Info Get(Player player) {
            var routines = Scp049SubroutineContainer.Get(player.RoleAs<Scp049Role>());
            if (!routines.IsValid)
                return null;
            var call = routines.CallAbility;
            var sense = routines.SenseAbility;
            return new Scp049Info(
                call.Cooldown,
                call.Duration,
                sense.Cooldown,
                sense.Duration,
                sense.Target,
                sense.DeadTargets.ToArray(),
                routines.AttackAbility.Cooldown,
                BasicRoleInfo.Get(player)
            );
        }

        /// <summary>
        /// Checks if the given player is SCP-049.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-049.</returns>
        public static bool Is049(Player p) => p.RoleIs<Scp049Role>();

        public Scp049Info(CooldownInfo callCooldown,
            CooldownInfo callDuration,
            CooldownInfo senseCooldown,
            CooldownInfo senseDuration,
            ReferenceHub target,
            ReferenceHub[] deadTargets,
            CooldownInfo attackCooldown,
            BasicRoleInfo basicRoleInfo) : base(basicRoleInfo) {
            CallCooldown = callCooldown;
            CallDuration = callDuration;
            SenseCooldown = senseCooldown;
            SenseDuration = senseDuration;
            Target = target;
            DeadTargets = deadTargets;
            AttackCooldown = attackCooldown;
        }

        #region Properties

        public CooldownInfo CallCooldown { get; }

        public CooldownInfo CallDuration { get; }

        public CooldownInfo SenseCooldown { get; }

        public CooldownInfo SenseDuration { get; }

        public ReferenceHub Target { get; }

        public ReferenceHub[] DeadTargets { get; }

        public CooldownInfo AttackCooldown { get; }

        #endregion

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            if (!player.IsConnected())
                return;
            base.ApplyTo(player);
            var routines = Scp049SubroutineContainer.Get(player.RoleAs<Scp049Role>());
            if (!routines.IsValid)
                return;
            var call = routines.CallAbility;
            CallCooldown.ApplyTo(call.Cooldown);
            CallDuration.ApplyTo(call.Duration);
            call._serverTriggered = false;

            var sense = routines.SenseAbility;
            SenseCooldown.ApplyTo(sense.Cooldown);
            SenseDuration.ApplyTo(sense.Duration);

            var hasTarget = Target != null;
            sense.HasTarget = hasTarget;
            if (hasTarget)
                sense.Target = Target;

            if (DeadTargets != null) {
                sense.DeadTargets.Clear();
                foreach (var target in DeadTargets)
                    sense.DeadTargets.Add(target);
            }

            var attack = routines.AttackAbility;
            AttackCooldown.ApplyTo(attack.Cooldown);

            call.Sync();
            sense.Sync();
            attack.Sync();
        }

    }

}
