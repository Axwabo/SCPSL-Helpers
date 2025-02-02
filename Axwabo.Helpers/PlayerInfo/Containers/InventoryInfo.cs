using System;
using System.Collections.Generic;
using Axwabo.Helpers.PlayerInfo.Item;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo.Containers;

/// <summary>
/// Contains information about the items and ammo in a player's inventory.
/// </summary>
public readonly struct InventoryInfo
{

    /// <summary>An empty object representing no items.</summary>
    public static readonly InventoryInfo Empty = new();

    /// <summary>
    /// Gets the inventory information about the specified player.
    /// </summary>
    /// <param name="p">The player to get the information from.</param>
    /// <returns>An <see cref="InventoryInfo"/> object.</returns>
    public static InventoryInfo Get(Player p)
    {
        var inventory = p.ReferenceHub.inventory;
        return new InventoryInfo(
            ItemInfoBase.ItemsToArray(inventory.UserInventory.Items.Values),
            new Dictionary<ItemType, ushort>(inventory.UserInventory.ReserveAmmo),
            inventory.CurItem.SerialNumber
        );
    }

    /// <summary>
    /// Creates a new <see cref="InventoryInfo"/> instance.
    /// </summary>
    /// <param name="items">The items in the player's inventory.</param>
    /// <param name="ammo">The ammo in the player's inventory.</param>
    /// <param name="currentItem">The serial of the item the player is currently holding.</param>
    public InventoryInfo(ItemInfoBase[] items, Dictionary<ItemType, ushort> ammo, ushort currentItem)
    {
        Items = items;
        Ammo = ammo;
        CurrentItem = currentItem;
        IsValid = true;
    }

    /// <summary>The items in the player's inventory.</summary>
    public readonly ItemInfoBase[] Items;

    /// <summary>The ammo in the player's inventory.</summary>
    public readonly Dictionary<ItemType, ushort> Ammo;

    /// <summary>The serial of the item the player is currently holding.</summary>
    public readonly ushort CurrentItem;

    /// <summary>True if this instance is valid (not empty).</summary>
    public readonly bool IsValid;

    /// <summary>
    /// Clears the player's inventory and applies the information.
    /// </summary>
    /// <param name="player">The player to apply the information to.</param>
    public void ApplyTo(Player player)
    {
        if (!IsValid || !player.IsConnected())
            return;
        var inv = player.ReferenceHub.inventory;
        inv.RemoveAllItems();
        ushort selected = 0;
        foreach (var info in Items ?? Array.Empty<ItemInfoBase>())
        {
            if (info == null)
                continue;
            var item = info.GiveTo(player);
            if (item.ItemSerial == CurrentItem)
                selected = CurrentItem;
        }

        var reserve = inv.UserInventory.ReserveAmmo;
        foreach (var pair in Ammo)
            reserve[pair.Key] = pair.Value;

        inv.ServerSelectItem(selected);
        inv.SendItemsNextFrame = true;
        inv.SendAmmoNextFrame = true;
    }

}
