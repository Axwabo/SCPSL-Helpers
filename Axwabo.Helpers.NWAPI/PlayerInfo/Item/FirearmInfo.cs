using Axwabo.Helpers.PlayerInfo.Item.Firearms.Attachments;
using Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;
using InventorySystem.Items;
using InventorySystem.Items.Firearms;

namespace Axwabo.Helpers.PlayerInfo.Item;

/// <summary>
/// Contains gameplay information about a firearm.
/// </summary>
public class FirearmInfo : ItemInfoBase
{

    /// <summary>
    /// Gets information about a firearm.
    /// </summary>
    /// <param name="item">The item to get the information from.</param>
    /// <returns>The information about the firearm.</returns>
    public static FirearmInfo Get(ItemBase item) => item is not Firearm f
        ? null
        : new FirearmInfo(f.GetAttachmentInfos(), f.GetModuleInfos(), item.ItemTypeId, item.ItemSerial);

    /// <summary>
    /// Checks if the given item is a firearm.
    /// </summary>
    /// <param name="item">The item to check.</param>
    /// <returns>Whether the item is a firearm.</returns>
    public static bool IsFirearm(ItemBase item) => item is Firearm;

    /// <summary>
    /// Creates a new <see cref="FirearmInfo"/> instance.
    /// </summary>
    /// <param name="attachments">Info about the firearm's attachments.</param>
    /// <param name="modules">Info about the firearm's modules.</param>
    /// <param name="type">The type of the item.</param>
    /// <param name="serial">The serial of the item.</param>
    public FirearmInfo(FirearmAttachmentInfo[] attachments, FirearmModuleInfo[] modules, ItemType type, ushort serial) : base(type, serial)
    {
        Attachments = attachments;
        Modules = modules;
    }

    public FirearmAttachmentInfo[] Attachments { get; set; }

    public FirearmModuleInfo[] Modules { get; set; }

    /// <inheritdoc />
    public override void ApplyTo(ItemBase item)
    {
        base.ApplyTo(item);
        if (item is not Firearm firearm)
            return;
        Attachments.ApplyTo(firearm);
        Modules.ApplyTo(firearm);
    }

}
