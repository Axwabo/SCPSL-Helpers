using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

public class MagazineInfo : FirearmModuleInfo
{

    public static MagazineInfo Get(MagazineModule module) => new()
    {
        Inserted = module.MagazineInserted,
        Ammo = module.AmmoStored
    };

    public bool Inserted { get; set; }

    public int Ammo { get; set; }

    public override void ApplyTo(ModuleBase module)
    {
        if (module is not MagazineModule magazine)
            return;
        magazine.MagazineInserted = Inserted;
        magazine.AmmoStored = Ammo;
    }

}
