using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Rewards;

namespace Axwabo.Helpers.PlayerInfo {

    public readonly struct Scp079SubroutineContainer {

        public static readonly Scp079SubroutineContainer Empty = new();

        public readonly Scp079TierManager TierManager;
        public readonly Scp079BlackoutZoneAbility ZoneBlackout;
        public readonly Scp079AuxManager AuxManager;
        public readonly Scp079CurrentCameraSync CurrentCameraSync;
        public readonly Scp079LostSignalHandler LostSignalHandler;
        public readonly Scp079RewardManager RewardManager;

        public readonly bool IsValid;

        public Scp079SubroutineContainer(Scp079TierManager tierManager, Scp079BlackoutZoneAbility zoneBlackout, Scp079CurrentCameraSync currentCameraSync, Scp079AuxManager auxManager, Scp079LostSignalHandler lostSignalHandler, Scp079RewardManager rewardManager) {
            TierManager = tierManager;
            ZoneBlackout = zoneBlackout;
            CurrentCameraSync = currentCameraSync;
            AuxManager = auxManager;
            LostSignalHandler = lostSignalHandler;
            RewardManager = rewardManager;
            IsValid = true;
        }

        /// <summary>
        /// Attempts to get all main subroutines of SCP-079.
        /// </summary>
        /// <param name="role">The role to get the main subroutines from.</param>
        /// <param name="tierManager">The tier manager of SCP-079.</param>
        /// <param name="auxManager">The auxiliary power manager of SCP-079.</param>
        /// <param name="cameraSync">The camera sync of SCP-079.</param>
        /// <param name="signalHandler">The lost signal handler of SCP-079.</param>
        /// <param name="rewardManager">The reward manager of SCP-079.</param>
        /// <returns></returns>
        public static Scp079SubroutineContainer Get(Scp079Role role) {
            Scp079TierManager tierManager = null;
            Scp079AuxManager auxManager = null;
            Scp079CurrentCameraSync cameraSync = null;
            Scp079LostSignalHandler signalHandler = null;
            Scp079RewardManager rewardManager = null;
            Scp079BlackoutZoneAbility zoneBlackout = null;
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
                }

            return propertiesSet != 6
                ? Empty
                : new Scp079SubroutineContainer(
                    tierManager,
                    zoneBlackout,
                    cameraSync,
                    auxManager,
                    signalHandler,
                    rewardManager
                );
        }

    }

}
