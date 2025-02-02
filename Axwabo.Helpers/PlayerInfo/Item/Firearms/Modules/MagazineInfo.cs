using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// Contains information about a <see cref="MagazineModule"/>.
/// </summary>
public class MagazineInfo : FirearmModuleInfo
{

    /// <summary>
    /// Gets information about a <see cref="MagazineModule"/>.
    /// </summary>
    /// <param name="module">The module to get the information from.</param>
    /// <returns>The information about the module.</returns>
    public static MagazineInfo Get(MagazineModule module) => new()
    {
        Inserted = module.MagazineInserted,
        Ammo = module.AmmoStored
    };

    /// <summary>Whether the magazine is inserted into the firearm.</summary>
    public bool Inserted { get; set; }

    /// <summary>The amount of ammo stored in the magazine.</summary>
    public int Ammo { get; set; }

    /// <inheritdoc />
    public override void ApplyTo(ModuleBase module)
    {
        if (module is not MagazineModule magazine)
            return;
        magazine.MagazineInserted = Inserted;
        magazine.AmmoStored = Ammo;
        magazine.ServerResyncData();
    }

}
