using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public abstract class FirearmModuleInfo
{

    public static FirearmModuleInfo Get(ModuleBase module) => module switch
    {
        _ => null
    };

    public abstract void ApplyTo(ModuleBase module);

}
