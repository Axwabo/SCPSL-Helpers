using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public class PumpActionInfo : FirearmModuleInfo
{

    public static PumpActionInfo Get(PumpActionModule module) => new()
    {
        CockedAmmo = module.SyncCocked,
        AmmoInChamber = module.SyncChambered
    };

    public int CockedAmmo { get; set; }

    public int AmmoInChamber { get; set; }

    public override void ApplyTo(ModuleBase module)
    {
        if (module is not PumpActionModule pump)
            return;
        pump.SyncCocked = CockedAmmo;
        pump.SyncChambered = AmmoInChamber;
        pump.ServerResync();
    }

}
