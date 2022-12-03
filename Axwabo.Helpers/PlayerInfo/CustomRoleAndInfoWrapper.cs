using Exiled.API.Features;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// Wraps a <see cref="PlayerInfoBase"/> object and a <see cref="PlayerRoleSetter"/> into a single <see langword="struct"/>.
    /// </summary>
    public readonly struct CustomRoleAndInfoWrapper : IPlayerInfoWithRole {

        /// <summary>
        /// An empty info object.
        /// </summary>
        public static readonly CustomRoleAndInfoWrapper Empty = new();

        /// <summary>
        /// Creates a new <see cref="CustomRoleAndInfoWrapper"/> instance from the player using the given <paramref name="infoGetter"/>.
        /// </summary>
        /// <param name="player">The player to get the info from.</param>
        /// <param name="infoGetter">A function that returns the info object.</param>
        /// <param name="roleSetter">A method to set the player's role.</param>
        /// <returns>A new <see cref="CustomRoleAndInfoWrapper"/> instance.</returns>
        public static CustomRoleAndInfoWrapper Get(Player player, PlayerInfoGetter infoGetter, PlayerRoleSetter roleSetter) =>
            player is null ? Empty : new CustomRoleAndInfoWrapper(roleSetter, infoGetter(player));

        public static CustomRoleAndInfoWrapper Get(Player player, PlayerInfoObtainer obtainer) => Get(player, obtainer.Get, obtainer.SetRole);

        /// <summary>
        /// The role of the player.
        /// </summary>
        public readonly PlayerRoleSetter SetRole;

        private readonly PlayerInfoBase _info;

        /// <inheritdoc/>
        public PlayerInfoBase Info => _info;

        /// <summary>
        /// Creates a new <see cref="CustomRoleAndInfoWrapper"/> instance.
        /// </summary>
        /// <param name="role">The role of the player.</param>
        /// <param name="info">Gameplay information about the player.</param>
        public CustomRoleAndInfoWrapper(PlayerRoleSetter role, PlayerInfoBase info) {
            SetRole = role;
            _info = info;
        }

        /// <inheritdoc/>
        public void SetClass(Player player) => SetRole(player);

        /// <inheritdoc/>
        public void ApplyInfo(Player player) => Info?.ApplyTo(player);

        /// <inheritdoc/>
        public void SetClassAndApplyInfo(Player player) {
            SetClass(player);
            ApplyInfo(player);
        }

    }

}
