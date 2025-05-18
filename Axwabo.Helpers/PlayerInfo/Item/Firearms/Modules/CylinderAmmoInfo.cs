using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// Contains information about a <see cref="CylinderAmmoModule"/>.
/// </summary>
public class CylinderAmmoInfo : FirearmModuleInfo
{

    /// <summary>
    /// Gets the chambers of the <see cref="CylinderAmmoModule"/>.
    /// </summary>
    /// <param name="cylinder">The module to retrieve the chambers from.</param>
    /// <returns>The chambers of the module.</returns>
    public static CylinderAmmoModule.Chamber[] GetChambers(CylinderAmmoModule cylinder) => CylinderAmmoModule.GetChambersArrayForSerial(cylinder.ItemSerial, cylinder.AmmoMax);

    /// <summary>
    /// Gets information about a <see cref="CylinderAmmoModule"/>.
    /// </summary>
    /// <param name="module">The module to get the information from.</param>
    /// <returns>The information about the module.</returns>
    public static CylinderAmmoInfo Get(CylinderAmmoModule module) => new()
    {
        Chambers = GetChambers(module).Select(chamber => chamber.ServerSyncState).ToArray()
    };

    /// <summary>The states of the chambers.</summary>
    public CylinderAmmoModule.ChamberState[] Chambers { get; set; }

    /// <inheritdoc />
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
