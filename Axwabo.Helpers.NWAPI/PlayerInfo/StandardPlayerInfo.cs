using Axwabo.Helpers.PlayerInfo.Containers;
using PluginAPI.Core;

namespace Axwabo.Helpers.PlayerInfo;

/// <summary>
/// An implementation of <see cref="PlayerInfoBase"/> for storing gameplay information about a player.
/// </summary>
/// <remarks>
/// This class is used to avoid Get conflicts between different implementations.
/// </remarks>
public sealed class StandardPlayerInfo : PlayerInfoBase
{

    /// <summary>
    /// Creates a <see cref="StandardPlayerInfo"/> instance using the given <paramref name="player"/>.
    /// </summary>
    /// <param name="player">The player to get the information from.</param>
    /// <returns>A <see cref="StandardPlayerInfo"/> instance.</returns>
    public static StandardPlayerInfo Get(Player player) => new(BasicRoleInfo.Get(player));

    /// <summary>
    /// Creates a <see cref="StandardPlayerInfo"/> instance.
    /// </summary>
    /// <param name="info">Basic information about the player.</param>
    public StandardPlayerInfo(BasicRoleInfo info) : base(info)
    {
    }

}
