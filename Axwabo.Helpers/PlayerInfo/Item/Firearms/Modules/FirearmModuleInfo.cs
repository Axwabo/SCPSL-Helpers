using InventorySystem.Items.Firearms.Modules;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// A base class for storing information about <see cref="ModuleBase">firearm modules</see>.
/// </summary>
public abstract class FirearmModuleInfo
{

    /// <summary>
    /// Applies the information to the given <see cref="ModuleBase"/>.
    /// </summary>
    /// <param name="module">The module to apply the information to.</param>
    public abstract void ApplyTo(ModuleBase module);

}
