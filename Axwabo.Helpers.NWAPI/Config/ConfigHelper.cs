using System.Linq;
using MapGeneration;
using UnityEngine;

namespace Axwabo.Helpers.Config {

    /// <summary>
    /// Helper methods for custom config structs.
    /// </summary>
    public static class ConfigHelper {

        public static RoomIdentifier[] Rooms => Object.FindObjectsOfType<RoomIdentifier>();

        /// <summary>
        /// Gets the position and rotation based on an offset and a transform object. 
        /// </summary>
        /// <param name="transform">The transform to calculate the offset from.</param>
        /// <param name="positionOffset">A world-space position offset.</param>
        /// <param name="rotationOffset">A world-space rotation offset.</param>
        /// <param name="position">The calculated position.</param>
        /// <param name="rotation">The calculated rotation.</param>
        /// <returns>Whether the transform object was not null.</returns>
        public static bool TryTransformOffset(this Transform transform, Vector3 positionOffset, Quaternion rotationOffset, out Vector3 position, out Quaternion rotation) {
            if (transform == null) {
                position = Vector3.zero;
                rotation = Quaternion.identity;
                return false;
            }

            position = transform.TransformPoint(positionOffset);
            rotation = transform.rotation * rotationOffset;
            return true;
        }

        /// <summary>
        /// Gets a room by the name of its GameObject.
        /// </summary>
        /// <param name="name">The name of the room.</param>
        /// <returns>The room, or null if it couldn't be found.</returns>
        public static RoomIdentifier GetRoomByRoomName(string name) {
            name = name.ToLowerInvariant();
            return string.IsNullOrEmpty(name) ? null : Rooms.FirstOrDefault(r => r.gameObject.name.ToLowerInvariant().Contains(name));
        }

        /// <summary>
        /// Gets the transform of the component without throwing a NullReferenceException.
        /// </summary>
        /// <param name="o">The component to get the transform from.</param>
        /// <returns>The transform of the component or null.</returns>
        public static Transform SafeGetTransform(this Component o) => o == null ? null : o.transform;

        /// <summary>
        /// Gets the transform of the object without throwing a NullReferenceException.
        /// </summary>
        /// <param name="o">The object to get the transform from.</param>
        /// <returns>The transform of the object or null.</returns>
        public static Transform SafeGetTransform(this GameObject o) => o == null ? null : o.transform;

        /// <summary>
        /// Maps the specified RoomType to the room's name.
        /// </summary>
        /// <param name="type">The EXILED room type.</param>
        /// <returns>The room's name.</returns>
        public static string GetRoomName(this RoomType type) => type switch {
            RoomType.EzCafeteria => "EZ_Cafeteria",
            RoomType.EzCollapsedTunnel => "EZ_CollapsedTunnel",
            RoomType.EzCrossing => "EZ_Crossing",
            RoomType.EzCurve => "EZ_Curve",
            RoomType.EzVent => "EZ_Endoof",
            RoomType.EzGateA => "EZ_GateA",
            RoomType.EzGateB => "EZ_GateB",
            RoomType.EzIntercom => "EZ_Intercom",
            RoomType.EzPcs => "EZ_PCs",
            RoomType.EzDownstairsPcs => "EZ_PCs_small",
            RoomType.EzShelter => "EZ_Shelter",
            RoomType.EzConference => "EZ_Smallrooms2",
            RoomType.EzStraight => "EZ_Straight",
            RoomType.EzTCross => "EZ_ThreeWay",
            RoomType.EzUpstairsPcs => "EZ_upstairs",
            RoomType.Hcz049 => "HCZ_049",
            RoomType.Hcz079 => "HCZ_079",
            RoomType.Hcz106 => "HCZ_106",
            RoomType.Hcz096 => "HCZ_457",
            RoomType.HczChkpA => "HCZ_ChkpA",
            RoomType.HczChkpB => "HCZ_ChkpB",
            RoomType.HczCrossing => "HCZ_Crossing",
            RoomType.HczCurve => "HCZ_Curve",
            RoomType.HczEzCheckpoint => "HCZ_EZ_Checkpoint",
            RoomType.HczHid => "HCZ_Hid",
            RoomType.HczNuke => "HCZ_Nuke",
            RoomType.HczTCross => "HCZ_Room3",
            RoomType.HczArmory => "HCZ_Room3ar",
            RoomType.HczServers => "HCZ_Servers",
            RoomType.HczStraight => "HCZ_Straight",
            RoomType.HczTesla => "HCZ_Tesla",
            RoomType.Hcz939 => "HCZ_Testroom",
            RoomType.Lcz173 => "LCZ_173",
            RoomType.Lcz330 => "LCZ_330",
            RoomType.LczGlassBox => "LCZ_372",
            RoomType.Lcz914 => "LCZ_914",
            RoomType.LczAirlock => "LCZ_Airlock",
            RoomType.LczArmory => "LCZ_Armory",
            RoomType.LczCafe => "LCZ_Cafe",
            RoomType.LczChkpA => "LCZ_ChkpA",
            RoomType.LczChkpB => "LCZ_ChkpB",
            RoomType.LczClassDSpawn => "LCZ_ClassDSpawn",
            RoomType.LczCrossing => "LCZ_Crossing",
            RoomType.LczCurve => "LCZ_Curve",
            RoomType.LczPlants => "LCZ_Plants",
            RoomType.LczStraight => "LCZ_Straight",
            RoomType.LczTCross => "LCZ_TCross",
            RoomType.LczToilets => "LCZ_Toilets",
            RoomType.Surface => "Outside",
            RoomType.Pocket => "PocketWorld",
            _ => ""
        };

