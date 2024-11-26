using System.Linq;
using InventorySystem.Items.Firearms;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public static class ModuleInfoExtensions
{

    public static void ApplyTo(this FirearmModuleInfo[] attachments, Firearm firearm)
    {
        for (var i = 0; i < attachments.Length; i++)
            attachments[i]?.ApplyTo(firearm.Modules[i]);
    }

    public static FirearmModuleInfo[] GetModuleInfos(this Firearm firearm)
        => firearm.Modules.Select(FirearmModuleInfo.Get).ToArray();

}
