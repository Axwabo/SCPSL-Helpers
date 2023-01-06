using System.Collections.Generic;
using System.Linq;
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
                inventory.UserInventory.ReserveAmmo.ToDictionary(k => k.Key, v => v.Value),
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
            ushort selected = 0;
            foreach (var info in Items) {
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

}
