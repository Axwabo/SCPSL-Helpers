using System.Collections.Generic;
using System.Collections.ObjectModel;
using PlayerRoles.PlayableScps.Scp079;
using PlayerRoles.PlayableScps.Scp079.Cameras;
using PlayerRoles.PlayableScps.Scp079.Rewards;
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
        /// Attempts to get all main subroutines of SCP-079.
        /// </summary>
        /// <param name="role">The role to get the main subroutines from.</param>
        /// <param name="tierManager">The tier manager of SCP-079.</param>
        /// <param name="auxManager">The auxiliary power manager of SCP-079.</param>
        /// <param name="cameraSync">The camera sync of SCP-079.</param>
        /// <param name="signalHandler">The lost signal handler of SCP-079.</param>
        /// <param name="rewardManager">The reward manager of SCP-079.</param>
        /// <returns></returns>
        public static bool TryGetAll079Subroutines(Scp079Role role, out Scp079TierManager tierManager, out Scp079AuxManager auxManager, out Scp079CurrentCameraSync cameraSync, out Scp079LostSignalHandler signalHandler, out Scp079RewardManager rewardManager) {
            tierManager = null;
            auxManager = null;
            cameraSync = null;
            signalHandler = null;
            rewardManager = null;
            if (role == null)
                return false;
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
                }

            return propertiesSet == 5;
        }

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>The information about SCP-079.</returns>
        public static Scp079Info Get(Player player) {
            var role = player.Rm().CurrentRole as Scp079Role;
            if (!TryGetAll079Subroutines(role, out var tierManager, out var auxManager, out var cameraSync, out var signalHandler, out var rewardManager))
                return null;
            return new Scp079Info(tierManager.AccessTierIndex, auxManager.CurrentAux, cameraSync.CurrentCamera.SyncId, signalHandler.RemainingTime, rewardManager);
        }

        /// <summary>
        /// Checks if the given player is SCP-079.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-079.</returns>
        public static bool Is079(Player p) => p.Role() is Scp079Role;

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance.
        /// </summary>
        /// <param name="tier">The current tier/level of SCP-079.</param>
        /// <param name="auxiliaryPower">The current auxiliary power of SCP-079.</param>
        /// <param name="experience">The current experience of SCP-079.</param>
        /// <param name="lockdownCooldown">The cooldown until lockdown can be used again.</param>
        /// <param name="currentCamera">The camera that SCP-079 is using.</param>
        /// <param name="lockedDoors">The list of doors that SCP-079 has locked.</param>
        public Scp079Info(byte tier, float auxiliaryPower, float experience, float lockdownCooldown, Scp079Camera currentCamera, List<uint> lockedDoors) : base(Vector3.zero, Vector3.zero, Health079, -1, -1, null) {
            Tier = tier;
            AuxiliaryPower = auxiliaryPower;
            Experience = experience;
            LockdownCooldown = lockdownCooldown;
            CurrentCamera = currentCamera;
            LockedDoors = lockedDoors?.AsReadOnly();
        }

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
        /// The cooldown until lockdown can be used again.
        /// </summary>
        public float LockdownCooldown { get; }

        /// <summary>
        /// The camera that SCP-079 is using.
        /// </summary>
        public Scp079Camera CurrentCamera { get; }

        /// <summary>
        /// The list of doors that SCP-079 has locked.
        /// </summary>
        public ReadOnlyCollection<uint> LockedDoors { get; }

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            if (!player.IsConnected())
                return;
            if (!TryGetAll079Subroutines(player.Role() as Scp079Role, out var tierManager, out var auxManager, out var cameraSync, out var signalHandler, out var rewardManager))
                return;
            tierManager.SetProp(nameof(Scp079TierManager.AccessTierIndex), Tier);
            tierManager.TotalExp = Experience;
            auxManager.CurrentAux = AuxiliaryPower;
            cameraSync.SetProp(nameof(Scp079CurrentCameraSync.CurrentCamera), CurrentCamera);
        }

    }

}
