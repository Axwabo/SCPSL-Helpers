using InventorySystem.Items;
using InventorySystem.Items.Usables;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo.Item {

    /// <summary>
    /// Contains gameplay information about an usable item with a cooldown.
    /// </summary>
    public abstract class UsableItemInfo : ItemInfoBase {

        /// <summary>
        /// Gets the remaining cooldown for the given <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The item to get the cooldown from.</param>
        /// <param name="isPersonal">If the cooldown is personal (based on the owner) or global (owner-independent).</param>
        /// <returns></returns>
        public static float GetRemainingCooldown(ItemBase item, bool isPersonal) {
            if (item == null)
                return 0;
            var time = Time.timeSinceLevelLoad;
            if (!isPersonal)
                return UsableItemsController.GlobalItemCooldowns.TryGetValue(item.ItemSerial, out var cooldown) ? cooldown - time : -1f;
            var handler = UsableItemsController.GetHandler(item.Owner);
            return handler == null ? -1f : handler.PersonalCooldowns.TryGetValue(item.ItemTypeId, out var personal) ? personal - time : -1f;
        }

        /// <summary>
        /// Creates a new <see cref="UsableItemInfo"/> instance.
        /// </summary>
        /// <param name="remainingCooldown">The remaining cooldown.</param>
        /// <param name="type">The type of the item.</param>
        /// <param name="serial">The serial of the item.</param>
        protected UsableItemInfo(float remainingCooldown, ItemType type, ushort serial) : base(type, serial) => RemainingCooldown = remainingCooldown;

        /// <summary>Gets whether this item has a personal cooldown.</summary>
        public abstract bool IsPersonal { get; }

        /// <summary>The remaining cooldown.</summary>
        public float RemainingCooldown { get; }

        /// <inheritdoc />
        public override void ApplyTo(ItemBase item) {
            base.ApplyTo(item);
            if (RemainingCooldown < 0 || item is not UsableItem usable)
                return;
            if (IsPersonal)
                usable.ServerSetPersonalCooldown(RemainingCooldown);
            else
                usable.ServerSetGlobalItemCooldown(RemainingCooldown);
        }

    }

}
