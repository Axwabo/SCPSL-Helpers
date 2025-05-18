using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// Contains information about an <see cref="AutomaticActionModule"/>.
/// </summary>
public class AutomaticActionInfo : FirearmModuleInfo
{

    /// <summary>
    /// Gets information about an <see cref="AutomaticActionModule"/>.
    /// </summary>
    /// <param name="actionModule">The module to get the information from.</param>
    /// <returns>The information about the module.</returns>
    public static AutomaticActionInfo Get(AutomaticActionModule actionModule) => new()
    {
        BoltLocked = actionModule.BoltLocked,
        Cocked = actionModule.Cocked,
        AmmoInChamber = actionModule.AmmoStored
    };

    /// <summary>Whether the firearm is cocked.</summary>
    public bool Cocked { get; set; }

    /// <summary>Whether the firearm has a locked bolt.</summary>
    public bool BoltLocked { get; set; }

    /// <summary>The amount of ammo stored in the chamber.</summary>
    public int AmmoInChamber { get; set; }

    /// <inheritdoc />
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
