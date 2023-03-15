using InventorySystem.Items;
using InventorySystem.Items.Usables.Scp330;

namespace Axwabo.Helpers.PlayerInfo.Item;

/// <summary>
/// Contains gameplay information about an SCP-330 bag.
/// </summary>
public sealed class Scp330BagInfo : ItemInfoBase
{

    /// <summary>
    /// Gets information about an SCP-330 bag.
    /// </summary>
    /// <param name="item">The item to get the information from.</param>
    /// <returns>The information about the SCP-330 bag.</returns>
    public static Scp330BagInfo Get(ItemBase item) => item is not Scp330Bag bag
        ? null
        : new Scp330BagInfo(bag.Candies.ToArray(), item.ItemSerial);

    /// <summary>
    /// Checks if the given item is an SCP-330 bag.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns>Whether the item is an SCP-330 bag.</returns>
    public static bool Is330(ItemBase item) => item is Scp330Bag;

    /// <summary>
    /// Creates a new <see cref="Scp330BagInfo"/> instance.
    /// </summary>
    /// <param name="candies">The candies in the bag.</param>
    /// <param name="serial">The serial of the item.</param>
    public Scp330BagInfo(CandyKindID[] candies, ushort serial) : base(ItemType.SCP330, serial) => Candies = candies;

    /// <summary>The candies in the bag.</summary>
    public CandyKindID[] Candies { get; set; }

    /// <inheritdoc />
    public override void ApplyTo(ItemBase item)
    {
        base.ApplyTo(item);
        if (item is not Scp330Bag bag || Candies == null)
            return;
        bag.Candies.Clear();
        for (var i = 0; i < Candies.Length; i++)
        {
            if (i >= Scp330Bag.MaxCandies)
                break;
            var kind = Candies[i];
            if (kind != CandyKindID.None)
                bag.Candies.Add(kind);
        }

        bag.ServerRefreshBag();
    }

}
