﻿using System;
using Exiled.API.Features;
using UnityEngine;

namespace Axwabo.Helpers.Config {

    /// <summary>
    /// A config object representing an offset to a room defined by a room's name.
    /// </summary>
    /// <seealso cref="ConfigHelper.GetRoomName"/>
    [Serializable]
    public struct MapPointByName {

        /// <summary>
        /// An empty config object, representing no rooms.
        /// </summary>
        public static readonly MapPointByName Empty = new();

        #region Info

        /// <summary>
        /// The name of the room.
        /// </summary>
        public string RoomName { get; set; }

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
        /// <param name="name">The name of the room.</param>
        public MapPointByName(string name) : this(name, Vector3.zero, SerializedRotation.Identity) {
        }

        /// <summary>
        /// Initializes a new instance with the given position offset.
        /// </summary>
        /// <param name="name">The name of the room.</param>
        /// <param name="positionOffset">A world-space position offset.</param>
        public MapPointByName(string name, Vector3 positionOffset) : this(name, positionOffset, SerializedRotation.Identity) {
        }

        /// <summary>
        /// Initializes a new instance with the given position offset.
        /// </summary>
        /// <param name="name">The name of the room.</param>
        /// <param name="x">Offset on the X axis.</param>
        /// <param name="y">Offset on the Y axis.</param>
        /// <param name="z">Offset on the Z axis.</param>
        public MapPointByName(string name, float x, float y, float z) : this(name, new Vector3(x, y, z), SerializedRotation.Identity) {
        }

        /// <summary>
        /// Initializes a new instance with the given position and rotation offset.
        /// </summary>
        /// <param name="name">The name of the room.</param>
        /// <param name="positionOffset">A world-space position offset.</param>
        /// <param name="rotationOffset">A rotational offset.</param>
        public MapPointByName(string name, Vector3 positionOffset, SerializedRotation rotationOffset) {
            RoomName = name;
            PositionOffset = positionOffset;
            RotationOffset = rotationOffset;
        }

        #endregion

        #region Getters

        /// <summary>
        /// If the object has been initialized using a constructor.
        /// </summary>
        /// <remarks>This does not check if the room exists, unlike <see cref="MapPointByRoomType.IsValid">MapPointByRoomType</see> does.</remarks>
        public bool IsValid() => !string.IsNullOrEmpty(RoomName);

        /// <summary>
        /// Gets the room component for the given <see cref="Type">room type</see>.
        /// </summary>
        public Room RoomObject() => ConfigHelper.GetRoomByRoomName(RoomName);

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