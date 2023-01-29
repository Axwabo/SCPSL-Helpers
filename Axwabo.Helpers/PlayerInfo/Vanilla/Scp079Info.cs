using System.Collections.Generic;
using System.Linq;
using Axwabo.Helpers.PlayerInfo.Containers;
using Exiled.API.Features;
using Interactables.Interobjects.DoorUtils;
using MapGeneration;
using Mirror;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Rewards;
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
            var doorLock = routines.DoorLock;
            var lockdown = routines.LockdownAbility;
            return new Scp079Info(
                tierManager.TotalExp,
                routines.AuxManager.CurrentAux,
                routines.CurrentCameraSync.CurrentCamera,
                zoneBlackout._cooldownTimer,
                zoneBlackout._syncZone,
                routines.TeslaAbility._nextUseTime - time,
                GetMarkedRoomsDelta(routines.RewardManager._markedRooms),
                routines.LostSignalHandler._recoveryTime - time,
                routines.Map._state,
                doorLock._lockedDoors.ToArray(),
                GetToggleDelta(doorLock._toggleTimes),
                lockdown._lockdownInEffect,
                lockdown._nextUseTime - time,
                lockdown._doorsToLockDown.ToArray()
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

        private static Dictionary<DoorVariant, double> GetToggleDelta(Dictionary<DoorVariant, double> toggleTimes) {
            var time = NetworkTime.time;
            return toggleTimes.ToDictionary(k => k.Key, v => time - v.Value);
        }

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance.
        /// </summary>
        /// <param name="experience">The current experience of SCP-079.</param>
        /// <param name="auxiliaryPower">The current auxiliary power of SCP-079.</param>
        /// <param name="currentCamera">The camera SCP-079 is using.</param>
        /// <param name="zoneBlackoutCooldown">Cooldown of the "Zone-Wide Blackout" ability.</param>
        /// <param name="blackoutZone">The currently blacked out zone.</param>
        /// <param name="teslaAbilityCooldown">The cooldown until SCP-079 can overcharge a Tesla Gate.</param>
        /// <param name="rewardCooldown">Rooms marked by the <see cref="Scp079RewardManager"/>.</param>
        /// <param name="signalLossRecoveryTime">Time when SCP-079 will regain control of the cameras.</param>
        /// <param name="mapOpen">If SCP-079 has opened the Facility map.</param>
        /// <param name="lockedDoors">The doors currently locked by SCP-079.</param>
        /// <param name="toggleTimes">The cooldown until the specified doors can be locked again.</param>
        /// <param name="lockdownActive">Whether the Lockdown ability is currently active.</param>
        /// <param name="lockdownCooldown">The cooldown until SCP-079 can use the Lockdown ability.</param>
        /// <param name="doorsToLock">The doors that will be locked by the Lockdown ability.</param>
        public Scp079Info(int experience,
            float auxiliaryPower,
            Scp079Camera currentCamera,
            CooldownInfo zoneBlackoutCooldown,
            FacilityZone blackoutZone,
            double teslaAbilityCooldown,
            Dictionary<RoomIdentifier, double> rewardCooldown,
            double signalLossRecoveryTime,
            bool mapOpen,
            DoorVariant[] lockedDoors,
            Dictionary<DoorVariant, double> toggleTimes,
            bool lockdownActive,
            double lockdownCooldown,
            DoorVariant[] doorsToLock) : base(BasicScp079Info) {
            AuxiliaryPower = auxiliaryPower;
            Experience = experience;
            CurrentCamera = currentCamera;
            CameraRotation = new Vector2(CurrentCamera.HorizontalRotation, CurrentCamera.VerticalRotation);
            ZoneBlackoutCooldown = zoneBlackoutCooldown;
            BlackoutZone = blackoutZone;
            TeslaAbilityCooldown = teslaAbilityCooldown;
            RewardCooldown = rewardCooldown;
            SignalLossRecoveryTime = signalLossRecoveryTime;
            MapOpen = mapOpen;
            LockedDoors = lockedDoors;
            ToggleTimes = toggleTimes;
            LockdownActive = lockdownActive;
            LockdownCooldown = lockdownCooldown;
            DoorsToLock = doorsToLock;
        }

        #region Properties

        /// <summary>The current AP of SCP-079.</summary>
        public float AuxiliaryPower { get; }

        /// <summary>The current EXP of SCP-079.</summary>
        public int Experience { get; }

        /// <summary>The camera that SCP-079 is using.</summary>
        public Scp079Camera CurrentCamera { get; }

        /// <summary>The rotation of the current camera.</summary>
        public Vector2 CameraRotation { get; }

        /// <summary>The cooldown of the "Zone-Wide Blackout" ability.</summary>
        public CooldownInfo ZoneBlackoutCooldown { get; }

        /// <summary>The currently blacked out zone.</summary>
        public FacilityZone BlackoutZone { get; }

        /// <summary>The cooldown until SCP-079 can overcharge a Tesla Gate.</summary>
        public double TeslaAbilityCooldown { get; }

        /// <summary>The rooms marked by the <see cref="Scp079RewardManager"/>.</summary>
        public Dictionary<RoomIdentifier, double> RewardCooldown { get; }

        /// <summary>The time when SCP-079 will regain control of the cameras.</summary>
        public double SignalLossRecoveryTime { get; }

        /// <summary>If SCP-079 has opened the Facility map.</summary>
        public bool MapOpen { get; }

        /// <summary>The doors currently locked by SCP-079.</summary>
        public DoorVariant[] LockedDoors { get; }

        /// <summary>The cooldown until the specified doors can be locked again.</summary>
        public Dictionary<DoorVariant, double> ToggleTimes { get; }

        /// <summary>Whether the Lockdown ability is currently active.</summary>
        public bool LockdownActive { get; }

        /// <summary>The cooldown until SCP-079 can use the Lockdown ability.</summary>
        public double LockdownCooldown { get; }

        /// <summary>The doors that will be locked by the Lockdown ability.</summary>
        public DoorVariant[] DoorsToLock { get; }

        #endregion

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            if (!player.IsConnected)
                return;
            var routines = Scp079SubroutineContainer.Get(player.RoleAs<Scp079Role>());
            if (!routines.IsValid)
                return;

            var time = NetworkTime.time;

            SetCameraProperties(routines);

            var zoneBlackout = routines.ZoneBlackout;
            zoneBlackout._syncZone = BlackoutZone;
            ZoneBlackoutCooldown.ApplyTo(zoneBlackout._cooldownTimer);

            var tesla = routines.TeslaAbility;
            tesla._nextUseTime = TeslaAbilityCooldown + time;

            var rewardManager = routines.RewardManager;
            SetRoomRewards(rewardManager, time);

            var lostSignalHandler = routines.LostSignalHandler;
            lostSignalHandler._recoveryTime = SignalLossRecoveryTime + time;

            var map = routines.Map;
            map._state = MapOpen;

            var doorLock = routines.DoorLock;
            var lockdown = routines.LockdownAbility;
            SetLocks(doorLock, lockdown, time);

            routines.CameraRotationSync.Sync();
            tesla.Sync();
            lostSignalHandler.Sync();
            map.Sync();
            if (BlackoutZone != FacilityZone.None)
                zoneBlackout.Sync();
            doorLock.Sync();
            lockdown.Sync();
        }

        private void SetCameraProperties(Scp079SubroutineContainer routines) {
            routines.TierManager.TotalExp = Experience;
            routines.AuxManager.CurrentAux = AuxiliaryPower;

            CurrentCamera.HorizontalRotation = CameraRotation.x;
            CurrentCamera.VerticalRotation = CameraRotation.y;
            routines.CurrentCameraSync.CurrentCamera = CurrentCamera;
        }

        private void SetRoomRewards(Scp079RewardManager rewardManager, double time) {
            var marked = rewardManager._markedRooms;
            foreach (var pair in RewardCooldown.AsNonNullEnumerable())
                marked[pair.Key] = pair.Value + time;
        }

        private void SetLocks(Scp079DoorLockChanger doorLock, Scp079LockdownRoomAbility lockdown, double time) {
            SetCurrentlyLockedDoors(doorLock._lockedDoors, LockedDoors, DoorLockReason.Regular079);
            var toggleTimes = doorLock._toggleTimes;
            foreach (var pair in ToggleTimes.AsNonNullEnumerable())
                toggleTimes[pair.Key] = pair.Value + time;

            lockdown._lockdownInEffect = LockdownActive;
            lockdown._nextUseTime = LockdownCooldown + time;
            SetCurrentlyLockedDoors(lockdown._doorsToLockDown, DoorsToLock, DoorLockReason.Lockdown079);
        }

        private static void SetCurrentlyLockedDoors(HashSet<DoorVariant> locked, DoorVariant[] newDoors, DoorLockReason flag) {
            if (newDoors == null)
                return;
            foreach (var door in locked)
                door.ServerChangeLock(flag, false);
            locked.Clear();

            foreach (var door in newDoors) {
                locked.Add(door);
                door.ServerChangeLock(flag, true);
            }
        }

    }

}
