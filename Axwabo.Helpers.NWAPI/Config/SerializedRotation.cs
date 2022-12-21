using System;
using UnityEngine;

namespace Axwabo.Helpers.Config {

    /// <summary>
    /// A serializable <see cref="Vector3"/> representing 3 axis of rotation. Can be converted into a <see cref="Quaternion"/>.
    /// </summary>
    [Serializable]
    public struct SerializedRotation {

        /// <summary>
        /// An empty rotation object.
        /// </summary>
        public static readonly SerializedRotation Identity = new();

        /// <summary>
        /// The X axis of rotation.
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The Y axis of rotation.
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The Z axis of rotation.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Creates a new <see cref="SerializedRotation"/> object.
        /// </summary>
        public SerializedRotation(float x, float y = 0, float z = 0) {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Checks if the two rotations are equal.
        /// </summary>
        /// <param name="other">The other rotation to compare with.</param>
        /// <returns>Whether the two rotations are equal.</returns>
        public bool Equals(SerializedRotation other) => X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

        /// <inheritdoc />
        public override bool Equals(object obj) => obj is SerializedRotation other && Equals(other);

        /// <summary>
        /// Calls the <see cref="Equals(SerializedRotation)"/> method.
        /// </summary>
        /// <param name="a">The first rotation to compare.</param>
        /// <param name="b">The second rotation to compare.</param>
        /// <returns>Whether the two rotations are equal.</returns>
        public static bool operator ==(SerializedRotation a, SerializedRotation b) => a.Equals(b);

        /// <summary>
        /// Calls the <see cref="Equals(SerializedRotation)"/> method.
        /// </summary>
        /// <param name="a">The first rotation to compare.</param>
        /// <param name="b">The second rotation to compare.</param>
        /// <returns>Whether the two rotations are not equal.</returns>
        public static bool operator !=(SerializedRotation a, SerializedRotation b) => !a.Equals(b);

        /// <summary>
        /// Adds two rotations.
        /// </summary>
        /// <param name="a">The rotation to add to.</param>
        /// <param name="b">A rotation to add.</param>
        /// <returns>The sum of the two rotations.</returns>
        public static SerializedRotation operator +(SerializedRotation a, SerializedRotation b) => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

        /// <summary>
        /// Subtracts two rotations.
        /// </summary>
        /// <param name="a">The rotation to subtract from.</param>
        /// <param name="b">A rotation to subtract.</param>
        /// <returns>The difference of the two rotations.</returns>
        public static SerializedRotation operator -(SerializedRotation a, SerializedRotation b) => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

        /// <summary>
        /// Multiplies two rotations.
        /// </summary>
        /// <param name="a">The rotation to multiply.</param>
        /// <param name="b">A rotation to multiply with.</param>
        /// <returns>The product of the two rotations.</returns>
        public static SerializedRotation operator *(SerializedRotation a, SerializedRotation b) => new(a.X * b.X, a.Y * b.Y, a.Z * b.Z);

        /// <summary>
        /// Divides two rotations.
        /// </summary>
        /// <param name="a">The rotation to divide.</param>
        /// <param name="b">A rotation to divide with.</param>
        /// <returns>The quotient of the two rotations.</returns>
        public static SerializedRotation operator /(SerializedRotation a, SerializedRotation b) => new(a.X / b.X, a.Y / b.Y, a.Z / b.Z);

        /// <summary>
        /// Multiplies a rotation by a scalar.
        /// </summary>
        /// <param name="a">The rotation to multiply.</param>
        /// <param name="b">A scalar to multiply with.</param>
        /// <returns>The product of the rotation and the scalar.</returns>
        public static SerializedRotation operator *(SerializedRotation a, float b) => new(a.X * b, a.Y * b, a.Z * b);

        /// <summary>
        /// Multiplies a rotation by a scalar.
        /// </summary>
        /// <param name="a">The rotation to multiply.</param>
        /// <param name="b">A scalar to multiply with.</param>
        /// <returns>The product of the rotation and the scalar.</returns>
        public static SerializedRotation operator *(float a, SerializedRotation b) => new(a * b.X, a * b.Y, a * b.Z);

        /// <summary>
        /// Divides a rotation by a scalar.
        /// </summary>
        /// <param name="a">The rotation to divide.</param>
        /// <param name="b">A scalar to divide with.</param>
        /// <returns>The quotient of the rotation and the scalar.</returns>
        public static SerializedRotation operator /(SerializedRotation a, float b) => new(a.X / b, a.Y / b, a.Z / b);

        /// <summary>
        /// Converts a <see cref="Quaternion"/> to a <see cref="SerializedRotation"/>.
        /// </summary>
        /// <param name="serialized">The rotation to convert.</param>
        /// <returns>A quaternion equivalent to the given rotation.</returns>
        public static implicit operator Quaternion(SerializedRotation serialized) => Quaternion.Euler(serialized.X, serialized.Y, serialized.Z);

        /// <summary>
        /// Converts a <see cref="Quaternion"/> to a <see cref="SerializedRotation"/>.
        /// </summary>
        /// <param name="rotation">The quaternion to convert.</param>
        /// <returns>A serialized rotation equivalent to the given quaternion.</returns>
        public static implicit operator SerializedRotation(Quaternion rotation) => rotation.eulerAngles;

        /// <summary>
        /// Converts a <see cref="SerializedRotation"/> to a <see cref="Vector3"/>.
        /// </summary>
        /// <param name="serialized">The rotation to convert.</param>
        /// <returns>A vector equivalent to the given rotation.</returns>
        public static implicit operator Vector3(SerializedRotation serialized) => new(serialized.X, serialized.Y, serialized.Z);

        /// <summary>
        /// Converts a <see cref="Vector3"/> to a <see cref="SerializedRotation"/>.
        /// </summary>
        /// <param name="rotation">The vector to convert.</param>
        /// <returns>A serialized rotation equivalent to the given vector.</returns>
        public static implicit operator SerializedRotation(Vector3 rotation) => new(rotation.x, rotation.y, rotation.z);

    }

}
