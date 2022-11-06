using System;
using Exiled.API.Features;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// Wraps a <see cref="PlayerInfoBase"/> object and a <see cref="RoleType"/> into a single <see langword="struct"/>.
    /// </summary>
    public readonly struct PlayerRoleAndInfoWrapper {

        /// <summary>
        /// An empty info object.
        /// </summary>
        public static readonly PlayerRoleAndInfoWrapper Empty = new();

        /// <summary>
        /// Creates a new <see cref="PlayerRoleAndInfoWrapper"/> instance from the player using the given <paramref name="infoGetter"/>.
        /// </summary>
        /// <param name="player">The player to get the info from.</param>
        /// <param name="infoGetter">A function that returns the info object.</param>
        /// <returns>A new <see cref="PlayerRoleAndInfoWrapper"/> instance.</returns>
        public static PlayerRoleAndInfoWrapper Get(Player player, Func<Player, PlayerInfoBase> infoGetter) =>
            player is null ? Empty : new PlayerRoleAndInfoWrapper(player.Ccm().NetworkCurClass, infoGetter(player));

        /// <summary>
        /// The role of the player.
        /// </summary>
        public readonly RoleType Role;

        /// <summary>
        /// Gameplay information about the player.
        /// </summary>
        public readonly PlayerInfoBase Info;

        /// <summary>
        /// Creates a new <see cref="PlayerRoleAndInfoWrapper"/> instance.
        /// </summary>
        /// <param name="role">The role of the player.</param>
        /// <param name="info">Gameplay information about the player.</param>
        public PlayerRoleAndInfoWrapper(RoleType role, PlayerInfoBase info) {
            Role = role;
            Info = info;
        }

        /// <summary>
        /// Sets the class of the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to set the class of.</param>
        public void SetClass(Player player) => SetClass(player.Ccm());

        /// <summary>
        /// Sets the class of a player using its <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="ccm">The <see cref="CharacterClassManager"/> of the player to set the class of.</param>
        public void SetClass(CharacterClassManager ccm) {
            if (Role >= RoleType.Scp173)
                ccm.SetPlayersClass(Role, ccm.gameObject, CharacterClassManager.SpawnReason.ForceClass);
        }

        /// <summary>
        /// Applies the information to the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to apply the information to.</param>
        public void ApplyInfo(Player player) => Info?.ApplyTo(player);

        /// <summary>
        /// Sets the class and applies the information to the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to set the class and apply the information to.</param>
        public void SetClassAndApplyInfo(Player player) {
            SetClass(player);
            ApplyInfo(player);
        }

    }

}
