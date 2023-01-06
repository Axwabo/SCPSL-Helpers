using InventorySystem.Items;
using InventorySystem.Items.Usables.Scp1576;

namespace Axwabo.Helpers.PlayerInfo.Item {

    public sealed class Scp1576Info : UsableItemInfo {

        public static Scp1576Info Get(ItemBase item) => item is not Scp1576Item
            ? null
            : new Scp1576Info(GetRemainingCooldown(item, false), item.ItemSerial);

        public static bool Is1576(ItemBase item) => item is Scp1576Item;

        public Scp1576Info(float remainingCooldown, ushort serial) : base(remainingCooldown, ItemType.SCP1576, serial) {
        }

        public override bool IsPersonal => false;

    }

}
