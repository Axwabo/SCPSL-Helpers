using InventorySystem.Items.Firearms;
using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// Extension methods for retrieving and applying firearm module info.
/// </summary>
public static class ModuleInfoExtensions
{

    /// <summary>
    /// Gets the info about the <see cref="ModuleBase"/>.
    /// </summary>
    /// <param name="module">The module to get the info from.</param>
    /// <returns>A <see cref="FirearmModuleInfo"/> storing the information; null if the module is unknown or doesn't contain syncable information.</returns>
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

    /// <summary>
    /// Applies all module information to the firearm.
    /// The array must be ordered the same way as the firearm's modules.
    /// </summary>
    /// <param name="modules">The module infos to apply.</param>
    /// <param name="firearm">The firearm to apply the infos to.</param>
    public static void ApplyTo(this FirearmModuleInfo[] modules, Firearm firearm)
    {
        for (var i = 0; i < modules.Length; i++)
            modules[i]?.ApplyTo(firearm.Modules[i]);
    }

    /// <summary>
    /// Retrieves all module information about the firearm.
    /// </summary>
    /// <param name="firearm">The firearm to get all module infos from.</param>
    /// <returns>
    /// The array of module information in the order of the firearm's module.
    /// Some elements <see cref="GetInfo">may be null</see>.
    /// </returns>
    public static FirearmModuleInfo[] GetModuleInfos(this Firearm firearm)
        => firearm.Modules.Select(GetInfo).ToArray();

}