        /// <summary>
        /// Gets the <see cref="RoomType"/> based on the room's name.
        /// </summary>
        /// <param name="rawName">The name of the room.</param>
        /// <returns>The EXILED room type.</returns>
        public static RoomType GetRoomType(string rawName) => rawName.RemoveParenthesesOnEndOfName() switch {
            "EZ_Cafeteria" => RoomType.EzCafeteria,
            "EZ_CollapsedTunnel" => RoomType.EzCollapsedTunnel,
            "EZ_Crossing" => RoomType.EzCrossing,
            "EZ_Curve" => RoomType.EzCurve,
            "EZ_Endoof" => RoomType.EzVent,
            "EZ_GateA" => RoomType.EzGateA,
            "EZ_GateB" => RoomType.EzGateB,
            "EZ_Intercom" => RoomType.EzIntercom,
            "EZ_PCs" => RoomType.EzPcs,
            "EZ_PCs_small" => RoomType.EzDownstairsPcs,
            "EZ_Shelter" => RoomType.EzShelter,
            "EZ_Smallrooms2" => RoomType.EzConference,
            "EZ_Straight" => RoomType.EzStraight,
            "EZ_ThreeWay" => RoomType.EzTCross,
            "EZ_upstairs" => RoomType.EzUpstairsPcs,
            "HCZ_049" => RoomType.Hcz049,
            "HCZ_079" => RoomType.Hcz079,
            "HCZ_106" => RoomType.Hcz106,
            "HCZ_457" => RoomType.Hcz096,
            "HCZ_ChkpA" => RoomType.HczChkpA,
            "HCZ_ChkpB" => RoomType.HczChkpB,
            "HCZ_Crossing" => RoomType.HczCrossing,
            "HCZ_Curve" => RoomType.HczCurve,
            "HCZ_EZ_Checkpoint" => RoomType.HczEzCheckpoint,
            "HCZ_Hid" => RoomType.HczHid,
            "HCZ_Nuke" => RoomType.HczNuke,
            "HCZ_Room3" => RoomType.HczTCross,
            "HCZ_Room3ar" => RoomType.HczArmory,
            "HCZ_Servers" => RoomType.HczServers,
            "HCZ_Straight" => RoomType.HczStraight,
            "HCZ_Tesla" => RoomType.HczTesla,
            "HCZ_Testroom" => RoomType.Hcz939,
            "LCZ_173" => RoomType.Lcz173,
            "LCZ_330" => RoomType.Lcz330,
            "LCZ_372" => RoomType.LczGlassBox,
            "LCZ_914" => RoomType.Lcz914,
            "LCZ_Airlock" => RoomType.LczAirlock,
            "LCZ_Armory" => RoomType.LczArmory,
            "LCZ_Cafe" => RoomType.LczCafe,
            "LCZ_ChkpA" => RoomType.LczChkpA,
            "LCZ_ChkpB" => RoomType.LczChkpB,
            "LCZ_ClassDSpawn" => RoomType.LczClassDSpawn,
            "LCZ_Crossing" => RoomType.LczCrossing,
            "LCZ_Curve" => RoomType.LczCurve,
            "LCZ_Plants" => RoomType.LczPlants,
            "LCZ_Straight" => RoomType.LczStraight,
            "LCZ_TCross" => RoomType.LczTCross,
            "LCZ_Toilets" => RoomType.LczToilets,
            "Outside" => RoomType.Surface,
            "PocketWorld" => RoomType.Pocket,
            _ => RoomType.Unknown
        };

        public static string RemoveParenthesesOnEndOfName(this string name) {
            var startIndex = name.IndexOf('(') - 1;
            if (startIndex > 0)
                name = name.Remove(startIndex, name.Length - startIndex);
            return name;
        }

        public static RoomIdentifier GetRoomByType(RoomType type) {
            var name = type.GetRoomName();
            return Rooms.FirstOrDefault(e => e.gameObject.name.RemoveParenthesesOnEndOfName() == name);
        }

    }

}
