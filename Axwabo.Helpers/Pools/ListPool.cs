using System;
using System.Collections.Generic;

namespace Axwabo.Helpers.Pools {

    /// <summary>
    /// A pool containing lists of type <typeparamref name="T"/>, allowing for renting from and returning objects to it, thus reducing the amount of object instantiation needed.
    /// </summary>
    /// <typeparam name="T">The type of list the pool contains.</typeparam>
    public class ListPool<T> : PoolBase<List<T>> {

        /// A shared instance of the pool.
        public static readonly ListPool<T> Shared = new();

        /// <inheritdoc />
        protected override Func<List<T>> DefaultSupplier { get; }

        /// The default size of lists created by this pool.
        public int DefaultCapacity { get; set; } = 128;

        /// Creates a pool with no size limit.
        public ListPool() => DefaultSupplier = () => new List<T>(DefaultCapacity);

        /// <summary>
        /// Creates a pool with the given size limit.
        /// </summary>
        /// <param name="maxSize">The maximum size of the pool.</param>
        public ListPool(uint maxSize) : base(maxSize) => DefaultSupplier = () => new List<T>(((Func<int>) (() => DefaultCapacity))());

        /// <summary>
        /// Clears the given list.
        /// </summary>
        /// <param name="obj">The list to clear.</param>
        protected override void ResetObject(List<T> obj) {
            obj.Clear();
            obj.Capacity = 0;
        }

        /// <summary>
        /// Rents a list with the given capacity. 
        /// </summary>
        /// <param name="capacity">The capacity of the list.</param>
        public List<T> Rent(int capacity) {
            if (!TryDequeue(out var list))
                return new List<T>(capacity);
            list.Capacity = capacity;
            return list;
        }

        /// <summary>
        /// Rents a list with the elements in the given <paramref name="enumerable"/>.
        /// </summary>
        /// <param name="enumerable">The enumerable containing elements.</param>
        public List<T> Rent(IEnumerable<T> enumerable) => RentOrGet(() => {
            var list = new List<T>();
            if (enumerable != null)
                list.AddRange(enumerable);
            return list;
        });

        /// <summary>
        /// Clears the given list.
        /// </summary>
        /// <param name="obj">The list to clear.</param>
        public override void DisposeOfObject(List<T> obj) => ResetObject(obj);

    }

}
