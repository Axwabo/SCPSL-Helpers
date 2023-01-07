using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// A base interface for player info and role structs.
    /// </summary>
    /// <seealso cref="CustomRoleAndInfoWrapper"/>
    /// <seealso cref="RoleTypeAndInfoWrapper"/>
    public interface IPlayerInfoWithRole {

        /// <summary>Gameplay information about the player.</summary>
        PlayerInfoBase Info { get; }

        /// <summary>
        /// Sets the class of the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to set the class of.</param>
        void SetClass(Player player);

        /// <summary>
        /// Applies the information to the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to apply the information to.</param>
        void ApplyInfo(Player player);

        /// <summary>
        /// Sets the class and applies the information to the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to set the class and apply the information to.</param>
        void SetClassAndApplyInfo(Player player);

    }

}
