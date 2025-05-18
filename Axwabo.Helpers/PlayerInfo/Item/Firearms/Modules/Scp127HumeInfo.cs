using InventorySystem.Items.Firearms.Modules;
using InventorySystem.Items.Firearms.Modules.Scp127;
using Mirror;

namespace Axwabo.Helpers.PlayerInfo.Item.Firearms.Modules;

/// <summary>
/// Contains information about an <see cref="Scp127HumeModule"/>.
/// </summary>
public class Scp127HumeInfo : FirearmModuleInfo
{

    /// <summary>
    /// Gets information about an <see cref="Scp127HumeModule"/>.
    /// </summary>
    /// <param name="humeModule">The module to get the information from.</param>
    /// <returns>The information about the module..</returns>
    public static Scp127HumeInfo Get(Scp127HumeModule humeModule)
    {
        foreach (var session in Scp127HumeModule.ServerActiveSessions)
            if (session._lastModule == humeModule)
                return new Scp127HumeInfo {SecondsSinceLastDamage = session.LastDamageElapsed};
        return new Scp127HumeInfo {SecondsSinceLastDamage = double.NaN};
    }

    /// <summary>The amount of seconds elapsed since the owner last took damage. <see cref="double.NaN"/> if there was no session associated with the module.</summary>
    public double SecondsSinceLastDamage { get; set; }

    /// <inheritdoc />
    public override void ApplyTo(ModuleBase module)
    {
        if (module is not Scp127HumeModule humeModule)
            return;
        for (var i = 0; i < Scp127HumeModule.ServerActiveSessions.Count; i++)
        {
            var session = Scp127HumeModule.ServerActiveSessions[i];
            if (session._lastModule != humeModule)
                continue;
            if (double.IsNaN(SecondsSinceLastDamage))
            {
                Scp127HumeModule.ServerActiveSessions.RemoveAt(i);
                return;
            }

            session._lastDamage = NetworkTime.time - SecondsSinceLastDamage;
            return;
        }

        Scp127HumeModule.ServerActiveSessions.Add(new Scp127HumeModule.HumeShieldSession(humeModule)
        {
            _lastDamage = NetworkTime.time - SecondsSinceLastDamage
        });
    }

}
