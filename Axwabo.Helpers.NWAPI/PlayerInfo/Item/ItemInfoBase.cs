using System.Collections.Generic;
using System.Linq;
using InventorySystem;
using InventorySystem.Items;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo.Item {

    /// <summary>
    /// A base class for storing gameplay information about an item.
    /// </summary>
    public class ItemInfoBase {

        private static readonly List<ItemInfoObtainer> CustomObtainers = new();

        private static readonly ItemInfoObtainer[] DefaultObtainers = {
            new(RadioInfo.IsRadio, RadioInfo.Get),
            new(Scp330BagInfo.Is330, Scp330BagInfo.Get),
            new(Scp268Info.Is268, Scp268Info.Get),
            new(Scp1576Info.Is1576, Scp1576Info.Get),
            new(FirearmInfo.IsFirearm, FirearmInfo.Get)
        };

        /// <summary>
        /// Registers a custom item info obtainer.
        /// </summary>
        /// <param name="check">The check to determine if the item matches the given specifications.</param>
        /// <param name="getter">A method to get the item info.</param>
        /// <returns>The id of the registered obtainer.</returns>
        public static uint RegisterCustomObtainer(ItemCheck check, ItemInfoGetter getter) {
            var obtainer = new ItemInfoObtainer(check, getter);
            CustomObtainers.Add(obtainer);
            return obtainer.Id;
        }

        /// <summary>
        /// Unregisters a custom item info obtainer.
        /// </summary>
        /// <param name="id">The id of the obtainer to unregister.</param>
        /// <returns>Whether the obtainer was unregistered.</returns>
        public static bool UnregisterCustomObtainer(uint id) => CustomObtainers.RemoveAll(obtainer => obtainer.Id == id) > 0;

        public static ItemInfoObtainer GetFirstMatchingObtainer(ItemBase item) {
            foreach (var obtainer in CustomObtainers)
                if (obtainer.IsValid && obtainer.Check(item))
                    return obtainer;

            foreach (var obtainer in DefaultObtainers)
                if (obtainer.IsValid && obtainer.Check(item))
                    return obtainer;

            return ItemInfoObtainer.Empty;
        }

        public static ItemInfoBase CreateAutomatically(ItemBase item) {
            var obtainer = GetFirstMatchingObtainer(item);
            return obtainer.IsValid ? obtainer.Get(item) : GetBasicInfo(item);
        }

        public static ItemInfoBase GetBasicInfo(ItemBase item) => item == null ? null : new ItemInfoBase(item.ItemTypeId, item.ItemSerial);

        public static ItemInfoBase[] ItemsToArray(IEnumerable<ItemBase> items) => items.Select(CreateAutomatically).ToArray();

        public ItemInfoBase(ItemType type, ushort serial) {
            Type = type;
            Serial = serial;
        }

        public ItemType Type { get; }

        public ushort Serial { get; }

        public virtual void ApplyTo(ItemBase item) => item.ItemSerial = Serial;

        public virtual ItemBase GiveTo(Player player) {
            var item = player.ReferenceHub.inventory.ServerAddItem(Type, Serial);
            ApplyTo(item);
            return item;
        }

    }

}
