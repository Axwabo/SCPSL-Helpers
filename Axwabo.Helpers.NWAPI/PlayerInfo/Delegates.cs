using Axwabo.Helpers.PlayerInfo.Item;
using InventorySystem.Items;
using PluginAPI.Core;

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

    /// <summary>
    /// Checks a specific information about an item.
    /// </summary>
    /// <param name="item">The item to check.</param>
    public delegate bool ItemCheck(ItemBase item);

    /// <summary>
    /// Gets the gameplay information about an item.
    /// </summary>
    /// <param name="item">The item to get the information from.</param>
    public delegate ItemInfoBase ItemInfoGetter(ItemBase item);

    /// <summary>
    /// A method to give an item to a player.
    /// </summary>
    /// <param name="player">The player to give the item to.</param>
    public delegate ItemBase GiveItem(Player player, ushort serial);

}
