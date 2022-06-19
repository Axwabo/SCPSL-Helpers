using System;
using System.Collections.Generic;
using System.Linq;
using Exiled.API.Features;
using Utils;

namespace Axwabo.Helpers {

    /// Extension methods for the EXILED <see cref="Player"/> API.
    public static class ExiledPlayerExtensions {

        /// <summary>
        /// Gets the team stored in the player's <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="player">The player to get the team from.</param>
        /// <returns>The actual team of the player.</returns>
        public static Team ActualTeam(this Player player) => Ccm(player).CurRole.team;

        /// <summary>
        /// Gets the faction stored in the player's <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="player">The player to get the faction from.</param>
        /// <returns>The actual faction of the player.</returns>
        public static Faction ActualFaction(this Player player) => Misc.GetFaction(Ccm(player).CurRole.team);

        /// <summary>
        /// Gets the player's <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="player">The player to get the CCM from.</param>
        /// <returns>The CCM of the player.</returns>
        public static CharacterClassManager Ccm(this Player player) => player.ReferenceHub.characterClassManager;

        /// <summary>
        /// Checks if the player is actually an SCP based on its <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>Whether the player is actually an SCP.</returns>
        public static bool IsActuallyScp(this Player player) => ActualTeam(player) == Team.SCP;

        /// <summary>
        /// Parses Remote Admin arguments to a player list.
        /// </summary>
        /// <param name="args">The list of arguments passed to the command.</param>
        /// <param name="startIndex">The index to start the parsing at.</param>
        /// <param name="newArgs">The newly generated arguments.</param>
        /// <param name="keepEmptyEntries">If empty arguments should be kept.</param>
        /// <returns>The list of parsed players.</returns>
        public static List<Player> ParseArguments(ArraySegment<string> args, int startIndex, out string[] newArgs, bool keepEmptyEntries = false) {
            return RAUtils.ProcessPlayerIdOrNamesList(args, startIndex, out newArgs, keepEmptyEntries).Select(Player.Get).Where(e => e != null).ToList();
        }

    }

}
