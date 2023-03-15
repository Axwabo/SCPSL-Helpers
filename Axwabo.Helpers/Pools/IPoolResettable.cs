namespace Axwabo.Helpers.Pools;

/// <summary>
/// Lets a <see cref="PoolBase{T}">pool</see> know that this object can be automatically reset and destroyed.
/// </summary>
public interface IPoolResettable
{

    /// <summary>
    /// Resets the object to its default state.
    /// </summary>
    void Reset();

    /// <summary>
    /// Destroys the object.
    /// </summary>
    void Destroy();

}
