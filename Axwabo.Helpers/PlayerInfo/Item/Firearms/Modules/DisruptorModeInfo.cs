using InventorySystem.Items.Firearms.Modules;
using Mirror;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// Contains information about a <see cref="DisruptorModeSelector"/>.
/// </summary>
/// <remarks>The state is not stored to the server, it's proxied back to other clients.</remarks>
public class DisruptorModeInfo : FirearmModuleInfo
{

    /// <summary>
    /// Gets information about a <see cref="DisruptorModeSelector"/>.
    /// The <see cref="SingleShotSelected"/> state is not stored on the server, therefore the value is lost.
    /// </summary>
    /// <param name="selector">The module to get the information from.</param>
    /// <returns>The information about the module.</returns>
    public static DisruptorModeInfo Get(DisruptorModeSelector selector) => new()
    {
        SingleShotSelected = selector.SingleShotSelected
    };

    /// <summary>Whether single-shot mode is selected.</summary>
    public bool SingleShotSelected { get; set; }

    /// <inheritdoc />
    public override void ApplyTo(ModuleBase module)
    {
        if (module is DisruptorModeSelector selector)
            selector.SendRpc(writer => writer.WriteBool(SingleShotSelected));
    }

}
