using System;
using System.Collections.Generic;
using System.Linq;
using Axwabo.Helpers.PlayerInfo.Effect;
using CommandSystem;
using CustomPlayerEffects;
using PlayerRoles;
using PlayerRoles.PlayableScps;
using PlayerRoles.Spectating;
using PlayerStatsSystem;
using PluginAPI.Core;
using Utils;

namespace Axwabo.Helpers {

    /// <summary>
    /// Extension methods for the <see cref="Player"/> API.
    /// </summary>
    public static class PlayerExtensions {

        /// <summary>
        /// Gets the team stored in the player's <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="player">The player to get the team from.</param>
        /// <returns>The actual team of the player.</returns>
        public static Team Team(this Player player) => player.ReferenceHub.GetTeam();

        /// <summary>
        /// Gets the faction stored in the player's <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="player">The player to get the faction from.</param>
        /// <returns>The actual faction of the player.</returns>
        public static Faction Faction(this Player player) => player.ReferenceHub.GetFaction();

        /// <summary>
        /// Gets the player's <see cref="PlayerRoleManager"/>.
        /// </summary>
        /// <param name="player">The player to get the RM from.</param>
        /// <returns>The RM of the player.</returns>
        public static PlayerRoleManager Rm(this Player player) => player.ReferenceHub.roleManager;

        /// <summary>
        /// Checks if the player is actually an SCP based on its <see cref="CharacterClassManager"/>.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>Whether the player is actually an SCP.</returns>
        public static bool IsScp(this Player player) => player.Rm().CurrentRole is FpcStandardScp;

        /// <summary>
        /// Parses Remote Admin arguments to a <see cref="Player"/> list.
        /// </summary>
        /// <param name="args">The list of arguments passed to the command.</param>
        /// <param name="startIndex">The index to start the parsing at.</param>
        /// <param name="newArgs">The newly generated arguments.</param>
        /// <param name="keepEmptyEntries">If empty arguments should be kept.</param>
        /// <returns>The list of parsed players.</returns>
        /// <seealso cref="RAUtils.ProcessPlayerIdOrNamesList"/>
        public static List<Player> ParseArguments(ArraySegment<string> args, int startIndex, out string[] newArgs, bool keepEmptyEntries = false) => RAUtils.ProcessPlayerIdOrNamesList(args, startIndex, out newArgs, keepEmptyEntries).Select(Player.Get).Where(e => e != null).ToList();

        /// <summary>
        /// Sends a message to the given command sender.
        /// </summary>
        /// <param name="sender">The sender to reply to.</param>
        /// <param name="message">The message to send.</param>
        /// <param name="success">If the command has executed successfully.</param>
        /// <param name="display">The prefix (command name) for <see cref="CommandSender">command senders</see>.</param>
        /// <seealso cref="ICommandSender.Respond"/>
        /// <seealso cref="CommandSender.RaReply"/>
        public static void Reply(this ICommandSender sender, string message, bool success = true, string display = "") {
            switch (sender) {
                case null:
                    return;
                case CommandSender cs:
                    cs.RaReply(message, success, false, display);
                    return;
                default:
                    sender.Respond(message, success);
                    return;
            }
        }

        /// <summary>
        /// Gets the role of the player.
        /// </summary>
        /// <param name="player">The player to get the role from.</param>
        /// <returns>A <see cref="PlayerRoleBase"/> object.</returns>
        public static PlayerRoleBase Role(this Player player) => player.Rm().CurrentRole;

        /// <summary>
        /// Puts the player to spectator without spawning a ragdoll.
        /// </summary>
        /// <param name="player">The player to kill.</param>
        /// <param name="handler">The death reason to show to the player.</param>
        public static void ForceClassToSpectator(this Player player, DamageHandlerBase handler) {
            var rm = player.Rm();
            rm.ServerSetRole(RoleTypeId.Spectator, RoleChangeReason.Died);
            if (handler == null)
                return;
            player.SendConsoleMessage($"You died. Reason: {handler.ServerLogsText}", "yellow");
            if (rm.CurrentRole is SpectatorRole currentRole)
                currentRole.ServerSetData(handler);
        }

        /// <summary>
        /// Puts the player to spectator without spawning a ragdoll.
        /// </summary>
        /// <param name="player">The player to kill.</param>
        /// <param name="reason">The death reason to show to the player.</param>
        public static void ForceClassToSpectator(this Player player, string reason = null) {
            var rm = player.Rm();
            rm.ServerSetRole(RoleTypeId.Spectator, RoleChangeReason.Died);
            if (string.IsNullOrEmpty(reason))
                return;
            player.SendConsoleMessage($"You died. Reason: {reason}", "yellow");
            if (rm.CurrentRole is SpectatorRole currentRole)
                currentRole.ServerSetData(new CustomReasonDamageHandler(reason));
        }

        /// <summary>
        /// Adds a hint to the player's hint queue.
        /// </summary>
        /// <param name="player">The player to show then hint for.</param>
        /// <param name="message">The message to show.</param>
        /// <param name="duration">The duration of the hint in seconds.</param>
        public static void QueueHint(this Player player, string message, float duration = 5f) =>
            player.GameObject.GetOrAddComponent<HintQueue>().Enqueue(message, duration);

        /// <summary>
        /// Clears the hint queue and current hint of the player.
        /// </summary>
        /// <param name="player">The player to clear the hint queue for.</param>
        public static void ClearHints(this Player player) => player.GameObject.GetOrAddComponent<HintQueue>().Clear();
        
        /// <summary>
        /// Determines whether the player is still connected to the server by checking its GameObject.
        /// </summary>
        /// <param name="player">The player to check.</param>
        /// <returns>Whether the player is connected.</returns>
        public static bool IsConnected(this Player player) => player.GameObject != null;
        
        public static StatusEffectBase GetEffect(this Player player, EffectType effectType) {
            var type = EffectInfoBase.EffectTypeToType(effectType);
            return player.ReferenceHub.playerEffectsController.AllEffects.FirstOrDefault(e => e.GetType() == type);
        }

    }

}
