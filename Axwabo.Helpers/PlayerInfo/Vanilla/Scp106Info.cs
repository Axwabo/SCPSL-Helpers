using Axwabo.Helpers.PlayerInfo.Containers;
using Exiled.API.Features;
using Mirror;
using PlayerRoles.PlayableScps.Scp106;

namespace Axwabo.Helpers.PlayerInfo.Vanilla {

    /// <summary>
    /// Contains information about SCP-106.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    /// <seealso cref="PlayerInfoBase"/>
    /// <seealso cref="Scp106Role"/>
    public sealed class Scp106Info : PlayerInfoBase {

        /// <summary>
        /// Creates an <see cref="Scp106Info"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>The information about SCP-106.</returns>
        public static Scp106Info Get(Player player) {
            var routines = Scp106SubroutineContainer.Get(player.RoleAs<Scp106Role>());
            if (!routines.IsValid)
                return null;
            return new Scp106Info(
                routines.Attack._nextAttack - NetworkTime.time,
                routines.Vigor._vigor,
                routines.StalkAbility.IsActive,
                routines.SinkholeController.Cooldown,
                BasicRoleInfo.Get(player)
            );
        }

        /// <summary>
        /// Checks if the given player is SCP-106.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-106.</returns>
        public static bool Is106(Player p) => p.RoleIs<Scp106Role>();

        /// <summary>
        /// Creates a new <see cref="Scp106Info"/> instance.
        /// </summary>
        /// <param name="attackCooldown">The remaining attack cooldown.</param>
        /// <param name="vigor">The current vigor amount.</param>
        /// <param name="isStalking">Whether SCP-106 is currently stalking.</param>
        /// <param name="sinkholeCooldown">The remaining sinkhole cooldown.</param>
        /// <param name="basicRoleInfo">Basic information about the player.</param>
        public Scp106Info(double attackCooldown, float vigor, bool isStalking, CooldownInfo sinkholeCooldown, BasicRoleInfo basicRoleInfo) : base(basicRoleInfo) {
            AttackCooldown = attackCooldown;
            Vigor = vigor;
            IsStalking = isStalking;
            SinkholeCooldown = sinkholeCooldown;
        }

        #region Properties

        /// <summary>The remaining attack cooldown.</summary>
        public double AttackCooldown { get; set; }

        /// <summary>The current vigor amount.</summary>
        public float Vigor { get; set; }

        /// <summary>Whether SCP-106 is currently stalking.</summary>
        public bool IsStalking { get; set; }

        /// <summary>The remaining sinkhole cooldown.</summary>
        public CooldownInfo SinkholeCooldown { get; set; }

        #endregion

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            if (!player.IsConnected)
                return;
            base.ApplyTo(player);
            var routines = Scp106SubroutineContainer.Get(player.RoleAs<Scp106Role>());
            if (!routines.IsValid)
                return;
            routines.Vigor._vigor = Vigor;

            var attack = routines.Attack;
            attack._nextAttack = NetworkTime.time + AttackCooldown;

            routines.StalkAbility.IsActive = IsStalking;

            var sinkhole = routines.SinkholeController;
            SinkholeCooldown.ApplyTo(sinkhole.Cooldown);

            attack.Sync();
            sinkhole.Sync();
        }

    }

}
