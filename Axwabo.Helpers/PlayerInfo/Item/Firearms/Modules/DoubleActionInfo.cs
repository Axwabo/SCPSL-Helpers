using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// Contains information about a <see cref="DoubleActionModule"/>.
/// </summary>
public class DoubleActionInfo : FirearmModuleInfo
{

    /// <summary>
    /// Gets information about a <see cref="DoubleActionModule"/>.
    /// </summary>
    /// <param name="doubleActionModule">The module to get the information from.</param>
    /// <returns>The information about the module.</returns>
    public static DoubleActionInfo Get(DoubleActionModule doubleActionModule) => new()
    {
        Cocked = doubleActionModule.Cocked
    };

    /// <summary>Whether the firearm is cocked.</summary>
    public bool Cocked { get; set; }

    /// <inheritdoc />
    public override void ApplyTo(ModuleBase module)
    {
        if (module is DoubleActionModule doubleActionModule)
            doubleActionModule.Cocked = Cocked;
    }

}
