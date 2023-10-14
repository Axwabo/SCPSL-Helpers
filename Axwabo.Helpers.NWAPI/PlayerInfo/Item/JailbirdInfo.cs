using InventorySystem.Items;
using InventorySystem.Items.Jailbird;

namespace Axwabo.Helpers.PlayerInfo.Item;

/// <summary>
/// Contains gameplay information about a Jailbird.
/// </summary>
public class JailbirdInfo : ItemInfoBase
{

    /// <summary>
    /// Gets information about a Jailbird.
    /// </summary>
    /// <param name="item">The item to get the information from.</param>
    /// <returns>The information about the Jailbird.</returns>
    public static JailbirdInfo Get(ItemBase item) => item is not JailbirdItem jb
        ? null
        : new JailbirdInfo(jb._hitreg.TotalMeleeDamageDealt, jb.TotalChargesPerformed, item.ItemSerial);

    /// <summary>
    /// Checks if the given item is a Jailbird.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns>Whether the item is a Jailbird.</returns>
    public static bool IsJailbird(ItemBase item) => item is JailbirdItem;

    /// <summary>
    /// Creates a new <see cref="FirearmInfo"/> instance.
    /// </summary>
    /// <param name="meleeDamage">The amount of total melee damage dealt-</param>
    /// <param name="charges">The amount of charges used.</param>
    /// <param name="serial">The serial of the item.</param>
    public JailbirdInfo(float meleeDamage, int charges, ushort serial) : base(ItemType.Jailbird, serial)
    {
        MeleeDamage = meleeDamage;
        Charges = charges;
    }

    /// <summary>The amount of total melee damage dealt.</summary>
    public float MeleeDamage { get; }

    /// <summary>The amount of charges used.</summary>
    public int Charges { get; }

    /// <inheritdoc />
    public override void ApplyTo(ItemBase item)
    {
        base.ApplyTo(item);
        if (item is not JailbirdItem jb)
            return;
        jb._hitreg.TotalMeleeDamageDealt = MeleeDamage;
        jb.TotalChargesPerformed = Charges;
        jb._deterioration.RecheckUsage();
    }

}
