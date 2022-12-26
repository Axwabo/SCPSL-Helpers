using System.Collections.Generic;
using Axwabo.Helpers.PlayerInfo.Effect;
using PluginAPI.Core;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// An implementation of <see cref="PlayerInfoBase"/> for storing gameplay information about a player.
    /// </summary>
    /// <remarks>
    /// This class is used to avoid Get conflicts between different implementations.
    /// </remarks>
    public sealed class StandardPlayerInfo : PlayerInfoBase {

        /// <summary>
        /// Creates a <see cref="StandardPlayerInfo"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>A <see cref="StandardPlayerInfo"/> instance.</returns>
        public static StandardPlayerInfo Get(Player player) => new(
            player.Position,
            player.Rotation,
            player.Health,
            GetHs(player),
            GetAhp(player),
            EffectInfoBase.EffectsToList(player.ReferenceHub.playerEffectsController.AllEffects)
        );

        /// <summary>
        /// Creates a <see cref="StandardPlayerInfo"/> instance.
        /// </summary>
        /// <param name="pos">The position of the player.</param>
        /// <param name="rot">The rotation of the player.</param>
        /// <param name="health">The base HP of the player.</param>
        /// <param name="humeShield">The Hume Shield of the player.</param>
        /// <param name="ahp">The additional HP of the player.</param>
        /// <param name="effects">The effects of the player.</param>
        public StandardPlayerInfo(Vector3 pos, Vector3 rot, float health, float humeShield, float ahp, List<EffectInfoBase> effects) : base(pos, rot, health, humeShield,ahp, effects) {
        }

    }

}
