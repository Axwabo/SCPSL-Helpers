using InventorySystem.Items;
using InventorySystem.Items.Usables;

namespace Axwabo.Helpers.PlayerInfo.Item {

    public sealed class Scp268Info : UsableItemInfo {

        public static Scp268Info Get(ItemBase item) => item is not Scp268
            ? null
            : new Scp268Info(GetRemainingCooldown(item, true), item.ItemSerial);

        public static bool Is268(ItemBase item) => item is Scp268;

        public Scp268Info(float remainingCooldown, ushort serial) : base(remainingCooldown, ItemType.SCP268, serial) {
        }

        public override bool IsPersonal => true;

    }

}
