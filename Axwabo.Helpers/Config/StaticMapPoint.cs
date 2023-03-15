using System;
using UnityEngine;

namespace Axwabo.Helpers.Config;

/// <summary>
/// A config object representing a static point on the map.
/// </summary>
[Serializable]
public struct StaticMapPoint : IMapPoint
{

    /// <summary>An empty config object placed at the center of the world without rotation.</summary>
    public static readonly StaticMapPoint Empty = new();

    #region Info

    /// <summary>The world-space position of the point.</summary>
    public SerializedRotation Position { get; set; }

    /// <summary>The world-space rotation of the point.</summary>
    public SerializedRotation Rotation { get; set; }

    #endregion

    #region Constructors

    /// <summary>
    /// Initializes a new instance with the given position.
    /// </summary>
    /// <param name="x">Position on the X axis.</param>
    /// <param name="y">Position on the Y axis.</param>
    /// <param name="z">Position on the Z axis.</param>
    public StaticMapPoint(float x, float y = 0, float z = 0) : this(new SerializedRotation(x, y, z))
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position and rotation.
    /// </summary>
    /// <param name="x">Position on the X axis.</param>
    /// <param name="y">Position on the Y axis.</param>
    /// <param name="z">Position on the Z axis.</param>
    /// <param name="rotation">The rotation of the point.</param>
    public StaticMapPoint(float x, float y, float z, SerializedRotation rotation) : this(new SerializedRotation(x, y, z), rotation)
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position.
    /// </summary>
    /// <param name="position">The position of the point.</param>
    public StaticMapPoint(SerializedRotation position) : this(position, SerializedRotation.Identity)
    {
    }

    /// <summary>
    /// Initializes a new instance with the given position and rotation.
    /// </summary>
    /// <param name="position">The position of the point.</param>
    /// <param name="rotation">The rotation of the point.</param>
    public StaticMapPoint(SerializedRotation position, SerializedRotation rotation)
    {
        Position = position;
        Rotation = rotation;
    }

    #endregion

    #region Getters

    /// <summary>Always returns true since there's no invalid state of a static point.</summary>
    public bool IsValid() => true;

    /// <inheritdoc />
    public Pose WorldPose() => new(Position, Rotation);

    /// <summary>
    /// Stores the object in two specified variables. 
    /// </summary>
    /// <param name="position">The position variable.</param>
    /// <param name="rotation">The rotation variable.</param>
    public bool TryGetWorldPose(out Vector3 position, out Quaternion rotation)
    {
        position = Position;
        rotation = Rotation;
        return true;
    }

    #endregion

    #region Operators

    /// <summary>
    /// Checks if the two points are equal.
    /// </summary>
    /// <param name="other">The other point to compare with.</param>
    /// <returns>Whether the two points are equal.</returns>
    public bool Equals(StaticMapPoint other) => Position == other.Position && Rotation == other.Rotation;

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is StaticMapPoint point && point.Equals(this);

    /// <summary>
    /// Calls the <see cref="WorldPose"/> method on a point to convert it to a <see cref="Pose"/>.
    /// </summary>
    /// <param name="point">The point to convert.</param>
    /// <returns>A pose equivalent to the point.</returns>
    public static implicit operator Pose(StaticMapPoint point) => point.WorldPose();

    /// <summary>
    /// Converts a Pose to a StaticMapPoint.
    /// </summary>
    /// <param name="pose">The pose to convert.</param>
    /// <returns>A StaticMapPoint equivalent to the pose.</returns>
    public static implicit operator StaticMapPoint(Pose pose) => new(pose.position, pose.rotation);

    /// <summary>
    /// Calls the <see cref="Equals(StaticMapPoint)"/> method.
    /// </summary>
    /// <param name="a">The first point to compare.</param>
    /// <param name="b">The second point to compare.</param>
    /// <returns>Whether the two points are equal.</returns>
    public static bool operator ==(StaticMapPoint a, StaticMapPoint b) => a.Equals(b);

    /// <summary>
    /// Calls the <see cref="Equals(StaticMapPoint)"/> method.
    /// </summary>
    /// <param name="a">The first point to compare.</param>
    /// <param name="b">The second point to compare.</param>
    /// <returns>Whether the two points not are equal.</returns>
    public static bool operator !=(StaticMapPoint a, StaticMapPoint b) => !a.Equals(b);

    #endregion

}
