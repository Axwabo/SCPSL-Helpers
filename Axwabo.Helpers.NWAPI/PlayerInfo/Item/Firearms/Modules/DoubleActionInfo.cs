using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public class DoubleActionInfo : FirearmModuleInfo
{

    public static DoubleActionInfo Get(DoubleActionModule doubleActionModule) => new()
    {
        Cocked = doubleActionModule.Cocked
    };

    public bool Cocked { get; set; }

    public override void ApplyTo(ModuleBase module)
    {
        if (module is DoubleActionModule doubleActionModule)
            doubleActionModule.Cocked = Cocked;
    }

}
