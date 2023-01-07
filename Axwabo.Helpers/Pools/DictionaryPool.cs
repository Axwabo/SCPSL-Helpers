using System;
using System.Collections.Generic;

namespace Axwabo.Helpers.Pools {

    /// <summary>
    /// A pool containing dictionaries of type <typeparamref name="TKey"/> and <typeparamref name="TValue"/>, allowing for renting from and returning objects to it, thus reducing the amount of object instantiation needed.
    /// </summary>
    /// <typeparam name="TKey">The type of key the pool contains.</typeparam>
    /// <typeparam name="TValue">The type of value the pool contains.</typeparam>
    public class DictionaryPool<TKey, TValue> : PoolBase<Dictionary<TKey, TValue>> {

        /// <summary>A shared instance of the pool.</summary>
        public static readonly DictionaryPool<TKey, TValue> Shared = new();

        /// <inheritdoc />
        protected override Func<Dictionary<TKey, TValue>> DefaultSupplier { get; }

        /// <summary>The default size of dictionaries created by this pool.</summary>
        public int DefaultCapacity { get; set; } = 128;

        /// <summary>Creates a pool with no size limit.</summary>
        public DictionaryPool() => DefaultSupplier = () => new Dictionary<TKey, TValue>(DefaultCapacity);

        /// <summary>
        /// Creates a pool with the given size limit.
        /// </summary>
        /// <param name="maxSize">The maximum size of the pool.</param>
        public DictionaryPool(uint maxSize) : base(maxSize) => DefaultSupplier = () => new Dictionary<TKey, TValue>(((Func<int>) (() => DefaultCapacity))());

        /// <summary>
        /// Clears the given dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to clear.</param>
        protected override void ResetObject(Dictionary<TKey, TValue> dictionary) => dictionary.Clear();

        /// <summary>
        /// Rents a dictionary with the pairs in the given <paramref name="enumerable"/>.
        /// </summary>
        /// <param name="enumerable">The enumerable containing pairs.</param>
        /// <returns>A dictionary with the elements in the given enumerable.</returns>
        public Dictionary<TKey, TValue> Rent(IEnumerable<KeyValuePair<TKey, TValue>> enumerable) => RentOrGet(DefaultSupplier, dict => {
            if (enumerable == null) 
                return;
            foreach (var pair in enumerable)
                dict.Add(pair.Key, pair.Value);
        });

        /// <summary>
        /// Clears the given dictionary.
        /// </summary>
        /// <param name="dictionary">The dictionary to clear.</param>
        public override void DisposeOfObject(Dictionary<TKey, TValue> dictionary) => ResetObject(dictionary);

    }

}
