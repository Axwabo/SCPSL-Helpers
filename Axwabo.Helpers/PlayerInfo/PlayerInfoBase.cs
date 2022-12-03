using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        #region Obtainers

        private static readonly List<PlayerInfoObtainer> CustomObtainers = new();

        private static readonly PlayerInfoObtainer[] DefaultObtainers = {
            new(Scp079Info.Is079, Scp079Info.Get)
        };

        /// <summary>
        /// Registers a custom player info obtainer.
        /// </summary>
        /// <param name="check">The check to determine if the player matches the given role.</param>
        /// <param name="getter">A method to get the player info.</param>
        /// <param name="roleSetter">A method to set the player's role.</param>
        /// <returns>The id of the registered obtainer.</returns>
        public static uint RegisterCustomObtainer(PlayerCheck check, PlayerInfoGetter getter, PlayerRoleSetter roleSetter = null) {
            var obtainer = new PlayerInfoObtainer(check, getter, roleSetter);
            CustomObtainers.Add(obtainer);
            return obtainer.Id;
        }

        /// <summary>
        /// Unregisters a custom player info obtainer.
        /// </summary>
        /// <param name="id">The id of the obtainer to unregister.</param>
        /// <returns>Whether the obtainer was unregistered.</returns>
        public static bool UnregisterCustomObtainer(uint id) => CustomObtainers.RemoveAll(obtainer => obtainer.Id == id) > 0;

        #endregion

        #region Common Methods

        public static PlayerInfoObtainer GetFirstMatchingObtainer(Player player) {
            foreach (var obtainer in CustomObtainers.Where(e => e.IsValid))
                if (obtainer.Check(player))
                    return obtainer;
            foreach (var obtainer in DefaultObtainers.Where(e => e.IsValid))
                if (obtainer.Check(player))
                    return obtainer;
            return default;
        }

        /// <summary>
        /// Creates a <see cref="PlayerInfoBase"/> from a player based on the registered obtainers.
        /// </summary>
        /// <param name="player">The player to obtain the info from.</param>
        /// <returns>The player info. If no special obtainers were found, it will be a <see cref="StandardPlayerInfo"/>.</returns>
        /// <seealso cref="RegisterCustomObtainer"/>
        /// <seealso cref="UnregisterCustomObtainer"/>
        public static PlayerInfoBase CreateAutomatically(Player player) {
            var obtainer = GetFirstMatchingObtainer(player);
            return obtainer.IsValid ? obtainer.Get(player) : StandardPlayerInfo.Get(player);
        }

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

        /// <summary>
        /// Gets the list of active AHP processes for the given <paramref name="stat"/>.
        /// </summary>
        /// <param name="stat">The stat to get the active processes of.</param>
        /// <returns>The list of active processes.</returns>
        public static List<AhpStat.AhpProcess> GetProcesses(AhpStat stat) => stat.Get<List<AhpStat.AhpProcess>>("_activeProcesses");

        #endregion

        #region Properties

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

        #endregion

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
        /// Applies the gameplay data to the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to apply the data to.</param>
        public virtual void ApplyTo(Player player) {
            if (!player.IsConnected)
                return;
            player.ReferenceHub.playerMovementSync.OnPlayerClassChange(Position, new PlayerMovementSync.PlayerRotation(Rotation.x, Rotation.y));
            player.Health = Health;
            if (!(Ahp < 0))
                Timing.CallDelayed(0.2f, () => SetAhp(player, Ahp));
            foreach (var effect in Effects)
                effect?.ApplyTo(player);
        }

    }

}
