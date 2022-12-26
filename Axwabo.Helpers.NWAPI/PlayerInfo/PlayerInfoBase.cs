using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Axwabo.Helpers.PlayerInfo.Effect;
using PlayerRoles.FirstPersonControl;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerStatsSystem;
using PluginAPI.Core;
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

        /// <summary>
        /// Gets the first matching info obtainer for the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the obtainer from.</param>
        /// <returns>The first matching obtainer, or <see cref="PlayerInfoObtainer.Empty"/> if none were found.</returns>
        public static PlayerInfoObtainer GetFirstMatchingObtainer(Player player) {
            foreach (var obtainer in CustomObtainers.Where(e => e.IsValid))
                if (obtainer.Check(player))
                    return obtainer;
            foreach (var obtainer in DefaultObtainers.Where(e => e.IsValid))
                if (obtainer.Check(player))
                    return obtainer;
            return PlayerInfoObtainer.Empty;
        }

        #endregion

        #region Common Methods

        /// <summary>
        /// Creates a <see cref="PlayerInfoBase"/> from a player based on the registered obtainers.
        /// </summary>
        /// <param name="player">The player to obtain the info from.</param>
        /// <returns>The player info. If no special obtainers were found, it will be a <see cref="StandardPlayerInfo"/>.</returns>
        /// <see cref="StandardPlayerInfo.Get"/>
        /// <seealso cref="RegisterCustomObtainer"/>
        /// <seealso cref="UnregisterCustomObtainer"/>
        /// <seealso cref="GetFirstMatchingObtainer"/>
        public static PlayerInfoBase CreateAutomatically(Player player) {
            var obtainer = GetFirstMatchingObtainer(player);
            return obtainer.IsValid ? obtainer.Get(player) : StandardPlayerInfo.Get(player);
        }

        /// <summary>
        /// Gets the Hume Shield value of the player, or -1 if the player is not currently playing as an <see cref="IHumeShieldedRole"/>.
        /// </summary>
        /// <param name="player">The player to get the HS value of.</param>
        /// <returns>The HS value, or -1 if there's no Hume Shield.</returns>
        protected static float GetHs(Player player) => player.Role() is not IHumeShieldedRole hs ? -1 : hs.HumeShieldModule.HsCurrent;

        /// <summary>
        /// Gets the AHP value of the player, or -1 if there are no currently active AHP processes.
        /// </summary>
        /// <param name="player">The player to get the AHP value of.</param>
        /// <returns>The AHP value, or -1 if there are no active processes.</returns>
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

        #endregion

        #region Properties

        /// <summary>
        /// The position of the player.
        /// </summary>
        public Vector3 Position { get; }

        /// <summary>
        /// The rotation of the player.
        /// </summary>
        public Vector3 Rotation { get; }

        /// <summary>
        /// The base HP of the player.
        /// </summary>
        public float Health { get; }

        /// <summary>
        /// The Hume Shield of the player.
        /// </summary>
        public float HumeShield { get; }

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
        /// <param name="humeShield"></param>
        /// <param name="ahp">The additional HP of the player.</param>
        /// <param name="effects">The effects of the player.</param>
        /// <seealso cref="EffectInfoBase"/>
        /// <seealso cref="StandardPlayerInfo"/>
        protected PlayerInfoBase(Vector3 position, Vector3 rotation, float health, float humeShield, float ahp, List<EffectInfoBase> effects) {
            Position = position;
            Rotation = rotation;
            Health = health;
            HumeShield = humeShield;
            Ahp = ahp;
            Effects = effects?.AsReadOnly();
        }

        /// <summary>
        /// Applies the gameplay data to the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to apply the data to.</param>
        public virtual void ApplyTo(Player player) {
            if (!player.IsConnected())
                return;
            player.ReferenceHub.TryOverridePosition(Position, Rotation - player.Rotation);
            player.Health = Health;
            var stats = player.ReferenceHub.playerStats.StatModules;
            if (Ahp >= 0)
                stats[1].CurValue = Ahp;
            stats[2].CurValue = HumeShield;
            foreach (var effect in Effects)
                effect?.ApplyTo(player);
        }

    }

}
