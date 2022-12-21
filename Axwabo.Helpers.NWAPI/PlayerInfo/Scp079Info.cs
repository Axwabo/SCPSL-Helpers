using System.Collections.Generic;
using System.Collections.ObjectModel;
using PluginAPI.Core;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo {

    /// <summary>
    /// Contains information about SCP-079.
    /// </summary>
    /// <seealso cref="StandardPlayerInfo"/>
    /// <seealso cref="PlayerInfoBase"/>
    /// <seealso cref="Scp079PlayerScript"/>
    public sealed class Scp079Info : PlayerInfoBase {

        private const float Health079 = 100000f;

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance using the given <paramref name="player"/>.
        /// </summary>
        /// <param name="player">The player to get the information from.</param>
        /// <returns>The information about SCP-079.</returns>
        public static Scp079Info Get(Player player) {
            var script = player.ReferenceHub.scp079PlayerScript;
            return script == null || !script.iAm079 ? null : new Scp079Info(script.Lvl, script.Mana, script.Exp, script.CurrentLDCooldown, script.currentCamera, script.lockedDoors.ToList());
        }

        /// <summary>
        /// Checks if the given player is SCP-079.
        /// </summary>
        /// <param name="p">The player to check.</param>
        /// <returns>Whether the given player is SCP-079.</returns>
        public static bool Is079(Player p) => p.Ccm().CurRole is {roleId: RoleType.Scp079};

        /// <summary>
        /// Creates an <see cref="Scp079Info"/> instance.
        /// </summary>
        /// <param name="tier">The current tier/level of SCP-079.</param>
        /// <param name="auxiliaryPower">The current auxiliary power of SCP-079.</param>
        /// <param name="experience">The current experience of SCP-079.</param>
        /// <param name="lockdownCooldown">The cooldown until lockdown can be used again.</param>
        /// <param name="currentCamera">The camera that SCP-079 is using.</param>
        /// <param name="lockedDoors">The list of doors that SCP-079 has locked.</param>
        public Scp079Info(byte tier, float auxiliaryPower, float experience, float lockdownCooldown, Camera079 currentCamera, List<uint> lockedDoors) : base(Vector3.zero, Vector2.zero, Health079, -1, null) {
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
        public byte Tier { get; }

        /// <summary>
        /// The current AP of SCP-079.
        /// </summary>
        public float AuxiliaryPower { get; }

        /// <summary>
        /// The current EXP of SCP-079.
        /// </summary>
        public float Experience { get; }

        /// <summary>
        /// The cooldown until lockdown can be used again.
        /// </summary>
        public float LockdownCooldown { get; }

        /// <summary>
        /// The camera that SCP-079 is using.
        /// </summary>
        public Camera079 CurrentCamera { get; }

        /// <summary>
        /// The list of doors that SCP-079 has locked.
        /// </summary>
        public ReadOnlyCollection<uint> LockedDoors { get; }

        /// <inheritdoc />
        public override void ApplyTo(Player player) {
            if (!player.IsConnected)
                return;
            var script = player.ReferenceHub.scp079PlayerScript;
            script.Network_curLvl = Tier;
            script.Network_curMana = AuxiliaryPower;
            script.Network_curExp = Experience;
            script.CurrentLDCooldown = LockdownCooldown;
            if (LockedDoors != null) {
                script.lockedDoors.Clear();
                script.lockedDoors.AddRange(LockedDoors);
            }

            script.Call("RpcSwitchCamera", CurrentCamera.cameraId, false);
        }

    }

}
