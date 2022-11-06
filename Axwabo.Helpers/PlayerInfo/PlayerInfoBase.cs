using System.Collections.Generic;
using System.Collections.ObjectModel;
using Axwabo.Helpers.PlayerInfo.Effect;
using Exiled.API.Features;
using MEC;
using PlayableScps.Interfaces;
using PlayerStatsSystem;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// A base class for storing gameplay information about a player.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    public abstract class PlayerInfoBase {

        /// <summary>
        /// Gets the AHP value of the player, or -1 if there are no currently active AHP processes.
        /// </summary>
        /// <param name="player">The player to get the AHP value of.</param>
        /// <returns>The AHP value of, or -1 if there are no active processes.</returns>
        protected static float GetAhp(Player player) {
            var ahp = (AhpStat) player.ReferenceHub.playerStats.StatModules[1];
            return GetProcesses(ahp).Count is 0 ? -1 : ahp.CurValue;
        }

        /// <summary>
        /// Gets the list of active AHP processes for the given <paramref name="stat"/>.
        /// </summary>
        /// <param name="stat">The stat to get the active processes of.</param>
        /// <returns>The list of active processes.</returns>
        public static List<AhpStat.AhpProcess> GetProcesses(AhpStat stat) => stat.Get<List<AhpStat.AhpProcess>>("_activeProcesses");

        /// <summary>
        /// Creates a base object for storing information about a player.
        /// </summary>
        /// <param name="position">The position of the player.</param>
        /// <param name="rotation">The rotation of the player.</param>
        /// <param name="health">The base HP of the player.</param>
        /// <param name="ahp">The additional HP of the player.</param>
        /// <param name="effects">The effects of the player.</param>
        /// <seealso cref="EffectInfoBase"/>
        /// <seealso cref="StandardPlayerInfo"/>
        protected PlayerInfoBase(Vector3 position, Vector2 rotation, float health, float ahp, List<EffectInfoBase> effects) {
            Position = position;
            Rotation = rotation;
            Health = health;
            Ahp = ahp;
            Effects = effects?.AsReadOnly();
        }

        /// <summary>
        /// The position of the player.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// The rotation of the player.
        /// </summary>
        public Vector2 Rotation { get; }

        /// <summary>
        /// The base HP of the player.
        /// </summary>
        public float Health { get; }

        /// <summary>
        /// The additional HP of the player.
        /// </summary>
        public float Ahp { get; }

        /// <summary>
        /// The effects of the player.
        /// </summary>
        /// <seealso cref="EffectInfoBase"/>
        public ReadOnlyCollection<EffectInfoBase> Effects { get; }

        /// <summary>
        /// Applies the gameplay data to the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to apply the data to.</param>
        public virtual void ApplyTo(Player player) {
            player.ReferenceHub.playerMovementSync.OnPlayerClassChange(Position, new PlayerMovementSync.PlayerRotation(Rotation.x, Rotation.y));
            player.Health = Health;
            if (!(Ahp < 0))
                Timing.CallDelayed(0.2f, () => SetAhp(player, Ahp));

            foreach (var effect in Effects)
                effect?.ApplyTo(player);
        }

        /// <summary>
        /// Sets the AHP or Hume Shield of the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to set the AHP of.</param>
        /// <param name="ahp">The AHP to set.</param>
        protected static void SetAhp(Player player, float ahp) {
            if (!player.IsConnected)
                return;
            if (player.ReferenceHub.scpsController is {CurrentScp: IShielded {Shield: var shield}})
                shield.CurrentAmount = Mathf.Min(ahp, shield.Limit);
            else
                player.ArtificialHealth = ahp;
        }

    }

}
