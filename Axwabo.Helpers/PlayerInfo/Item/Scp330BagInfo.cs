using InventorySystem.Items;
using InventorySystem.Items.Usables.Scp330;

namespace Axwabo.Helpers.PlayerInfo.Item {

    public sealed class Scp330BagInfo : ItemInfoBase {

        public static Scp330BagInfo Get(ItemBase item) => item is not Scp330Bag bag
            ? null
            : new Scp330BagInfo(bag.Candies.ToArray(), item.ItemSerial);

        public static bool Is330(ItemBase item) => item is Scp330Bag;

        public Scp330BagInfo(CandyKindID[] candies, ushort serial) : base(ItemType.SCP330, serial) => Candies = candies;

        public CandyKindID[] Candies { get; }

        public override void ApplyTo(ItemBase item) {
            base.ApplyTo(item);
            if (item is not Scp330Bag bag || Candies == null)
                return;
            bag.Candies.Clear();
            for (var i = 0; i < Candies.Length; i++) {
                if (i >= Scp330Bag.MaxCandies)
                    break;
                var kind = Candies[i];
                if (kind != CandyKindID.None)
                    bag.Candies.Add(kind);
            }
            
            bag.ServerRefreshBag();
        }

    }

}
