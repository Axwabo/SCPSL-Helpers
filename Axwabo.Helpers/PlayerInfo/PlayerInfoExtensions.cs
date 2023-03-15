using Exiled.API.Features;
using PlayerRoles;
using PlayerRoles.PlayableScps.Subroutines;
using Utils.Networking;

namespace Axwabo.Helpers.PlayerInfo;

/// <summary>
/// Extension methods for working with player info.
/// </summary>
public static class PlayerInfoExtensions
{

    /// <summary>
    /// Gets the first matching <see cref="PlayerInfoObtainer"/> method for for the given player.
    /// </summary>
    /// <param name="p">The player to get the info obtainer from.</param>
    /// <returns>A method that can be used to get the player's info.</returns>
    /// <seealso cref="PlayerInfoBase.GetFirstMatchingObtainer"/>
    public static PlayerInfoObtainer GetFirstMatchingObtainer(this Player p) => PlayerInfoBase.GetFirstMatchingObtainer(p);

    /// <summary>
    /// Creates a <see cref="PlayerInfoBase"/> from a player based on the registered obtainers.
    /// </summary>
    /// <param name="p">The player to get the info from.</param>
    /// <returns>The player info. If no special obtainer was found, it will be a <see cref="StandardPlayerInfo"/>.</returns>
    /// <seealso cref="PlayerInfoBase.CreateAutomatically"/>
    public static PlayerInfoBase GetInfo(this Player p) => PlayerInfoBase.CreateAutomatically(p);

    /// <summary>
    /// Gets the player info with the <see cref="RoleTypeId">vanilla role type</see> from the player. 
    /// </summary>
    /// <param name="p">The player to get the info from.</param>
    /// <returns>The player info. with the vanilla role.</returns>
    public static RoleTypeAndInfoWrapper GetInfoWithVanillaRole(this Player p) =>
        RoleTypeAndInfoWrapper.Get(p, PlayerInfoBase.CreateAutomatically);

    /// <summary>
    /// Gets the player info with a custom role type from the player.
    /// </summary>
    /// <param name="p">The player to get the info from.</param>
    /// <returns>The player info with the custom role. If no obtainer was found, it will be <see cref="CustomRoleAndInfoWrapper.Empty"/>.</returns>
    public static CustomRoleAndInfoWrapper GetInfoWithCustomRole(this Player p) =>
        GetInfoWithCustomRole(p, PlayerInfoBase.GetFirstMatchingObtainer(p));

    /// <summary>
    /// Gets the player info with a custom role type from the player using the given <see cref="PlayerInfoObtainer"/>.
    /// </summary>
    /// <param name="p">The player to get the info from.</param>
    /// <param name="obtainer">The obtainer to use.</param>
    /// <returns>The player info and custom role.</returns>
    public static CustomRoleAndInfoWrapper GetInfoWithCustomRole(this Player p, PlayerInfoObtainer obtainer) =>
        !obtainer.IsValid ? CustomRoleAndInfoWrapper.Empty : CustomRoleAndInfoWrapper.Get(p, obtainer);

    /// <summary>
    /// Gets the player info with a custom role or vanilla role from the player.
    /// </summary>
    /// <param name="p">The player to get the info from.</param>
    /// <returns>The player info with the role.</returns>
    public static IPlayerInfoWithRole GetInfoWithRole(this Player p)
    {
        var custom = GetInfoWithCustomRole(p);
        return custom.Info != null ? custom : GetInfoWithVanillaRole(p);
    }

    /// <summary>
    /// Gets thr player info from the given <see cref="ReferenceHub"/> based on the registered obtainers.
    /// </summary>
    /// <param name="hub">The hub to get the info from.</param>
    /// <returns>The player info. If no special obtainer was found, it will be a <see cref="StandardPlayerInfo"/>.</returns>
    public static PlayerInfoBase GetInfo(this ReferenceHub hub) => Player.Get(hub)?.GetInfo();

    /// <summary>
    /// Gets the player info with the <see cref="RoleTypeId">vanilla role type</see> from the given <see cref="ReferenceHub"/>.
    /// </summary>
    /// <param name="hub">The hub to get the info from.</param>
    /// <returns>The player info with the vanilla role.</returns>
    public static RoleTypeAndInfoWrapper GetInfoWithVanillaRole(this ReferenceHub hub) =>
        Player.Get(hub)?.GetInfoWithVanillaRole() ?? RoleTypeAndInfoWrapper.Empty;

    /// <summary>
    /// Gets the player info with a custom role type from the given <see cref="ReferenceHub"/>.
    /// </summary>
    /// <param name="hub">The hub to get the info from.</param>
    /// <returns>The player info with the custom role. If no obtainer was found or the <see cref="ReferenceHub"/> could not be mapped to a <see cref="Player"/>, it will be <see cref="CustomRoleAndInfoWrapper.Empty"/>.</returns>
    public static CustomRoleAndInfoWrapper GetInfoWithCustomRole(this ReferenceHub hub) =>
        Player.Get(hub)?.GetInfoWithCustomRole() ?? CustomRoleAndInfoWrapper.Empty;

    /// <summary>
    /// Gets the player info with a custom role type from the given <see cref="ReferenceHub"/> using the given <see cref="PlayerInfoObtainer"/>.
    /// </summary>
    /// <param name="hub">The hub to get the info from.</param>
    /// <param name="obtainer">The obtainer to use.</param>
    /// <returns>The player info and custom role. If the <see cref="ReferenceHub"/> could not be mapped to a <see cref="Player"/>, it will be <see cref="CustomRoleAndInfoWrapper.Empty"/>.</returns>
    public static CustomRoleAndInfoWrapper GetInfoWithCustomRole(this ReferenceHub hub, PlayerInfoObtainer obtainer) =>
        Player.Get(hub)?.GetInfoWithCustomRole(obtainer) ?? CustomRoleAndInfoWrapper.Empty;

    /// <summary>
    /// Gets the player info with a custom role or vanilla role from the given <see cref="ReferenceHub"/>.
    /// </summary>
    /// <param name="hub">The hub to get the info from.</param>
    /// <returns>The player info with the role. If the ReferenceHub <see cref="ReferenceHub"/> could not be mapped to a <see cref="Player"/>, it will be <see cref="RoleTypeAndInfoWrapper.Empty"/>.</returns>
    public static IPlayerInfoWithRole GetInfoWithRole(this ReferenceHub hub) =>
        Player.Get(hub)?.GetInfoWithRole() ?? RoleTypeAndInfoWrapper.Empty;

    /// <summary>
    /// Copies the gameplay data from one player to another.
    /// </summary>
    /// <param name="from">The player to copy the data from.</param>
    /// <param name="to">The player to transfer the data to.</param>
    /// <returns>The player that the data was transferred to.</returns>
    public static bool CopyInfo(this Player from, Player to)
    {
        var info = from.GetInfoWithRole();
        if (info.Info == null)
            return false;
        info.SetClassAndApplyInfo(to);
        return true;
    }

    /// <summary>
    /// Sends a subroutine sync message to all authenticated players.
    /// </summary>
    /// <param name="routine">The routine to sync.</param>
    public static void Sync(this ScpSubroutineBase routine) => new SubroutineMessage(routine, true).SendToAuthenticated();

}
