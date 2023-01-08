using InventorySystem.Items;
using InventorySystem.Items.Firearms;

namespace Axwabo.Helpers.PlayerInfo.Item {

    public sealed class FirearmInfo : ItemInfoBase {

        public static FirearmInfo Get(ItemBase item) => item is not Firearm f
            ? null
            : new FirearmInfo(f.Status, item.ItemTypeId, item.ItemSerial);


        public static bool IsFirearm(ItemBase item) => item is Firearm;

        public FirearmInfo(FirearmStatus status, ItemType type, ushort serial) : base(type, serial) => Status = status;

        public FirearmStatus Status { get; }

        public override void ApplyTo(ItemBase item) {
            base.ApplyTo(item);
            if (item is Firearm firearm)
                firearm.Status = Status;
        }

    }

}
