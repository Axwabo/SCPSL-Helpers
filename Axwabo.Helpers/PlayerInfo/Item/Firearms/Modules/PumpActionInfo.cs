using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// Contains information about a <see cref="PumpActionModule"/>.
/// </summary>
public class PumpActionInfo : FirearmModuleInfo
{

    /// <summary>
    /// Gets information about a <see cref="PumpActionModule"/>.
    /// </summary>
    /// <param name="module">The module to get the information from.</param>
    /// <returns>The information about the module.</returns>
    public static PumpActionInfo Get(PumpActionModule module) => new()
    {
        CockedAmmo = module.SyncCocked,
        AmmoInChamber = module.SyncChambered
    };

    /// <summary>The amount of cocked ammo that can be fired immediately.</summary>
    public int CockedAmmo { get; set; }

    /// <summary>The amount of ammo in the chamber.</summary>
    public int AmmoInChamber { get; set; }

    /// <inheritdoc />
    public override void ApplyTo(ModuleBase module)
    {
        if (module is not PumpActionModule pump)
            return;
        pump.SyncCocked = CockedAmmo;
        pump.SyncChambered = AmmoInChamber;
        pump.ServerResync();
    }

}
