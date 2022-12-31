using Axwabo.Helpers.PlayerInfo.Containers;
using Exiled.API.Features;
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

        public Scp106Info(float vigor, bool isStalking, CooldownInfo sinkholeCooldown, BasicRoleInfo basicRoleInfo) : base(basicRoleInfo) {
            Vigor = vigor;
            IsStalking = isStalking;
            SinkholeCooldown = sinkholeCooldown;
        }

        #region Properties

        public float Vigor { get; }
        public bool IsStalking { get; }
        public CooldownInfo SinkholeCooldown { get; }

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
            routines.StalkAbility.IsActive = IsStalking;

            var sinkhole = routines.SinkholeController;
            SinkholeCooldown.ApplyTo(sinkhole.Cooldown);
            sinkhole.Sync();
        }

    }

}
