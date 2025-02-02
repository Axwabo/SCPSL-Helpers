using PlayerRoles;

namespace Axwabo.Helpers.PlayerInfo;

/// <summary>
/// Wraps a <see cref="PlayerInfoBase"/> object and a <see cref="RoleTypeId"/> into a single <see langword="struct"/>.
/// </summary>
public readonly struct RoleTypeAndInfoWrapper : IPlayerInfoWithRole
{

    /// <summary>An empty info object.</summary>
    public static readonly RoleTypeAndInfoWrapper Empty = new();

    /// <summary>
    /// Creates a new <see cref="RoleTypeAndInfoWrapper"/> instance from the player using the given <paramref name="infoGetter"/>.
    /// </summary>
    /// <param name="player">The player to get the info from.</param>
    /// <param name="infoGetter">A method that returns the info object.</param>
    /// <returns>A new <see cref="RoleTypeAndInfoWrapper"/> instance.</returns>
    public static RoleTypeAndInfoWrapper Get(Player player, PlayerInfoGetter infoGetter) =>
        player is null ? Empty : new RoleTypeAndInfoWrapper(player.Rm().CurrentRole.RoleTypeId, infoGetter(player));

    /// <summary>The role of the player.</summary>
    public readonly RoleTypeId Role;

    /// <inheritdoc/>
    public PlayerInfoBase Info { get; }

    /// <summary>
    /// Creates a new <see cref="RoleTypeAndInfoWrapper"/> instance.
    /// </summary>
    /// <param name="role">The role of the player.</param>
    /// <param name="info">Gameplay information about the player.</param>
    public RoleTypeAndInfoWrapper(RoleTypeId role, PlayerInfoBase info)
    {
        Role = role;
        Info = info ?? throw new ArgumentNullException(nameof(info));
    }

    /// <inheritdoc/>
    public void SetClass(Player player) => SetClass(player.Rm());

    /// <summary>
    /// Sets the class of a player using its <see cref="CharacterClassManager"/>.
    /// </summary>
    /// <param name="rm">The <see cref="CharacterClassManager"/> of the player to set the class of.</param>
    public void SetClass(PlayerRoleManager rm)
    {
        if (Role >= RoleTypeId.Scp173)
            rm.ServerSetRole(Role, RoleChangeReason.RemoteAdmin);
    }

    /// <inheritdoc/>
    public void ApplyInfo(Player player) => Info?.ApplyTo(player);

    /// <inheritdoc/>
    public void SetClassAndApplyInfo(Player player)
    {
        SetClass(player);
        var info = Info;
        UnityHelper.CallAfterFrames(() => info.ApplyTo(player), 2);
    }

}
