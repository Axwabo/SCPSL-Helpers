using MEC;
using Object = UnityEngine.Object;

namespace Axwabo.Helpers;

/// <summary>
/// Helper methods for Unity classes.
/// </summary>
public static class UnityHelper
{

    #region Distance

    #region Squared distance

    /// <summary>
    /// Gets the squared distance between two <see cref="Vector3">vectors</see>.
    /// </summary>
    /// <param name="a">The source vector.</param>
    /// <param name="b">The destination vector.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this Vector3 a, Vector3 b) => (a - b).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between two <see cref="GameObject">GameObjects</see>.
    /// </summary>
    /// <param name="a">The source object.</param>
    /// <param name="b">The destination object.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this GameObject a, GameObject b) => (a.transform.position - b.transform.position).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between a <see cref="GameObject"/> and a <see cref="Vector3"/>.
    /// </summary>
    /// <param name="a">The source object.</param>
    /// <param name="b">The destination vector.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this GameObject a, Vector3 b) => (a.transform.position - b).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between a <see cref="Vector3"/> and a <see cref="GameObject"/>.
    /// </summary>
    /// <param name="a">The source vector.</param>
    /// <param name="b">The destination object.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this Vector3 a, GameObject b) => (a - b.transform.position).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between the positions of two <see cref="Component">Components</see>.
    /// </summary>
    /// <param name="a">A component on the source object.</param>
    /// <param name="b">A component on the destination object.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this Component a, Component b) => (a.transform.position - b.transform.position).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between a <see cref="Component"/> and a <see cref="Vector3"/>.
    /// </summary>
    /// <param name="a">A component on the source object.</param>
    /// <param name="b">The destination vector.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this Component a, Vector3 b) => (a.transform.position - b).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between a <see cref="Vector3"/> and a <see cref="Component"/>.
    /// </summary>
    /// <param name="a">The source vector.</param>
    /// <param name="b">A component on the destination object.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this Vector3 a, Component b) => (a - b.transform.position).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between two <see cref="Transform">Transforms</see>.
    /// </summary>
    /// <param name="a">The source transform.</param>
    /// <param name="b">The destination transform.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this Transform a, Transform b) => (a.position - b.position).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between a <see cref="Transform"/> and a <see cref="Vector3"/>.
    /// </summary>
    /// <param name="a">The source transform.</param>
    /// <param name="b">The destination vector.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this Transform a, Vector3 b) => (a.position - b).sqrMagnitude;

    /// <summary>
    /// Gets the squared distance between a <see cref="Vector3"/> and a <see cref="Transform"/>.
    /// </summary>
    /// <param name="a">The source vector.</param>
    /// <param name="b">The destination transform.</param>
    /// <returns>The squared distance between the two.</returns>
    public static float DistanceSquared(this Vector3 a, Transform b) => (a - b.transform.position).sqrMagnitude;

    #endregion

    #region Distance checks

    /// <summary>
    /// Checks if the distance between two <see cref="Vector3">vectors</see> is less than or equal to a value.
    /// </summary>
    /// <param name="a">The source vector.</param>
    /// <param name="b">The destination vector.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this Vector3 a, Vector3 b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between two <see cref="GameObject">GameObjects</see> is less than or equal to a value.
    /// </summary>
    /// <param name="a">The source object.</param>
    /// <param name="b">The destination object.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this GameObject a, GameObject b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between two a <see cref="GameObject"/> and a <see cref="Vector3"/> is less than or equal to a value.
    /// </summary>
    /// <param name="a">The source object.</param>
    /// <param name="b">The destination vector.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this GameObject a, Vector3 b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between a <see cref="Vector3"/> and a <see cref="GameObject"/> is less than or equal to a value.
    /// </summary>
    /// <param name="a">The source vector.</param>
    /// <param name="b">The destination object.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this Vector3 a, GameObject b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between two <see cref="Component">Components</see> is less than or equal to a value.
    /// </summary>
    /// <param name="a">A component on the source object.</param>
    /// <param name="b">A component on the destination object.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this Component a, Component b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between a <see cref="Component"/> and a <see cref="Vector3"/> is less than or equal to a value.
    /// </summary>
    /// <param name="a">A component on the source object.</param>
    /// <param name="b">The destination vector.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this Component a, Vector3 b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between a <see cref="Vector3"/> and a <see cref="Component"/> is less than or equal to a value.
    /// </summary>
    /// <param name="a">The source vector.</param>
    /// <param name="b">A component on the destination object.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this Vector3 a, Component b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between two <see cref="Transform">Transforms</see> is less than or equal to a value.
    /// </summary>
    /// <param name="a">The source transform.</param>
    /// <param name="b">The destination transform.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this Transform a, Transform b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between a <see cref="Transform"/> and a <see cref="Vector3"/> is less than or equal to a value.
    /// </summary>
    /// <param name="a">The source transform.</param>
    /// <param name="b">The destination vector.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this Transform a, Vector3 b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    /// <summary>
    /// Checks if the distance between a <see cref="Vector3"/> and a <see cref="Transform"/> is less than or equal to a value.
    /// </summary>
    /// <param name="a">The source vector.</param>
    /// <param name="b">The destination transform.</param>
    /// <param name="maxDistance">The maximum distance.</param>
    /// <returns>Whether the distance is in range.</returns>
    public static bool IsWithinDistance(this Vector3 a, Transform b, float maxDistance) => DistanceSquared(a, b) <= maxDistance * maxDistance;

