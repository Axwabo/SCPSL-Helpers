namespace Axwabo.Helpers.Pools {

    /// Lets a <see cref="PoolBase{T}">pool</see> know that this object can be automatically reset and destroyed.
    public interface IPoolResettable {

        /// Resets the object to its default state.
        void Reset();

        /// Destroys the object.
        void Destroy();

    }

}
