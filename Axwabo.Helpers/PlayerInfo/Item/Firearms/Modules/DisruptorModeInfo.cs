using InventorySystem.Items.Firearms.Modules;
using Mirror;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public class DisruptorModeInfo : FirearmModuleInfo
{

    public static DisruptorModeInfo Get(DisruptorModeSelector selector) => new()
    {
        SingleShotSelected = selector.SingleShotSelected
    };

    public bool SingleShotSelected { get; set; }

    public override void ApplyTo(ModuleBase module)
    {
        if (module is DisruptorModeSelector selector)
            selector.SendRpc(writer => writer.WriteBool(SingleShotSelected));
    }

}
