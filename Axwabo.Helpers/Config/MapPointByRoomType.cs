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
    public struct MapPointByRoomType {

        /// <summary>
        /// An empty config object, representing no rooms.
        /// </summary>
        public static readonly MapPointByRoomType Empty = new(RoomType.Unknown);

        #region Info

        /// <summary>
        /// The type of thea|ad room.
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
        public Pose WorldPose() => TryGetWorldTransform(out var pos, out var rot) ? new Pose(pos, rot) : Pose.identity;

        /// <summary>
        /// Attempts to get the world-space position and rotation by applying the offset to the room.
        /// </summary>
        /// <param name="position">The world-space position to store.</param>
        /// <param name="rotation">The world-space rotation to store.</param>
        /// <returns>If the room is not null.</returns>
        public bool TryGetWorldTransform(out Vector3 position, out Quaternion rotation) => RoomTransform().TryTransformOffset(PositionOffset, RotationOffset, out position, out rotation);

        #endregion

    }

}
