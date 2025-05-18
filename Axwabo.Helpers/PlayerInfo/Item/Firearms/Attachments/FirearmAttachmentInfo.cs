using InventorySystem.Items.Firearms.Attachments.Components;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Attachments;

/// <summary>
/// A base class for storing information about a firearm attachment.
/// </summary>
public class FirearmAttachmentInfo
{

    /// <summary>Whether the attachment is enabled.</summary>
    public bool IsEnabled { get; }

    /// <summary>
    /// Creates a new <see cref="FirearmAttachmentInfo"/> instance.
    /// </summary>
    /// <param name="isEnabled">Whether the attachment is enabled.</param>
    public FirearmAttachmentInfo(bool isEnabled) => IsEnabled = isEnabled;

    /// <summary>
    /// Applies the information to the given <see cref="Attachment"/>.
    /// </summary>
    /// <param name="attachment">The attachment to apply the information to.</param>
    public virtual void ApplyTo(Attachment attachment) => attachment.IsEnabled = IsEnabled;

}
