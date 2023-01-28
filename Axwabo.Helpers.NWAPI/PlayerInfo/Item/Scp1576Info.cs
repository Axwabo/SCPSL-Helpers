using InventorySystem.Items;
using InventorySystem.Items.Usables.Scp1576;

namespace Axwabo.Helpers.PlayerInfo.Item {

    /// <summary>
    /// Contains gameplay information about an SCP-1576 instance.
    /// </summary>
    public sealed class Scp1576Info : UsableItemInfo {

        /// <summary>
        /// Gets information about an SCP-1576 instance.
        /// </summary>
        /// <param name="item">The item to get the information from.</param>
        /// <returns>The information about the SCP-1576 instance.</returns>
        public static Scp1576Info Get(ItemBase item) => item is not Scp1576Item
            ? null
            : new Scp1576Info(GetRemainingCooldown(item, false), item.ItemSerial);

        /// <summary>
        /// Checks if the given item is an SCP-1576 instance.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>Whether the item is an SCP-1576 instance.</returns>
        public static bool Is1576(ItemBase item) => item is Scp1576Item;

        /// <summary>
        /// Creates a new <see cref="Scp1576Info"/> instance.
        /// </summary>
        /// <param name="remainingCooldown">The remaining cooldown of the item.</param>
        /// <param name="serial">The serial of the item.</param>
        public Scp1576Info(float remainingCooldown, ushort serial) : base(remainingCooldown, ItemType.SCP1576, serial) {
        }

        /// <inheritdoc />
        public override bool IsPersonal => false;

    }

}
