using Axwabo.Helpers.PlayerInfo.Item;
using Exiled.API.Features;
using InventorySystem.Items;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// Checks a specific information about a player.
    /// </summary>
    /// <param name="player">The player to check.</param>
    public delegate bool PlayerCheck(Player player);

    /// <summary>
    /// Gets the gameplay information about a player.
    /// </summary>
    /// <param name="player">The player to get the information from.</param>
    public delegate PlayerInfoBase PlayerInfoGetter(Player player);

    /// <summary>
    /// Sets the player's custom role.
    /// </summary>
    /// <param name="player">The player to set the role of.</param>
    public delegate void PlayerRoleSetter(Player player);

    public delegate bool ItemCheck(ItemBase item);

    public delegate ItemInfoBase ItemInfoGetter(ItemBase item);

}
