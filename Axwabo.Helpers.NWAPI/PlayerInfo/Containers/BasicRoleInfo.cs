using System.Collections.Generic;
using Axwabo.Helpers.PlayerInfo.Effect;
using PlayerRoles.PlayableScps.HumeShield;
using PlayerStatsSystem;
using PluginAPI.Core;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct BasicRoleInfo : IIsValid {

        #region Common Static

        public const int AhpIndex = 1;
        public const int StaminaIndex = 2;
        public const int HumeShieldIndex = 4;

        /// <summary>
        /// Gets the Hume Shield value of the player, or -1 if the player is not currently playing as an <see cref="IHumeShieldedRole"/>.
        /// </summary>
        /// <param name="player">The player to get the HS value of.</param>
        /// <returns>The HS value, or -1 if there's no Hume Shield.</returns>
        public static float GetHs(Player player) => player.Role() is not IHumeShieldedRole hs ? -1 : hs.HumeShieldModule.HsCurrent;

        /// <summary>
        /// Gets the AHP value of the player, or -1 if there are no currently active AHP processes.
        /// </summary>
        /// <param name="player">The player to get the AHP value of.</param>
        /// <returns>The AHP value, or -1 if there are no active processes.</returns>
        public static float GetAhp(Player player) {
            var ahp = (AhpStat) player.ReferenceHub.playerStats.StatModules[AhpIndex];
            return PlayerInfoBase.GetProcesses(ahp).Count is 0 ? -1 : ahp.CurValue;
        }

        /// <summary>
        /// Gets the stamina amount of the player.
        /// </summary>
        /// <param name="player">The player to get the stamina of.</param>
        /// <returns>The stamina amount.</returns>
        public static float GetStamina(Player player) => ((StaminaStat) player.ReferenceHub.playerStats.StatModules[StaminaIndex]).CurValue;

        #endregion

        public static BasicRoleInfo Get(Player player) => new(
            player.Position,
            player.Rotation,
            player.Health,
            GetAhp(player),
            GetStamina(player),
            GetHs(player),
            EffectInfoBase.EffectsToList(player.ReferenceHub.playerEffectsController.AllEffects)
        );

        public BasicRoleInfo(Vector3 position, Vector3 rotation, float health, float ahp, float stamina, float humeShield, List<EffectInfoBase> effects) {
            Position = position;
            Rotation = rotation;
            Health = health;
            Ahp = ahp;
            Stamina = stamina;
            HumeShield = humeShield;
            Effects = effects;
            IsValid = true;
        }

        #region Members

        public readonly Vector3 Position;

        public readonly Vector3 Rotation;

        public readonly float Health;

        public readonly float Ahp;

        public readonly float Stamina;

        public readonly float HumeShield;

        public readonly List<EffectInfoBase> Effects;

        public bool IsValid { get; }

        #endregion

    }

}
