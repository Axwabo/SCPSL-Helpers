﻿using System;
using System.Collections.Generic;

namespace Axwabo.Helpers.Pools {

    /// <summary>
    /// A base pool containing objects of type <typeparamref name="T"/>, allowing for renting from and returning objects to it, thus reducing the amount of object instantiation needed.
    /// </summary>
    /// <typeparam name="T">The type of object the pool contains.</typeparam>
    /// <seealso cref="BasicPool{T}"/>
    public abstract class PoolBase<T> : IDisposable {

        /// <summary>
        /// Creates an object instance.
        /// </summary>
        /// <returns>A new object of type <typeparamref name="T"/>.</returns>
        protected abstract Func<T> DefaultSupplier { get; }

        private readonly Queue<T> _queue = new();

        /// Creates a pool with no size limit.
        public PoolBase() => MaxSize = 0;

        /// <summary>
        /// Creates a pool with the given size limit.
        /// </summary>
        /// <param name="maxSize">The maximum size of the pool.</param>
        public PoolBase(uint maxSize) => MaxSize = maxSize;

        /// <summary>
        /// The maximum amount of elements that can be stored in the pool.
        /// </summary>
        public uint MaxSize { get; }

        /// <summary>
        /// Pulls an element from the pool.
        /// </summary>
        /// <returns>An object of type <typeparamref name="T"/>.</returns>
        public virtual T Rent() => RentOrGet(DefaultSupplier);

        /// <summary>
        /// Pulls an element from the pool or uses the given <paramref name="supplier"/> to obtain an object.
        /// </summary>
        /// <param name="supplier">A function to create an object.</param>
        /// <returns>An object of type <typeparamref name="T"/>.</returns>
        protected T RentOrGet(Func<T> supplier) => TryDequeue(out var obj) ? obj : supplier();

        /// <summary>
        /// Returns an element to the pool and resets it.
        /// </summary>
        /// <param name="obj">The object to return.</param>
        public void Return(T obj) {
            if (MaxSize == 0 || _queue.Count >= MaxSize)
                DisposeOfObject(obj);
            else {
                ResetObject(obj);
                _queue.Enqueue(obj);
            }
        }

        /// <summary>
        /// a
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected bool TryDequeue(out T obj) => _queue.TryDequeue(out obj);

        /// <summary>
        /// Attempts to reset an object to its default state.
        /// </summary>
        /// <param name="obj">The object to reset.</param>
        protected virtual void ResetObject(T obj) {
            if (obj is IPoolResettable disposable)
                disposable.Reset(obj);
        }

        /// <summary>
        /// Attempts to destroy an object.
        /// </summary>
        /// <param name="obj">The object to dispose of.</param>
        public virtual void DisposeOfObject(T obj) {
            if (obj is IPoolResettable disposable)
                disposable.Destroy(obj);
        }

        /// <inheritdoc />
        public void Dispose() => _queue.Clear();

        ~PoolBase() => Dispose();

    }

}
