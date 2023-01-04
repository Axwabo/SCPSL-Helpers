﻿using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Rewards;

namespace Axwabo.Helpers.PlayerInfo.Containers {

    public readonly struct Scp079SubroutineContainer : IIsValid {

        public static readonly Scp079SubroutineContainer Empty = new();

        public readonly Scp079TierManager TierManager;
        public readonly Scp079BlackoutZoneAbility ZoneBlackout;
        public readonly Scp079AuxManager AuxManager;
        public readonly Scp079CurrentCameraSync CurrentCameraSync;
        public readonly Scp079LostSignalHandler LostSignalHandler;
        public readonly Scp079RewardManager RewardManager;
        public readonly Scp079TeslaAbility TeslaAbility;

        public bool IsValid { get; }

        public Scp079SubroutineContainer(Scp079TierManager tierManager, Scp079BlackoutZoneAbility zoneBlackout, Scp079CurrentCameraSync currentCameraSync, Scp079AuxManager auxManager, Scp079LostSignalHandler lostSignalHandler, Scp079RewardManager rewardManager, Scp079TeslaAbility teslaAbility) {
            TierManager = tierManager;
            ZoneBlackout = zoneBlackout;
            CurrentCameraSync = currentCameraSync;
            AuxManager = auxManager;
            LostSignalHandler = lostSignalHandler;
            RewardManager = rewardManager;
            TeslaAbility = teslaAbility;
            IsValid = true;
        }

        /// <summary>
        /// Gets all main subroutines of SCP-079.
        /// </summary>
        /// <param name="role">The role to get the main subroutines from.</param>
        /// <returns>An <see cref="Scp079SubroutineContainer"/> containing the subroutines.</returns>
        public static Scp079SubroutineContainer Get(Scp079Role role) {
            Scp079TierManager tierManager = null;
            Scp079AuxManager auxManager = null;
            Scp079CurrentCameraSync cameraSync = null;
            Scp079LostSignalHandler signalHandler = null;
            Scp079RewardManager rewardManager = null;
            Scp079BlackoutZoneAbility zoneBlackout = null;
            Scp079TeslaAbility tesla = null;
            if (role == null)
                return Empty;
            var propertiesSet = 0;
            foreach (var sub in role.SubroutineModule.AllSubroutines)
                switch (sub) {
                    case Scp079TierManager t:
                        tierManager = t;
                        propertiesSet++;
                        break;
                    case Scp079AuxManager a:
                        auxManager = a;
                        propertiesSet++;
                        break;
                    case Scp079CurrentCameraSync sync:
                        cameraSync = sync;
                        propertiesSet++;
                        break;
                    case Scp079LostSignalHandler signal:
                        signalHandler = signal;
                        propertiesSet++;
                        break;
                    case Scp079RewardManager r:
                        rewardManager = r;
                        propertiesSet++;
                        break;
                    case Scp079BlackoutZoneAbility bz:
                        zoneBlackout = bz;
                        propertiesSet++;
                        break;
                    case Scp079TeslaAbility t:
                        tesla = t;
                        propertiesSet++;
                        break;
                }

            return propertiesSet != 7
                ? Empty
                : new Scp079SubroutineContainer(
                    tierManager,
                    zoneBlackout,
                    cameraSync,
                    auxManager,
                    signalHandler,
                    rewardManager,
                    tesla
                );
        }

    }

}