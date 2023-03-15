using System;
using MapGeneration;
using UnityEngine;

namespace Axwabo.Helpers.Config;

/// <summary>
/// A config object representing an offset to a room defined by a room's name.
/// </summary>
[Serializable]
public struct MapPointByName : IMapPoint
{

    /// <summary>An empty config object representing no rooms.</summary>
    public static readonly MapPointByName Empty = new();

    #region Info

    /// <summary>The name of the room.</summary>
    public string RoomName { get; set; }

    /// <summary>World-space position offset to apply to the room.</summary>
    public SerializedRotation PositionOffset { get; set; }

    /// <summary>Rotational offset to the room.</summary>
    public SerializedRotation RotationOffset { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance without any offset.
    /// </summary>
    /// <param name="name">The name of the room.</param>
    public MapPointByName(string name) : this(name, SerializedRotation.Identity, SerializedRotation.Identity)
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position offset.
    /// </summary>
    /// <param name="name">The name of the room.</param>
    /// <param name="positionOffset">A world-space position offset.</param>
    public MapPointByName(string name, SerializedRotation positionOffset) : this(name, positionOffset, SerializedRotation.Identity)
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position offset.
    /// </summary>
    /// <param name="name">The name of the room.</param>
    /// <param name="x">Offset on the X axis.</param>
    /// <param name="y">Offset on the Y axis.</param>
    /// <param name="z">Offset on the Z axis.</param>
    public MapPointByName(string name, float x, float y, float z) : this(name, new SerializedRotation(x, y, z), SerializedRotation.Identity)
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position and rotation offset.
    /// </summary>
    /// <param name="name">The name of the room.</param>
    /// <param name="positionOffset">A world-space position offset.</param>
    /// <param name="rotationOffset">A rotational offset.</param>
    public MapPointByName(string name, SerializedRotation positionOffset, SerializedRotation rotationOffset)
    {
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

    /// <summary>Gets the room component for the given <see cref="Type">room type</see>.</summary>
    public RoomIdentifier RoomObject() => ConfigHelper.GetRoomByRoomName(RoomName);

    /// <summary>Gets the transform of the room object.</summary>
    public Transform RoomTransform() => RoomObject().SafeGetTransform();

    /// <inheritdoc />
    public Pose WorldPose() => TryGetWorldPose(out var pos, out var rot) ? new Pose(pos, rot) : Pose.identity;

    /// <inheritdoc />
    public bool TryGetWorldPose(out Vector3 position, out Quaternion rotation) => RoomTransform().TryTransformOffset(PositionOffset, RotationOffset, out position, out rotation);

    #endregion

    #region Operators

    /// <summary>
    /// Checks if the two points are equal.
    /// </summary>
    /// <param name="other">The other point to compare with.</param>
    /// <returns>Whether the two points are equal.</returns>
    public bool Equals(MapPointByName other) => PositionOffset == other.PositionOffset && RotationOffset == other.RotationOffset && string.Equals(RoomName, other.RoomName, StringComparison.InvariantCultureIgnoreCase);

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is MapPointByName point && point.Equals(this);

    /// <summary>
    /// Calls the <see cref="Equals(MapPointByName)"/> method.
    /// </summary>
    /// <param name="a">The first point to compare.</param>
    /// <param name="b">The second point to compare.</param>
    /// <returns>Whether the two points are equal.</returns>
    public static bool operator ==(MapPointByName a, MapPointByName b) => a.Equals(b);

    /// <summary>
    /// Calls the <see cref="Equals(MapPointByName)"/> method.
    /// </summary>
    /// <param name="a">The first point to compare.</param>
    /// <param name="b">The second point to compare.</param>
    /// <returns>Whether the two points not are equal.</returns>
    public static bool operator !=(MapPointByName a, MapPointByName b) => !a.Equals(b);

    #endregion

}
