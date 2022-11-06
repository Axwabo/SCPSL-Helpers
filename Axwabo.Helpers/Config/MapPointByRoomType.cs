using System;
using Exiled.API.Enums;
using Exiled.API.Features;
using UnityEngine;

namespace Axwabo.Helpers.Config {

    /// <summary>
    /// A config object representing an offset to a room defined by the given <see cref="RoomType"/>.
    /// </summary>
    /// <seealso cref="ConfigHelper.GetRoomName"/>
    [Serializable]
    public struct MapPointByRoomType : IMapPoint {

        /// <summary>
        /// An empty config object representing no rooms.
        /// </summary>
        public static readonly MapPointByRoomType Empty = new(RoomType.Unknown);

        #region Info

        /// <summary>
        /// The type of the room.
        /// </summary>
        public RoomType Type { get; set; }

        /// <summary>
        /// World-space position offset to apply to the room.
        /// </summary>
        public Vector3 PositionOffset { get; set; }

        /// <summary>
        /// Rotational offset to the room.
        /// </summary>
        public SerializedRotation RotationOffset { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance without any offset.
        /// </summary>
        /// <param name="type">The type of the room.</param>
        public MapPointByRoomType(RoomType type) : this(type, Vector3.zero, SerializedRotation.Identity) {
        }

        /// <summary>
        /// Initializes a new instance with the given position offset.
        /// </summary>
        /// <param name="type">The type of the room.</param>
        /// <param name="positionOffset">A world-space position offset.</param>
        public MapPointByRoomType(RoomType type, Vector3 positionOffset) : this(type, positionOffset, SerializedRotation.Identity) {
        }

        /// <summary>
        /// Initializes a new instance with the given position offset.
        /// </summary>
        /// <param name="type">The type of the room.</param>
        /// <param name="x">Offset on the X axis.</param>
        /// <param name="y">Offset on the Y axis.</param>
        /// <param name="z">Offset on the Z axis.</param>
        public MapPointByRoomType(RoomType type, float x = 0, float y = 0, float z = 0) : this(type, new Vector3(x, y, z), SerializedRotation.Identity) {
        }

        /// <summary>
        /// Initializes a new instance with the given position and rotation offset.
        /// </summary>
        /// <param name="type">The type of the room.</param>
        /// <param name="positionOffset">A world-space position offset.</param>
        /// <param name="rotationOffset">A rotational offset.</param>
        public MapPointByRoomType(RoomType type, Vector3 positionOffset, SerializedRotation rotationOffset) {
            Type = type;
            PositionOffset = positionOffset;
            RotationOffset = rotationOffset;
        }

        #endregion

        #region Getters

        /// <summary>
        /// If the object has been initialized using a constructor.
        /// </summary>
        public bool IsValid() => Type != RoomType.Unknown;

        /// <summary>
        /// Gets the room component for the given <see cref="Type">room type</see>.
        /// </summary>
        public Room RoomObject() => Type == RoomType.Unknown ? null : Room.Get(Type);

        /// <summary>
        /// Gets the transform of the room object.
        /// </summary>
        public Transform RoomTransform() => RoomObject().SafeGetTransform();

        /// <summary>
        /// Gets the world-space position and rotation by applying the offset to the room.
        /// </summary>
        public Pose WorldPose() => TryGetWorldPose(out var pos, out var rot) ? new Pose(pos, rot) : Pose.identity;

        /// <summary>
        /// Attempts to get the world-space position and rotation by applying the offset to the room.
        /// </summary>
        /// <param name="position">The world-space position to store.</param>
        /// <param name="rotation">The world-space rotation to store.</param>
        /// <returns>If the room is not null.</returns>
        public bool TryGetWorldPose(out Vector3 position, out Quaternion rotation) => RoomTransform().TryTransformOffset(PositionOffset, RotationOffset, out position, out rotation);

        #endregion

        #region Operators

        /// <summary>
        /// Checks if the two points are equal.
        /// </summary>
        /// <param name="other">The other point to compare with.</param>
        /// <returns>Whether the two points are equal.</returns>
        public bool Equals(MapPointByRoomType other) => PositionOffset == other.PositionOffset && RotationOffset == other.RotationOffset && Type == other.Type;

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is MapPointByRoomType point && point.Equals(this);

        /// <summary>
        /// Calls the <see cref="Equals(MapPointByRoomType)"/> method.
        /// </summary>
        /// <param name="a">The first point to compare.</param>
        /// <param name="b">The second point to compare.</param>
        /// <returns>Whether the two points are equal.</returns>
        public static bool operator ==(MapPointByRoomType a, MapPointByRoomType b) => a.Equals(b);

        /// <summary>
        /// Calls the <see cref="Equals(MapPointByRoomType)"/> method.
        /// </summary>
        /// <param name="a">The first point to compare.</param>
        /// <param name="b">The second point to compare.</param>
        /// <returns>Whether the two points not are equal.</returns>
        public static bool operator !=(MapPointByRoomType a, MapPointByRoomType b) => !a.Equals(b);

        #endregion

    }

}
