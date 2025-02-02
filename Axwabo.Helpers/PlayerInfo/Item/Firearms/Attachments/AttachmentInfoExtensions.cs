using System.Linq;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments.Components;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Attachments;

/// <summary>
/// Extension methods for retrieving and applying firearm attachment info.
/// </summary>
public static class AttachmentInfoExtensions
{

    /// <summary>
    /// Gets the info about the <see cref="Attachment"/>.
    /// </summary>
    /// <param name="attachment">The attachment to get the info from.</param>
    /// <returns>A <see cref="FirearmAttachmentInfo"/> storing the information.</returns>
    public static FirearmAttachmentInfo GetInfo(this Attachment attachment) => attachment switch
    {
        ReflexSightAttachment reflexSight => ReflexSightInfo.Get(reflexSight),
        _ => new FirearmAttachmentInfo(attachment.IsEnabled)
    };

    /// <summary>
    /// Applies all attachment information to the firearm.
    /// The array must be ordered the same way as the firearm's attachments.
    /// </summary>
    /// <param name="attachments">The attachment infos to apply.</param>
    /// <param name="firearm">The firearm to apply the infos to.</param>
    public static void ApplyTo(this FirearmAttachmentInfo[] attachments, Firearm firearm)
    {
        for (var i = 0; i < attachments.Length; i++)
            attachments[i]?.ApplyTo(firearm.Attachments[i]);
    }

    /// <summary>
    /// Retrieves all attachment information about the firearm.
    /// </summary>
    /// <param name="firearm">The firearm to get all attachment infos from.</param>
    /// <returns>The array of attachment information in the order of the firearm's attachments.</returns>
    public static FirearmAttachmentInfo[] GetAttachmentInfos(this Firearm firearm)
        => firearm.Attachments.Select(GetInfo).ToArray();

}
