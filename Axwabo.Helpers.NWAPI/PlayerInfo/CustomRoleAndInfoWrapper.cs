using System;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo;

/// <summary>
/// Wraps a <see cref="PlayerInfoBase"/> object and a <see cref="PlayerRoleSetter"/> into a single <see langword="struct"/>.
/// </summary>
public readonly struct CustomRoleAndInfoWrapper : IPlayerInfoWithRole
{

    /// <summary>An empty info object.</summary>
    public static readonly CustomRoleAndInfoWrapper Empty = new();

    /// <summary>
    /// Creates a new <see cref="CustomRoleAndInfoWrapper"/> instance from the player using the given <paramref name="infoGetter"/> and <paramref name="roleSetter"/>.
    /// </summary>
    /// <param name="player">The player to get the info from.</param>
    /// <param name="infoGetter">A method that returns the info object.</param>
    /// <param name="roleSetter">A method to set the player's role.</param>
    /// <returns>A new <see cref="CustomRoleAndInfoWrapper"/> instance.</returns>
    public static CustomRoleAndInfoWrapper Get(Player player, PlayerInfoGetter infoGetter, PlayerRoleSetter roleSetter) =>
        player is null ? Empty : new CustomRoleAndInfoWrapper(roleSetter, infoGetter(player));

    /// <summary>
    /// Creates a new <see cref="CustomRoleAndInfoWrapper"/> instance from the player using the given <paramref name="obtainer"/>.
    /// </summary>
    /// <param name="player">The player to get the info from.</param>
    /// <param name="obtainer">A <see cref="PlayerInfoObtainer"/> struct to get the methods from.</param>
    /// <returns>A new <see cref="CustomRoleAndInfoWrapper"/> instance.</returns>
    /// <seealso cref="Get(Player,Axwabo.Helpers.PlayerInfo.PlayerInfoGetter,Axwabo.Helpers.PlayerInfo.PlayerRoleSetter)"/>
    public static CustomRoleAndInfoWrapper Get(Player player, PlayerInfoObtainer obtainer) =>
        Get(player, obtainer.Get, obtainer.SetRole);

    /// <summary>The role of the player.</summary>
    public readonly PlayerRoleSetter SetRole;

    /// <inheritdoc/>
    public PlayerInfoBase Info { get; }

    /// <summary>
    /// Creates a new <see cref="CustomRoleAndInfoWrapper"/> instance.
    /// </summary>
    /// <param name="role">The role of the player.</param>
    /// <param name="info">Gameplay information about the player.</param>
    public CustomRoleAndInfoWrapper(PlayerRoleSetter role, PlayerInfoBase info)
    {
        SetRole = role;
        Info = info ?? throw new ArgumentNullException(nameof(info));
    }

    /// <inheritdoc/>
    public void SetClass(Player player) => SetRole(player);

    /// <inheritdoc/>
    public void ApplyInfo(Player player) => Info?.ApplyTo(player);

    /// <inheritdoc/>
    public void SetClassAndApplyInfo(Player player)
    {
        if (SetRole != null)
            SetClass(player);
        var info = Info;
        UnityHelper.CallAfterFrames(() => info.ApplyTo(player), 2);
    }

}
