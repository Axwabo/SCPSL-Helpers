namespace Axwabo.Helpers.Config;

/// <summary>
/// An interface used to implement methods to serialize points on the map.
/// </summary>
/// <seealso cref="StaticMapPoint"/>
/// <seealso cref="MapPointByName"/>
/// <seealso cref="MapPointByRoomType"/>
public interface IMapPoint
{

    /// <summary>
    /// Checks if the point is a valid instance.
    /// </summary>
    /// <returns>Whether the point is valid.</returns>
    bool IsValid();

    /// <summary>
    /// Gets the world-space position and rotation of the map point.
    /// </summary>
    Pose WorldPose();

    /// <summary>
    /// Attempts to get the world-space position and rotation of the map point.
    /// </summary>
    /// <param name="position">The world-space position to store.</param>
    /// <param name="rotation">The world-space rotation to store.</param>
    /// <returns>If the operation was successful.</returns>
    bool TryGetWorldPose(out Vector3 position, out Quaternion rotation);

}
