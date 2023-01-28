using InventorySystem.Items;
using InventorySystem.Items.Firearms;

namespace Axwabo.Helpers.PlayerInfo.Item {

    /// <summary>
    /// Contains gameplay information about a firearm.
    /// </summary>
    public sealed class FirearmInfo : ItemInfoBase {

        /// <summary>
        /// Gets information about a firearm.
        /// </summary>
        /// <param name="item">The item to get the information from.</param>
        /// <returns>The information about the firearm.</returns>
        public static FirearmInfo Get(ItemBase item) => item is not Firearm f
            ? null
            : new FirearmInfo(f.Status, item.ItemTypeId, item.ItemSerial);

        /// <summary>
        /// Checks if the given item is a firearm.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>Whether the item is a firearm.</returns>
        public static bool IsFirearm(ItemBase item) => item is Firearm;

        /// <summary>
        /// Creates a new <see cref="FirearmInfo"/> instance.
        /// </summary>
        /// <param name="status">The status of the firearm.</param>
        /// <param name="type">The type of the item.</param>
        /// <param name="serial">The serial of the item.</param>
        public FirearmInfo(FirearmStatus status, ItemType type, ushort serial) : base(type, serial) => Status = status;

        /// <summary>The status of the firearm.</summary>
        public FirearmStatus Status { get; }

        /// <inheritdoc />
        public override void ApplyTo(ItemBase item) {
            base.ApplyTo(item);
            if (item is Firearm firearm)
                firearm.Status = Status;
        }

    }

}
