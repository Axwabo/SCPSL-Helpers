using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Exiled.API.Features;
using UnityEngine;

namespace Axwabo.Helpers.PlayerInfo {

    public sealed class Scp079Info : PlayerInfoBase {

        public static Scp079Info Get(Player player) {
            var script = player.ReferenceHub.scp079PlayerScript;
            return script == null || !script.iAm079 ? null : new Scp079Info(script.Lvl, script.Mana, script.Exp, script.currentCamera, script.lockedDoors.ToList());
        }

        public Scp079Info(byte tier, float auxiliaryPower, float experience, Camera079 currentCamera, List<uint> lockedDoors) : base(Vector3.zero, Vector2.zero, 100f, -1, null) {
            Tier = tier;
            AuxiliaryPower = auxiliaryPower;
            Experience = experience;
            CurrentCamera = currentCamera;
            LockedDoors = lockedDoors.AsReadOnly();
        }

        public byte Tier { get; }

        public float AuxiliaryPower { get; }

        public float Experience { get; }

        public Camera079 CurrentCamera { get; }

        public ReadOnlyCollection<uint> LockedDoors { get; }

        public override void ApplyTo(Player player) {
            var script = player.ReferenceHub.scp079PlayerScript;
            script.Network_curLvl = Tier;
            script.Network_curMana = AuxiliaryPower;
            script.Network_curExp = Experience;
            script.lockedDoors.Clear();
            script.lockedDoors.AddRange(LockedDoors);
            script.Call("RpcSwitchCamera", CurrentCamera.cameraId, false);
        }

    }

}
