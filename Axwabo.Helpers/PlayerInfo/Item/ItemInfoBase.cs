using InventorySystem;

namespace Axwabo.Helpers.PlayerInfo.Item;

/// <summary>
/// A base class for storing gameplay information about an item.
/// </summary>
public class ItemInfoBase
{

    private static readonly List<ItemInfoObtainer> CustomObtainers = new();

    private static readonly ItemInfoObtainer[] DefaultObtainers =
    {
        new(JailbirdInfo.IsJailbird, JailbirdInfo.Get),
        new(RadioInfo.IsRadio, RadioInfo.Get),
        new(Scp330BagInfo.Is330, Scp330BagInfo.Get),
        new(Scp268Info.Is268, Scp268Info.Get),
        new(Scp1576Info.Is1576, Scp1576Info.Get),
        new(FirearmInfo.IsFirearm, FirearmInfo.Get)
    };

    /// <summary>
    /// Registers a custom item info obtainer.
    /// </summary>
    /// <param name="check">The check to determine if the item matches the given specifications.</param>
    /// <param name="getter">A method to get the item info.</param>
    /// <param name="give">A method to give the item to a player.</param>
    /// <returns>The id of the registered obtainer.</returns>
    public static uint RegisterCustomObtainer(ItemCheck check, ItemInfoGetter getter, GiveItem give = null)
    {
        var obtainer = new ItemInfoObtainer(check, getter, give);
        CustomObtainers.Add(obtainer);
        return obtainer.Id;
    }

    /// <summary>
    /// Unregisters a custom item info obtainer.
    /// </summary>
    /// <param name="id">The id of the obtainer to unregister.</param>
    /// <returns>Whether the obtainer was unregistered.</returns>
    public static bool UnregisterCustomObtainer(uint id) => CustomObtainers.RemoveAll(obtainer => obtainer.Id == id) > 0;

    /// <summary>
    /// Gets the first matching info obtainer for the given <paramref name="item"/>.
    /// </summary>
    /// <param name="item">The item to get the obtainer from.</param>
    /// <returns>The first matching obtainer, or <see cref="ItemInfoObtainer.Empty"/> if none were found.</returns>
    public static ItemInfoObtainer GetFirstMatchingObtainer(ItemBase item)
    {
        foreach (var obtainer in CustomObtainers)
            if (obtainer.IsValid && obtainer.Check(item))
                return obtainer;

        foreach (var obtainer in DefaultObtainers)
            if (obtainer.IsValid && obtainer.Check(item))
                return obtainer;

        return ItemInfoObtainer.Empty;
    }

    /// <summary>
    /// Creates an <see cref="ItemInfoBase"/> from a item based on the registered obtainers.
    /// </summary>
    /// <param name="item">The item to obtain the info from.</param>
    /// <returns>The item info. If no special obtainers were found, it will be an <see cref="ItemInfoBase"/>.</returns>
    /// <seealso cref="RegisterCustomObtainer"/>
    /// <seealso cref="UnregisterCustomObtainer"/>
    /// <seealso cref="GetFirstMatchingObtainer"/>
    public static ItemInfoBase CreateAutomatically(ItemBase item)
    {
        var obtainer = GetFirstMatchingObtainer(item);
        return obtainer.IsValid ? obtainer.Get(item) : GetBasicInfo(item);
    }

    /// <summary>
    /// Gets the basic item info from a item.
    /// </summary>
    /// <param name="item">The item to get the info from.</param>
    /// <returns>A simple <see cref="ItemInfoBase"/> instance.</returns>
    public static ItemInfoBase GetBasicInfo(ItemBase item) => item == null ? null : new ItemInfoBase(item.ItemTypeId, item.ItemSerial);

    /// <summary>
    /// Converts a collection of <see cref="ItemBase"/> objects to a corresponding <see cref="ItemInfoBase"/> array.
    /// </summary>
    /// <param name="items">The items to convert.</param>
    /// <returns>An array of item info objects.</returns>
    public static ItemInfoBase[] ItemsToArray(IEnumerable<ItemBase> items) => items.Select(CreateAutomatically).ToArray();

    /// <summary>
    /// Creates a new <see cref="ItemInfoBase"/> instance.
    /// </summary>
    /// <param name="type">The type of the item.</param>
    /// <param name="serial">The serial of the item.</param>
    public ItemInfoBase(ItemType type, ushort serial)
    {
        Type = type;
        Serial = serial;
    }

    /// <summary>The type of the item.</summary>
    public ItemType Type { get; }

    /// <summary>The serial number of the item.</summary>
    public ushort Serial { get; set; }

    /// <summary>
    /// Applies the information to the given item.
    /// </summary>
    /// <param name="item">The item to apply the information to.</param>
    public virtual void ApplyTo(ItemBase item) => item.ItemSerial = Serial;

    /// <summary>
    /// Gives a new item to the player with the information applied.
    /// </summary>
    /// <param name="player">The player to give the item to.</param>
    /// <returns>The item that was given to the player.</returns>
    public virtual ItemBase GiveTo(Player player)
    {
        var item = player.ReferenceHub.inventory.ServerAddItem(Type, ItemAddReason.Undefined, Serial);
        ApplyTo(item);
        return item;
    }

}
