using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MapGeneration;
using UnityEngine;

namespace Axwabo.Helpers.Config {

    /// <summary>
    /// Helper methods for custom config structs.
    /// </summary>
    public static class ConfigHelper {

        private static readonly Dictionary<string, RoomType> NameToValue = new();
        private static readonly Dictionary<RoomType, string> ValueToName = new();

        static ConfigHelper() {
            var members = typeof(RoomType).GetMembers();
            var enums = typeof(RoomType).Enums<RoomType>().ToArray();
            foreach (var memberInfo in members) {
                var attr = memberInfo.GetCustomAttribute<RoomNameAttribute>();
                if (attr == null)
                    continue;
                var value = enums.FirstOrDefault(e => e.ToString() == memberInfo.Name);
                if (value == RoomType.Unknown)
                    continue;
                NameToValue[attr.Name] = value;
                ValueToName[value] = attr.Name;
            }
        }

        /// <summary>Gets all <see cref="RoomIdentifier"/>s in the scene.</summary>
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
        public static string GetRoomName(this RoomType type) => ValueToName.TryGetValue(type, out var name) ? name : null;

        /// <summary>
        /// Gets the <see cref="RoomType"/> based on the room's name.
        /// </summary>
        /// <param name="rawName">The name of the room.</param>
        /// <returns>The EXILED room type.</returns>
        public static RoomType GetRoomType(string rawName) => NameToValue.TryGetValue(rawName.RemoveParenthesesOnEndOfName(), out var type) ? type : RoomType.Unknown;

        /// <summary>
        /// Removes the parentheses and everything after it from the string.
        /// </summary>
        /// <param name="name">The string to remove the parentheses from.</param>
        /// <returns>The string without the parentheses.</returns>
        public static string RemoveParenthesesOnEndOfName(this string name) {
            var startIndex = name.IndexOf('(') - 1;
            return startIndex > 0 ? name.Remove(startIndex, name.Length - startIndex) : name;
        }

        /// <summary>
        /// Gets a room by its type.
        /// </summary>
        /// <param name="type">The type of the room.</param>
        /// <returns>The room, or null if it couldn't be found.</returns>
        /// <seealso cref="RoomType"/>
        public static RoomIdentifier GetRoomByType(RoomType type) {
            var name = type.GetRoomName();
            return Rooms.FirstOrDefault(e => e.gameObject.name.RemoveParenthesesOnEndOfName() == name);
        }

    }

}
