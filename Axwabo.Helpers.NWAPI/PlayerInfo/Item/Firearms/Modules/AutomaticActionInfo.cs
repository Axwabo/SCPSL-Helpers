using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public class AutomaticActionInfo : FirearmModuleInfo
{

    public static AutomaticActionInfo Get(AutomaticActionModule actionModule) => new()
    {
        BoltLocked = actionModule.BoltLocked,
        Cocked = actionModule.Cocked,
        AmmoInChamber = actionModule.AmmoStored
    };

    public bool Cocked { get; set; }

    public bool BoltLocked { get; set; }

    public int AmmoInChamber { get; set; }

    public override void ApplyTo(ModuleBase module)
    {
        if (module is not AutomaticActionModule actionModule)
            return;
        actionModule.BoltLocked = BoltLocked;
        actionModule.Cocked = Cocked;
        actionModule.AmmoStored = AmmoInChamber;
        actionModule.ServerResync();
    }

}
