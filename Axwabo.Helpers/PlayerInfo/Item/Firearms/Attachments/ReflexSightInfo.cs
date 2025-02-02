using InventorySystem.Items.Firearms.Attachments.Components;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Attachments;

/// <summary>
/// Contains information about a <see cref="ReflexSightAttachment"/>.
/// </summary>
public class ReflexSightInfo : FirearmAttachmentInfo
{

    /// <summary>
    /// Gets information about a <see cref="ReflexSightAttachment"/>.
    /// </summary>
    /// <param name="reflexSight">The attachment to get the information from.</param>
    /// <returns>The information about the attachment.</returns>
    public static FirearmAttachmentInfo Get(ReflexSightAttachment reflexSight) => new ReflexSightInfo(reflexSight.IsEnabled)
    {
        Texture = reflexSight.CurTextureIndex,
        Color = reflexSight.CurColorIndex,
        Size = reflexSight.CurSizeIndex,
        Brightness = reflexSight.CurBrightnessIndex
    };

    /// <summary>The current texture index of the attachment.</summary>
    public int Texture { get; set; }

    /// <summary>The current color index of the attachment.</summary>
    public int Color { get; set; }

    /// <summary>The current size index of the attachment.</summary>
    public int Size { get; set; }

    /// <summary>The current brightness index of the attachment.</summary>
    public int Brightness { get; set; }

    /// <summary>
    /// Creates a new <see cref="ReflexSightAttachment"/> instance.
    /// </summary>
    /// <param name="isEnabled">Whether the attachment is enabled.</param>
    public ReflexSightInfo(bool isEnabled) : base(isEnabled)
    {
    }

    /// <inheritdoc />
    public override void ApplyTo(Attachment attachment)
    {
        base.ApplyTo(attachment);
        if (attachment is not ReflexSightAttachment reflexSight)
            return;
        reflexSight.SetValues(Texture, Color, Size, Brightness);
        var data = new ReflexSightSyncData(reflexSight);
        reflexSight.SetDatabaseEntry(data);
        reflexSight.ServerSendData(data, false);
    }

}
