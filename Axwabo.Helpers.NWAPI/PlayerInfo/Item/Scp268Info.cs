using InventorySystem.Items;
using InventorySystem.Items.Usables;

namespace Axwabo.Helpers.PlayerInfo.Item;

/// <summary>
/// Contains gameplay information about an SCP-268 instance.
/// </summary>
public sealed class Scp268Info : UsableItemInfo
{

    /// <summary>
    /// Gets information about SCP-268.
    /// </summary>
    /// <param name="item">The item to get the information from.</param>
    /// <returns>The information about SCP-268.</returns>
    public static Scp268Info Get(ItemBase item) => item is not Scp268
        ? null
        : new Scp268Info(GetRemainingCooldown(item, true), item.ItemSerial);

    /// <summary>
    /// Checks if the given item is SCP-268.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns>Whether the item is SCP-268.</returns>
    public static bool Is268(ItemBase item) => item is Scp268;

    /// <summary>
    /// Creates a new <see cref="Scp268Info"/> instance.
    /// </summary>
    /// <param name="remainingCooldown">The remaining cooldown of the item.</param>
    /// <param name="serial">The serial of the item.</param>
    public Scp268Info(float remainingCooldown, ushort serial) : base(remainingCooldown, ItemType.SCP268, serial)
    {
    }

    /// <inheritdoc />
    public override bool IsPersonal => true;

}
