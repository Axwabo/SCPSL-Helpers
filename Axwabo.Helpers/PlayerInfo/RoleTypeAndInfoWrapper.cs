using Exiled.API.Features;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// Wraps a <see cref="PlayerInfoBase"/> object and a <see cref="RoleType"/> into a single <see langword="struct"/>.
    /// </summary>
    public readonly struct RoleTypeAndInfoWrapper : IPlayerInfoWithRole {

        /// <summary>
        /// An empty info object.
        /// </summary>
        public static readonly RoleTypeAndInfoWrapper Empty = new();

        /// <summary>
        /// Creates a new <see cref="RoleTypeAndInfoWrapper"/> instance from the player using the given <paramref name="infoGetter"/>.
        /// </summary>
        /// <param name="player">The player to get the info from.</param>
        /// <param name="infoGetter">A function that returns the info object.</param>
        /// <returns>A new <see cref="RoleTypeAndInfoWrapper"/> instance.</returns>
        public static RoleTypeAndInfoWrapper Get(Player player, PlayerInfoGetter infoGetter) =>
            player is null ? Empty : new RoleTypeAndInfoWrapper(player.Ccm().NetworkCurClass, infoGetter(player));

        /// <summary>
        /// The role of the player.
        /// </summary>
        public readonly RoleType Role;

        private readonly PlayerInfoBase _info;

        /// <inheritdoc/>
        public PlayerInfoBase Info => _info;

        /// <summary>
        /// Creates a new <see cref="RoleTypeAndInfoWrapper"/> instance.
        /// </summary>
        /// <param name="role">The role of the player.</param>
        /// <param name="info">Gameplay information about the player.</param>
        public RoleTypeAndInfoWrapper(RoleType role, PlayerInfoBase info) {
            Role = role;
            _info = info;
        }

        /// <inheritdoc/>
        public void SetClass(Player player) => SetClass(player.Ccm());

        /// <summary>
        /// Sets the class of a player using its <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="ccm">The <see cref="CharacterClassManager"/> of the player to set the class of.</param>
        public void SetClass(CharacterClassManager ccm) {
            if (Role >= RoleType.Scp173)
                ccm.SetPlayersClass(Role, ccm.gameObject, CharacterClassManager.SpawnReason.ForceClass);
        }

        /// <inheritdoc/>
        public void ApplyInfo(Player player) => Info?.ApplyTo(player);

        /// <inheritdoc/>
        public void SetClassAndApplyInfo(Player player) {
            SetClass(player);
            ApplyInfo(player);
        }

    }

}
