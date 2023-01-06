using InventorySystem.Items;
using InventorySystem.Items.Usables;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo.Item {

    public abstract class UsableItemInfo : ItemInfoBase {

        public static float GetRemainingCooldown(ItemBase item, bool isPersonal) {
            if (item == null)
                return 0;
            var time = Time.timeSinceLevelLoad;
            if (!isPersonal)
                return UsableItemsController.GlobalItemCooldowns.TryGetValue(item.ItemSerial, out var cooldown) ? cooldown - time : 0;
            var handler = UsableItemsController.GetHandler(item.Owner);
            return handler == null ? 0 : handler.PersonalCooldowns.TryGetValue(item.ItemTypeId, out var personal) ? personal - time : 0;
        }

        protected UsableItemInfo(float remainingCooldown, ItemType type, ushort serial) : base(type, serial) => RemainingCooldown = remainingCooldown;

        public abstract bool IsPersonal { get; }

        public float RemainingCooldown { get; }

        public override void ApplyTo(ItemBase item) {
            base.ApplyTo(item);
            if (item is not UsableItem usable)
                return;
            if (IsPersonal)
                usable.ServerSetPersonalCooldown(RemainingCooldown);
            else
                usable.ServerSetGlobalItemCooldown(RemainingCooldown);
        }

    }

}
