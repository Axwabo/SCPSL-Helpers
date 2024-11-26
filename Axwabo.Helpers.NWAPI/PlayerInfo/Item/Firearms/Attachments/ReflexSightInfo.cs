using InventorySystem.Items.Firearms.Attachments.Components;
using Mirror;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Attachments;

public class ReflexSightInfo : FirearmAttachmentInfo
{

    public static FirearmAttachmentInfo Get(ReflexSightAttachment reflexSight) => new ReflexSightInfo(reflexSight.IsEnabled)
    {
        Texture = reflexSight.CurTextureIndex,
        Color = reflexSight.CurColorIndex,
        Size = reflexSight.CurSizeIndex,
        Brightness = reflexSight.CurBrightnessIndex
    };

    public int Texture { get; set; }

    public int Color { get; set; }

    public int Size { get; set; }

    public int Brightness { get; set; }

    public ReflexSightInfo(bool isEnabled) : base(isEnabled)
    {
    }

    public override void ApplyTo(Attachment attachment)
    {
        base.ApplyTo(attachment);
        if (attachment is not ReflexSightAttachment reflexSight)
            return;
        reflexSight.SetValues(Texture, Color, Size, Brightness);
        reflexSight.SendCmd(writer =>
        {
            var data = new ReflexSightSyncData(reflexSight);
            writer.WriteBool(true);
            data.Write(writer);
        });
    }

}
