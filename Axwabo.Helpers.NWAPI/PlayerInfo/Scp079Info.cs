using System.Collections.Generic;
using System.Linq;
using MapGeneration;
using Mirror;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PluginAPI.Core;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// Contains information about SCP-079.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    /// <seealso cref="PlayerInfoBase"/>
    /// <seealso cref="Scp079Role"/>
    public sealed class Scp079Info : PlayerInfoBase {

        private const float Health079 = 100000f;

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>The information about SCP-079.</returns>
        public static Scp079Info Get(Player player) {
            var routines = Scp079SubroutineContainer.Get(player.RoleAs<Scp079Role>());
            if (!routines.IsValid)
                return null;
            var zoneBlackout = routines.ZoneBlackout;
            return new Scp079Info(
                routines.TierManager.AccessTierIndex,
                routines.AuxManager.CurrentAux,
                routines.CurrentCameraSync.CurrentCamera.SyncId,
                routines.CurrentCameraSync.CurrentCamera,
                zoneBlackout._cooldownTimer,
                zoneBlackout._syncZone
            );
        }

        /// <summary>
        /// Checks if the given player is SCP-079.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-079.</returns>
        public static bool Is079(Player p) => p.RoleIs<Scp079Role>();

        private static Dictionary<RoomIdentifier, double> GetMarkedRoomsDelta(Dictionary<RoomIdentifier, double> marked) {
            var networkTime = NetworkTime.time;
            return marked.ToDictionary(k => k.Key, v => networkTime - v.Value);
        }

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance.
        /// </summary>
        /// <param name="tier">The current tier/level of SCP-079.</param>
        /// <param name="auxiliaryPower">The current auxiliary power of SCP-079.</param>
        /// <param name="experience">The current experience of SCP-079.</param>
        /// <param name="currentCamera"></param>
        /// <param name="zoneBlackoutCooldown"></param>
        /// <param name="blackoutZone"></param>
        public Scp079Info(int tier, float auxiliaryPower, int experience, Scp079Camera currentCamera, CooldownInfo zoneBlackoutCooldown, FacilityZone blackoutZone) : base(Vector3.zero, Vector3.zero, Health079, -1, -1, null) {
            Tier = tier;
            AuxiliaryPower = auxiliaryPower;
            Experience = experience;
            CurrentCamera = currentCamera;
            ZoneBlackoutCooldown = zoneBlackoutCooldown;
            BlackoutZone = blackoutZone;
        }

        #region Properties

        /// <summary>
        /// The current tier/level of SCP-079.
        /// </summary>
        public int Tier { get; }

        /// <summary>
        /// The current AP of SCP-079.
        /// </summary>
        public float AuxiliaryPower { get; }

        /// <summary>
        /// The current EXP of SCP-079.
        /// </summary>
        public int Experience { get; }

        /// <summary>
        /// The camera that SCP-079 is using.
        /// </summary>
        public Scp079Camera CurrentCamera { get; }

        public CooldownInfo ZoneBlackoutCooldown { get; }
        
        public FacilityZone BlackoutZone { get; }

        #endregion

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            if (!player.IsConnected())
                return;
            var container = Scp079SubroutineContainer.Get(player.Role() as Scp079Role);
            if (!container.IsValid)
                return;
            container.TierManager.AccessTierIndex = Tier;
            container.TierManager.TotalExp = Experience;
            container.AuxManager.CurrentAux = AuxiliaryPower;
            container.CurrentCameraSync.CurrentCamera = CurrentCamera;
            var zoneBlackout = container.ZoneBlackout;
            zoneBlackout._syncZone = BlackoutZone;
            ZoneBlackoutCooldown.ApplyTo(zoneBlackout._cooldownTimer);
        }

    }

}
