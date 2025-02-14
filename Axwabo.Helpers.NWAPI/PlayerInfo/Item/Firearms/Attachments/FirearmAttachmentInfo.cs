﻿using InventorySystem.Items.Firearms.Attachments.Components;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Attachments;

public class FirearmAttachmentInfo
{

    public bool IsEnabled { get; }

    public FirearmAttachmentInfo(bool isEnabled) => IsEnabled = isEnabled;

    public virtual void ApplyTo(Attachment attachment) => attachment.IsEnabled = IsEnabled;

}
