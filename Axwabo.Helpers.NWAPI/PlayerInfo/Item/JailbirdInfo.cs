using InventorySystem.Items;
using InventorySystem.Items.Jailbird;

namespace Axwabo.Helpers.PlayerInfo.Item {

    public sealed class JailbirdInfo : ItemInfoBase {

        public static JailbirdInfo Get(ItemBase item) => item is not JailbirdItem jb
            ? null
            : new JailbirdInfo(jb._hitreg.TotalMeleeDamageDealt, jb.TotalChargesPerformed, item.ItemSerial);

        public static bool IsJailbird(ItemBase item) => item is JailbirdItem;

        public JailbirdInfo(float meleeDamage, int charges, ushort serial) : base(ItemType.Jailbird, serial) {
            MeleeDamage = meleeDamage;
            Charges = charges;
        }

        public float MeleeDamage { get; }

        public int Charges { get; }

        /// <inheritdoc />
        public override void ApplyTo(ItemBase item) {
            base.ApplyTo(item);
            if (item is not JailbirdItem jb)
                return;
            jb._hitreg.TotalMeleeDamageDealt = MeleeDamage;
            jb.TotalChargesPerformed = Charges;
            jb.ServerRecheckUsage();
        }

    }

}
