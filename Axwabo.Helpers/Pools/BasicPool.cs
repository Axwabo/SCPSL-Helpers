using System;

namespace Axwabo.Helpers.Pools {

    /// <summary>
    /// A pool containing objects of type <typeparamref name="T"/>, allowing for renting from and returning objects to it, thus reducing the amount of object instantiation needed.
    /// </summary>
    /// <typeparam name="T">The type of object the pool contains.</typeparam>
    public class BasicPool<T> : PoolBase<T> where T : new() {

        /// <summary>A shared instance of the pool.</summary>
        public static readonly BasicPool<T> Shared = new();

        /// <inheritdoc />
        protected override Func<T> DefaultSupplier { get; }

        /// <inheritdoc />
        public BasicPool() => DefaultSupplier = () => new T();

        /// <inheritdoc />
        public BasicPool(uint maxSize) : base(maxSize) => DefaultSupplier = () => new T();
        
    }

}
