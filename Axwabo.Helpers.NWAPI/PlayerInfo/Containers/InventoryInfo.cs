using System.Collections.Generic;
using Axwabo.Helpers.PlayerInfo.Item;
using InventorySystem.Items;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct InventoryInfo : IIsValid {

        public static readonly InventoryInfo Empty = new();

        public static InventoryInfo Get(Player p) {
            var inventory = p.ReferenceHub.inventory;
            return new InventoryInfo(
                ItemInfoBase.ItemsToArray(p.Items),
                inventory.UserInventory.ReserveAmmo,
                inventory.CurItem.SerialNumber
            );
        }

        public readonly ItemInfoBase[] Items;

        public readonly Dictionary<ItemType, ushort> Ammo;

        public readonly ushort CurrentItem;

        /// <inheritdoc />
        public bool IsValid { get; }

        public InventoryInfo(ItemInfoBase[] items, Dictionary<ItemType, ushort> ammo, ushort currentItem) {
            Items = items;
            Ammo = ammo;
            CurrentItem = currentItem;
            IsValid = true;
        }

        public void ApplyTo(Player player) {
            if (!IsValid)
                return;
            var inv = player.ReferenceHub.inventory;
            inv.UserInventory.Items.Clear();
            ItemBase selected = null;
            foreach (var info in Items) {
                var item = info.GiveTo(player);
                if (item.ItemSerial == CurrentItem)
                    selected = item;
            }

            inv.UserInventory.ReserveAmmo = Ammo;
            if (selected != null)
                inv.CurInstance = selected;
        }

    }

}
