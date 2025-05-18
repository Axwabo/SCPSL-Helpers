using Axwabo.Helpers.PlayerInfo.Containers;
using PlayerRoles.PlayableScps.Scp3114;

namespace Axwabo.Helpers.PlayerInfo.Vanilla;

/// <summary>
/// Contains information about SCP-3114.
/// </summary>
/// <seealso cref="StandardPlayerInfo"/>
/// <seealso cref="PlayerInfoBase"/>
/// <seealso cref="Scp3114Role"/>
public class Scp3114Info : PlayerInfoBase
{

    /// <summary>
    /// Creates an <see cref="Scp3114Info"/> instance using the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to get the information from.</param>
    /// <returns>The information about SCP-3114.</returns>
    public static Scp3114Info Get(Player player) =>
        !player.RoleIs<Scp3114Role>(out var role) || !role.SubroutineModule.TryGetSubroutine(out Scp3114Identity identity)
            ? null
            : new Scp3114Info(
                new Scp3114Identity.StolenIdentity
                {
                    _status = identity.CurIdentity._status,
                    Ragdoll = identity.CurIdentity.Ragdoll,
                    UnitNameId = identity.CurIdentity.UnitNameId,
                },
                identity.RemainingDuration,
                BasicRoleInfo.Get(player)
            );

    /// <summary>
    /// Checks if the given player is SCP-3114.
    /// </summary>
    /// <param name="p">The player to check.</param>
    /// <returns>Whether the given player is SCP-3114.</returns>
    public static bool Is3114(Player p) => p.RoleIs<Scp3114Role>();

    /// <summary>
    /// Creates a new <see cref="Scp3114Info"/> instance.
    /// </summary>
    /// <param name="identity">The information of the stolen identity.</param>
    /// <param name="remainingDisguise">The amount of time remaining from the disguise.</param>
    /// <param name="basicRoleInfo">Basic information about the player.</param>
    public Scp3114Info(Scp3114Identity.StolenIdentity identity, CooldownInfo remainingDisguise, BasicRoleInfo basicRoleInfo) : base(basicRoleInfo)
    {
        Identity = identity;
        RemainingDisguise = remainingDisguise;
    }

    #region Properties

    /// <summary>The information of the stolen identity.</summary>
    public Scp3114Identity.StolenIdentity Identity { get; }

    /// <summary>The amount of time remaining from the disguise.</summary>
    public CooldownInfo RemainingDisguise { get; }

    #endregion

    /// <inheritdoc />
    public override void ApplyTo(Player player)
    {
        if (!player.IsConnected())
            return;
        base.ApplyTo(player);
        if (!player.RoleIs<Scp3114Role>(out var role) || !role.SubroutineModule.TryGetSubroutine(out Scp3114Identity identity))
            return;
        RemainingDisguise.ApplyTo(identity.RemainingDuration);
        identity.CurIdentity.Ragdoll = Identity.Ragdoll;
        identity.CurIdentity.UnitNameId = Identity.UnitNameId;
        identity.CurIdentity.Status = Identity._status;
    }

}