    #endregion

    #endregion

    #region Component helpers

    /// <summary>
    /// Gets an already existing component on a GameObject or adds it.
    /// </summary>
    /// <param name="gameObject">The object to get the component from.</param>
    /// <typeparam name="T">The type of the component to get.</typeparam>
    /// <returns>An already existing or newly created component.</returns>
    public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component =>
        gameObject != null ? gameObject.TryGetComponent(out T component) ? component : gameObject.AddComponent<T>() : throw new ArgumentNullException(nameof(gameObject));

    /// <summary>
    /// Gets an already existing component on a GameObject or adds it.
    /// </summary>
    /// <param name="component">A component on the object to get the component of type <typeparamref name="T"/> from.</param>
    /// <typeparam name="T">The type of the component to get.</typeparam>
    /// <returns>An already existing or newly created component.</returns>
    public static T GetOrAddComponent<T>(this Component component) where T : Component => GetOrAddComponent<T>(component.gameObject);

    /// <summary>
    /// Destroys the component of type <typeparamref name="T"/> on a GameObject. 
    /// </summary>
    /// <param name="gameObject">The object to remove the component from.</param>
    /// <typeparam name="T">The type of component to destroy.</typeparam>
    /// <returns>If the component was found and will be destroyed at the end of the frame.</returns>
    /// <footer><a href="https://docs.unity3d.com/ScriptReference/Object.Destroy.html">Object.Destroy on docs.unity3d.com</a></footer>
    public static bool DestroyComponent<T>(this GameObject gameObject) where T : Component
    {
        if (gameObject == null || !gameObject.TryGetComponent(out T component))
            return false;
        Object.Destroy(component);
        return true;
    }

    /// <summary>
    /// Destroys the component of type <typeparamref name="T"/> on a GameObject. 
    /// </summary>
    /// <param name="component">A component on the object to remove the component of type <typeparamref name="T"/> from.</param>
    /// <typeparam name="T">The type of component to destroy.</typeparam>
    /// <returns>If the component was found and will be destroyed at the end of the frame.</returns>
    /// <footer><a href="https://docs.unity3d.com/ScriptReference/Object.Destroy.html">Object.Destroy on docs.unity3d.com</a></footer>
    public static bool DestroyComponent<T>(this Component component) where T : Component
    {
        if (component == null || !component.gameObject.TryGetComponent(out T c))
            return false;
        Object.Destroy(c);
        return true;
    }

    /// <summary>
    /// Immediately destroys the component of type <typeparamref name="T"/> on a GameObject. 
    /// </summary>
    /// <param name="gameObject">The object to remove the component from.</param>
    /// <typeparam name="T">The type of component to destroy.</typeparam>
    /// <returns>If the component was found and was destroyed.</returns>
    /// <footer><a href="https://docs.unity3d.com/ScriptReference/Object.DestroyImmediate.html">Object.DestroyImmediate on docs.unity3d.com</a></footer>
    public static bool DestroyComponentImmediate<T>(this GameObject gameObject) where T : Component
    {
        if (!gameObject.TryGetComponent(out T component))
            return false;
        Object.DestroyImmediate(component);
        return true;
    }

    /// <summary>
    /// Immediately destroys the component of type <typeparamref name="T"/> on a GameObject. 
    /// </summary>
    /// <param name="component">A component on the object to remove the component of type <typeparamref name="T"/> from.</param>
    /// <typeparam name="T">The type of component to destroy.</typeparam>
    /// <returns>If the component was found and was destroyed.</returns>
    /// <footer><a href="https://docs.unity3d.com/ScriptReference/Object.DestroyImmediate.html">Object.DestroyImmediate on docs.unity3d.com</a></footer>
    public static bool DestroyComponentImmediate<T>(this Component component) where T : Component
    {
        if (!component.gameObject.TryGetComponent(out T c))
            return false;
        Object.DestroyImmediate(c);
        return true;
    }

    #endregion

    #region Call next frame

    /// <summary>
    /// Calls the given <see cref="Action"/> on the next frame.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    public static void CallNextFrame(Action action) => Timing.RunCoroutine(ExecuteAfterFrames(action));

    /// <summary>
    /// Calls the given <see cref="Action"/> after a given amount of frames.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="frameCount">The amount of frames to wait before executing the action.</param>
    public static void CallAfterFrames(Action action, int frameCount = 1) => Timing.RunCoroutine(ExecuteAfterFrames(action, frameCount));

    private static IEnumerator<float> ExecuteAfterFrames(Action action, int frameCount = 1)
    {
        for (var i = 0; i < frameCount; i++)
            yield return Timing.WaitForOneFrame;
        action();
    }

    #endregion

}
