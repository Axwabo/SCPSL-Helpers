using System.Linq;
using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public class CylinderAmmoInfo : FirearmModuleInfo
{

    public static CylinderAmmoModule.Chamber[] GetChambers(CylinderAmmoModule cylinder) => CylinderAmmoModule.GetChambersArrayForSerial(cylinder.ItemSerial, cylinder.AmmoMax);

    public static CylinderAmmoInfo Get(CylinderAmmoModule module) => new()
    {
        Chambers = GetChambers(module).Select(chamber => chamber.ServerSyncState).ToArray()
    };

    public CylinderAmmoModule.ChamberState[] Chambers { get; set; }

    public override void ApplyTo(ModuleBase module)
    {
        if (module is not CylinderAmmoModule cylinder)
            return;
        var chambers = GetChambers(cylinder);
        for (var i = 0; i < Chambers.Length; i++)
        {
            var state = Chambers[i];
            chambers[i].ServerSyncState = state;
        }

        cylinder.ServerResync();
    }

}
