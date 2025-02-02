using MapGeneration;

namespace Axwabo.Helpers.Config;

/// <summary>
/// A config object representing an offset to a room defined by the given <see cref="RoomType"/>.
/// </summary>
[Serializable]
public record struct MapPointByRoomType : IMapPoint
{

    /// <summary>An empty config object representing no rooms.</summary>
    public static readonly MapPointByRoomType Empty = new(RoomType.Unknown);

    #region Info

    /// <summary>The type of the room.</summary>
    public RoomType Type { get; set; }

    /// <summary>World-space position offset to apply to the room.</summary>
    public SerializedRotation PositionOffset { get; set; }

    /// <summary>Rotational offset to the room.</summary>
    public SerializedRotation RotationOffset { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance without any offset.
    /// </summary>
    /// <param name="type">The type of the room.</param>
    public MapPointByRoomType(RoomType type) : this(type, SerializedRotation.Identity, SerializedRotation.Identity)
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position offset.
    /// </summary>
    /// <param name="type">The type of the room.</param>
    /// <param name="positionOffset">A world-space position offset.</param>
    public MapPointByRoomType(RoomType type, SerializedRotation positionOffset) : this(type, positionOffset, SerializedRotation.Identity)
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position offset.
    /// </summary>
    /// <param name="type">The type of the room.</param>
    /// <param name="x">Offset on the X axis.</param>
    /// <param name="y">Offset on the Y axis.</param>
    /// <param name="z">Offset on the Z axis.</param>
    public MapPointByRoomType(RoomType type, float x = 0, float y = 0, float z = 0) : this(type, new SerializedRotation(x, y, z), SerializedRotation.Identity)
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position and rotation offset.
    /// </summary>
    /// <param name="type">The type of the room.</param>
    /// <param name="positionOffset">A world-space position offset.</param>
    /// <param name="rotationOffset">A rotational offset.</param>
    public MapPointByRoomType(RoomType type, SerializedRotation positionOffset, SerializedRotation rotationOffset)
    {
        Type = type;
        PositionOffset = positionOffset;
        RotationOffset = rotationOffset;
    }

    #endregion

    #region Getters

    /// <summary>If the object has been initialized using a constructor.</summary>
    public bool IsValid() => Type != RoomType.Unknown;

    /// <summary>Gets the room component for the given <see cref="Type">room type</see>.</summary>
    public RoomIdentifier RoomObject() => Type == RoomType.Unknown ? null : ConfigHelper.GetRoomByType(Type);

    /// <summary>Gets the transform of the room object.</summary>
    public Transform RoomTransform() => RoomObject().SafeGetTransform();

    /// <inheritdoc />
    public Pose WorldPose() => TryGetWorldPose(out var pos, out var rot) ? new Pose(pos, rot) : Pose.identity;

    /// <inheritdoc />
    public bool TryGetWorldPose(out Vector3 position, out Quaternion rotation) => RoomTransform().TryTransformOffset(PositionOffset, RotationOffset, out position, out rotation);

    #endregion

}
