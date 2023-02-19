using InventorySystem.Items;
using InventorySystem.Items.Radio;

namespace Axwabo.Helpers.PlayerInfo.Item {

    /// <summary>
    /// Contains gameplay information about a radio.
    /// </summary>
    public sealed class RadioInfo : ItemInfoBase {

        /// <summary>
        /// Gets information about a radio.
        /// </summary>
        /// <param name="item">The item to get the information from.</param>
        /// <returns>The information about the radio.</returns>
        public static RadioInfo Get(ItemBase item) => item is not RadioItem radio
            ? null
            : new RadioInfo(radio._enabled, radio._battery, radio._rangeId, item.ItemSerial);

        /// <summary>
        /// Checks if the given item is a radio.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns>Whether the item is a radio.</returns>
        public static bool IsRadio(ItemBase item) => item is RadioItem;

        /// <summary>
        /// Creates a new <see cref="RadioInfo"/> instance.
        /// </summary>
        /// <param name="enabled">Whether the radio is turned on.</param>
        /// <param name="battery">The battery level.</param>
        /// <param name="range">The range setting of the radio.</param>
        /// <param name="serial">The serial of the item.</param>
        public RadioInfo(bool enabled, float battery, byte range, ushort serial) : base(ItemType.Radio, serial) {
            Enabled = enabled;
            Battery = battery;
            Range = range;
        }

        /// <summary>Whether the radio is turned on.</summary>
        public bool Enabled { get; set; }

        /// <summary>The battery level.</summary>
        public float Battery { get; set; }

        /// <summary>The range setting of the radio.</summary>
        public byte Range { get; set; }

        /// <inheritdoc />
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
