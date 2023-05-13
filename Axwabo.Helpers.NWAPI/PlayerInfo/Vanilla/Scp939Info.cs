using Axwabo.Helpers.PlayerInfo.Containers;
using PlayerRoles.PlayableScps.Scp939;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo.Vanilla;

/// <summary>
/// Contains information about SCP-939.
/// </summary>
/// <seealso cref="StandardPlayerInfo"/>
/// <seealso cref="PlayerInfoBase"/>
/// <seealso cref="Scp939Role"/>
public class Scp939Info : PlayerInfoBase
{

    /// <summary>
    /// Creates an <see cref="Scp939Info"/> instance using the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to get the information from.</param>
    /// <returns>The information about SCP-939.</returns>
    public static Scp939Info Get(Player player) =>
        !player.RoleIs<Scp939Role>(out var role) || !role.SubroutineModule.TryGetSubroutine(out Scp939AmnesticCloudAbility amnesticCloud)
            ? null
            : new Scp939Info(
                amnesticCloud.Cooldown,
                amnesticCloud.Duration,
                BasicRoleInfo.Get(player)
            );

    /// <summary>
    /// Checks if the given player is SCP-939.
    /// </summary>
    /// <param name="p">The player to check.</param>
    /// <returns>Whether the given player is SCP-939.</returns>
    public static bool Is939(Player p) => p.RoleIs<Scp939Role>();

    /// <summary>
    /// Creates a new <see cref="Scp939Info"/> instance.
    /// </summary>
    /// <param name="amnesticCloudCooldown">The cooldown of the Amnestic Cloud ability.</param>
    /// <param name="amnesticCloudDuration">The duration of the Amnestic Cloud ability.</param>
    /// <param name="basicRoleInfo">Basic information about the player.</param>
    public Scp939Info(
        CooldownInfo amnesticCloudCooldown,
        CooldownInfo amnesticCloudDuration,
        BasicRoleInfo basicRoleInfo
    ) : base(basicRoleInfo)
    {
        AmnesticCloudCooldown = amnesticCloudCooldown;
        AmnesticCloudDuration = amnesticCloudDuration;
    }

    #region Properties

    /// <summary>The cooldown of the Amnestic Cloud ability.</summary>
    public CooldownInfo AmnesticCloudCooldown { get; set; }

    /// <summary>The duration of the Amnestic Cloud ability.</summary>
    public CooldownInfo AmnesticCloudDuration { get; set; }

    #endregion

    /// <inheritdoc />
    public override void ApplyTo(Player player)
    {
        if (!player.IsConnected())
            return;
        base.ApplyTo(player);
        if (!player.RoleIs<Scp939Role>(out var role) || !role.SubroutineModule.TryGetSubroutine(out Scp939AmnesticCloudAbility amnesticCloud))
            return;
        AmnesticCloudCooldown.ApplyTo(amnesticCloud.Cooldown);
        AmnesticCloudDuration.ApplyTo(amnesticCloud.Duration);
        amnesticCloud.Sync();
    }

}
