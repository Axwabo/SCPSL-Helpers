﻿using System.Linq;
using Exiled.API.Features;
using UnityEngine;

namespace Axwabo.Helpers.Config;

/// <summary>Helper methods for custom config structs.</summary>
public static class ConfigHelper
{

    /// <summary>
    /// Gets the position and rotation based on an offset and a transform object. 
    /// </summary>
    /// <param name="transform">The transform to calculate the offset from.</param>
    /// <param name="positionOffset">A world-space position offset.</param>
    /// <param name="rotationOffset">A world-space rotation offset.</param>
    /// <param name="position">The calculated position.</param>
    /// <param name="rotation">The calculated rotation.</param>
    /// <returns>Whether the transform object was not null.</returns>
    public static bool TryTransformOffset(this Transform transform, Vector3 positionOffset, Quaternion rotationOffset, out Vector3 position, out Quaternion rotation)
    {
        if (transform == null)
        {
            position = Vector3.zero;
            rotation = Quaternion.identity;
            return false;
        }

        position = transform.TransformPoint(positionOffset);
        rotation = transform.rotation * rotationOffset;
        return true;
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
    /// Gets a room by its name.
    /// </summary>
    /// <param name="roomName">The name of the room.</param>
    /// <returns>The room if found; otherwise null.</returns>
    public static Room GetRoomByRoomName(string roomName)
    {
        var lower = roomName.ToLower();
        return Room.List.FirstOrDefault(e => e.name.ToLower().Contains(lower));
    }

}
