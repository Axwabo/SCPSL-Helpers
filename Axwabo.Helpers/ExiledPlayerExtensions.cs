using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CommandSystem;
using Exiled.API.Features;
using HarmonyLib;
using PlayerStatsSystem;
using Utils;

namespace Axwabo.Helpers {

    /// <summary>
    /// Extension methods for the EXILED <see cref="Player"/> API.
    /// </summary>
    public static class ExiledPlayerExtensions {

        private static readonly MethodInfo DeathScreenAttacker = AccessTools.Method(typeof(PlayerStats), "TargetReceiveAttackerDeathReason");
        private static readonly MethodInfo DeathScreenSpecific = AccessTools.Method(typeof(PlayerStats), "TargetReceiveSpecificDeathReason");

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
        public static bool IsActuallyScp(this Player player) => player.ReferenceHub.characterClassManager.CurRole.fullName.Contains("SCP-");

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
        /// Gets the current <see cref="Role">vanilla role</see> of the player.
        /// </summary>
        /// <param name="player">The player to get the role of.</param>
        /// <returns></returns>
        /// <remarks>It does not supply an <see cref="Exiled.API.Features.Roles.Role">EXILED role</see>.</remarks>
        public static Role VanillaRole(this Player player) => player.Ccm().CurRole;

        /// <summary>
        /// Puts the player to spectator without spawning a ragdoll.
        /// </summary>
        /// <param name="player">The player to kill.</param>
        /// <param name="handler">The death reason to show to the player.</param>
        public static void ForceClassToSpectator(this Player player, DamageHandlerBase handler) {
            var hub = player.ReferenceHub;
            var ccm = hub.characterClassManager;
            ccm.SetClassID(RoleType.Spectator, CharacterClassManager.SpawnReason.Died);
            if (handler == null)
                return;
            ccm.TargetConsolePrint(ccm.connectionToClient, "You died. Reason: " + handler.ServerLogsText, "yellow");
            if (handler is AttackerDamageHandler {Attacker: var attacker})
                DeathScreenAttacker.Invoke(hub.playerStats, new object[] {attacker.Nickname, attacker.Role});
            else
                DeathScreenSpecific.Invoke(hub.playerStats, new object[] {handler});
        }

        /// <summary>
        /// Puts the player to spectator without spawning a ragdoll.
        /// </summary>
        /// <param name="player">The player to kill.</param>
        /// <param name="reason">The death reason to show to the player.</param>
        public static void ForceClassToSpectator(this Player player, string reason = null) {
            var hub = player.ReferenceHub;
            var ccm = hub.characterClassManager;
            ccm.SetClassID(RoleType.Spectator, CharacterClassManager.SpawnReason.Died);
            if (string.IsNullOrEmpty(reason))
                return;
            ccm.TargetConsolePrint(ccm.connectionToClient, "You died. Reason: " + reason, "yellow");
            DeathScreenSpecific.Invoke(hub.playerStats, new object[] {new CustomReasonDamageHandler(reason)});
        }

    }

}
