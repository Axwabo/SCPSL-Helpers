using System.Linq;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Attachments.Components;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Attachments;

public static class AttachmentInfoExtensions
{

    public static FirearmAttachmentInfo GetInfo(this Attachment attachment) => attachment switch
    {
        ReflexSightAttachment reflexSight => ReflexSightInfo.Get(reflexSight),
        _ => new FirearmAttachmentInfo(attachment.IsEnabled)
    };

    public static void ApplyTo(this FirearmAttachmentInfo[] attachments, Firearm firearm)
    {
        for (var i = 0; i < attachments.Length; i++)
            attachments[i]?.ApplyTo(firearm.Attachments[i]);
    }

    public static FirearmAttachmentInfo[] GetAttachmentInfos(this Firearm firearm)
        => firearm.Attachments.Select(GetInfo).ToArray();

}
