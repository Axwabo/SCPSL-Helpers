using System.Linq;
using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public static class ModuleInfoExtensions
{

    public static FirearmModuleInfo GetInfo(this ModuleBase module) => module switch
    {
        AutomaticActionModule automaticActionModule => AutomaticActionInfo.Get(automaticActionModule),
        CylinderAmmoModule cylinderAmmoModule => CylinderAmmoInfo.Get(cylinderAmmoModule),
        DisruptorModeSelector disruptorModeSelector => DisruptorModeInfo.Get(disruptorModeSelector),
        DoubleActionModule doubleActionModule => DoubleActionInfo.Get(doubleActionModule),
        MagazineModule magazineModule => MagazineInfo.Get(magazineModule),
        PumpActionModule pumpActionModule => PumpActionInfo.Get(pumpActionModule),
        _ => null
    };

    public static void ApplyTo(this FirearmModuleInfo[] attachments, Firearm firearm)
    {
        for (var i = 0; i < attachments.Length; i++)
            attachments[i]?.ApplyTo(firearm.Modules[i]);
    }

    public static FirearmModuleInfo[] GetModuleInfos(this Firearm firearm)
        => firearm.Modules.Select(GetInfo).ToArray();

}
