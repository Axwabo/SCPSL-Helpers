using InventorySystem.Items;
using InventorySystem.Items.Radio;

namespace Axwabo.Helpers.PlayerInfo.Item {

    public sealed class RadioInfo : ItemInfoBase {

        public static RadioInfo Get(ItemBase item) => item is not RadioItem radio
            ? null
            : new RadioInfo(radio._enabled, radio._battery, radio._rangeId, item.ItemSerial);

        public static bool IsRadio(ItemBase item) => item is RadioItem;

        public RadioInfo(bool enabled, float battery, byte range, ushort serial) : base(ItemType.Radio, serial) {
            Enabled = enabled;
            Battery = battery;
            Range = range;
        }

        public bool Enabled { get; }

        public float Battery { get; }

        public byte Range { get; }

        public override void ApplyTo(ItemBase item) {
            base.ApplyTo(item);
            if (item is not RadioItem radio)
                return;
            radio._enabled = Enabled;
            radio._battery = Battery;
            radio._rangeId = Range;
        }

    }

}
