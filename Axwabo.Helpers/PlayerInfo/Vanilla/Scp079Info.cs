using System.Collections.Generic;
using System.Linq;
using Axwabo.Helpers.PlayerInfo.Containers;
using Exiled.API.Features;
using MapGeneration;
using Mirror;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo.Vanilla {

    /// <summary>
    /// Contains information about SCP-079.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    /// <seealso cref="PlayerInfoBase"/>
    /// <seealso cref="Scp079Role"/>
    public sealed class Scp079Info : PlayerInfoBase {

        private const float Health079 = 100000f;

        private static readonly BasicRoleInfo BasicScp079Info = new(Vector3.zero, Vector3.zero, Health079, -1, -1, -1, null, InventoryInfo.Empty);

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>The information about SCP-079.</returns>
        public static Scp079Info Get(Player player) {
            var routines = Scp079SubroutineContainer.Get(player.RoleAs<Scp079Role>());
            if (!routines.IsValid)
                return null;
            var time = NetworkTime.time;
            var zoneBlackout = routines.ZoneBlackout;
            var tierManager = routines.TierManager;
            return new Scp079Info(
                tierManager.TotalExp,
                routines.AuxManager.CurrentAux,
                routines.CurrentCameraSync.CurrentCamera,
                zoneBlackout._cooldownTimer,
                zoneBlackout._syncZone,
                routines.TeslaAbility._nextUseTime - time,
                GetMarkedRoomsDelta(routines.RewardManager._markedRooms),
                routines.LostSignalHandler._recoveryTime - time,
                routines.Map._state
            );
        }

        /// <summary>
        /// Checks if the given player is SCP-079.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-079.</returns>
        public static bool Is079(Player p) => p.RoleIs<Scp079Role>();

        private static Dictionary<RoomIdentifier, double> GetMarkedRoomsDelta(Dictionary<RoomIdentifier, double> marked) {
            var time = NetworkTime.time;
            return marked.ToDictionary(k => k.Key, v => time - v.Value);
        }

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance.
        /// </summary>
        /// <param name="experience">The current experience of SCP-079.</param>
        /// <param name="auxiliaryPower">The current auxiliary power of SCP-079.</param>
        /// <param name="currentCamera"></param>
        /// <param name="zoneBlackoutCooldown"></param>
        /// <param name="blackoutZone"></param>
        /// <param name="teslaAbilityNextUseTime"></param>
        /// <param name="rewardCooldown"></param>
        /// <param name="signalLossRecoveryTime"></param>
        /// <param name="mapOpen"></param>
        public Scp079Info(int experience,
            float auxiliaryPower,
            Scp079Camera currentCamera,
            CooldownInfo zoneBlackoutCooldown,
            FacilityZone blackoutZone,
            double teslaAbilityNextUseTime,
            Dictionary<RoomIdentifier, double> rewardCooldown,
            double signalLossRecoveryTime,
            bool mapOpen) : base(BasicScp079Info) {
            AuxiliaryPower = auxiliaryPower;
            Experience = experience;
            CurrentCamera = currentCamera;
            CameraRotation = new Vector2(CurrentCamera.HorizontalRotation, CurrentCamera.VerticalRotation);
            ZoneBlackoutCooldown = zoneBlackoutCooldown;
            BlackoutZone = blackoutZone;
            TeslaAbilityNextUseTime = teslaAbilityNextUseTime;
            RewardCooldown = rewardCooldown;
            SignalLossRecoveryTime = signalLossRecoveryTime;
            MapOpen = mapOpen;
        }

        #region Properties

        /// <summary>The current AP of SCP-079.</summary>
        public float AuxiliaryPower { get; }

        /// <summary>The current EXP of SCP-079.</summary>
        public int Experience { get; }

        /// <summary>The camera that SCP-079 is using.</summary>
        public Scp079Camera CurrentCamera { get; }

        public Vector2 CameraRotation { get; }

        public CooldownInfo ZoneBlackoutCooldown { get; }

        public FacilityZone BlackoutZone { get; }

        public double TeslaAbilityNextUseTime { get; }

        public Dictionary<RoomIdentifier, double> RewardCooldown { get; }

        public double SignalLossRecoveryTime { get; }

        public bool MapOpen { get; }

        #endregion

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            if (!player.IsConnected)
                return;
            var routines = Scp079SubroutineContainer.Get(player.RoleAs<Scp079Role>());
            if (!routines.IsValid)
                return;

            var time = NetworkTime.time;

            routines.TierManager.TotalExp = Experience;
            routines.AuxManager.CurrentAux = AuxiliaryPower;

            CurrentCamera.HorizontalRotation = CameraRotation.x;
            CurrentCamera.VerticalRotation = CameraRotation.y;
            routines.CurrentCameraSync.CurrentCamera = CurrentCamera;

            var zoneBlackout = routines.ZoneBlackout;
            zoneBlackout._syncZone = BlackoutZone;
            ZoneBlackoutCooldown.ApplyTo(zoneBlackout._cooldownTimer);

            var tesla = routines.TeslaAbility;
            tesla._nextUseTime = TeslaAbilityNextUseTime + time;

            var rewardManager = routines.RewardManager;
            var marked = rewardManager._markedRooms;
            foreach (var pair in RewardCooldown)
                marked[pair.Key] = pair.Value + time;

            var lostSignalHandler = routines.LostSignalHandler;
            lostSignalHandler._recoveryTime = SignalLossRecoveryTime + time;

            var map = routines.Map;
            map._state = MapOpen;

            routines.CameraRotationSync.Sync();
            tesla.Sync();
            if (BlackoutZone != FacilityZone.None)
                zoneBlackout.Sync();
            lostSignalHandler.Sync();
            map.Sync();
        }

    }

}
