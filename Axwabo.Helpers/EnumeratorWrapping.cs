using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Axwabo.Helpers {

    /// <summary>
    /// Classes and methods for converting <see cref="IEnumerator">Enumerators</see> to <see cref="IEnumerable">Enumerables</see>.
    /// </summary>
    public static class EnumeratorWrapping {

        /// <summary>
        /// Wraps a non-generic <see cref="IEnumerator"/> into an enumerable.
        /// </summary>
        public readonly struct EnumeratorWrapper : IEnumerable {

            /// <summary>
            /// Creates a new <see cref="EnumeratorWrapper"/> instance.
            /// </summary>
            /// <param name="enumerator">The enumerator to wrap.</param>
            /// <remarks>The constructor is null-safe, it will use an empty Array enumerator if a null enumerator is supplied.</remarks>
            public EnumeratorWrapper(IEnumerator enumerator) => _enumerator = enumerator ?? Array.Empty<object>().GetEnumerator();

            private readonly IEnumerator _enumerator;

            /// <inheritdoc />
            public IEnumerator GetEnumerator() => _enumerator;

        }

        /// <summary>
        /// Wraps a generic <see cref="IEnumerator{T}"/> into an enumerable.
        /// </summary>
        public readonly struct GenericEnumeratorWrapper<T> : IEnumerable<T> {

            /// <summary>
            /// Creates a new <see cref="GenericEnumeratorWrapper{T}"/> instance.
            /// </summary>
            /// <param name="enumerator">The enumerator to wrap.</param>
            /// <remarks>The constructor is null-safe, it will use an empty Array enumerator if a null enumerator is supplied.</remarks>
            public GenericEnumeratorWrapper(IEnumerator<T> enumerator) => _enumerator = enumerator ?? Array.Empty<T>().AsEnumerable().GetEnumerator();

            private readonly IEnumerator<T> _enumerator;

            /// <inheritdoc />
            public IEnumerator<T> GetEnumerator() => _enumerator;

            /// <inheritdoc />
            IEnumerator IEnumerable.GetEnumerator() => _enumerator;

        }

        /// <summary>
        /// Converts a non-generic <see cref="IEnumerator"/> to an <see cref="IEnumerable"/>. 
        /// </summary>
        /// <param name="enumerator">The enumerator to wrap.</param>
        /// <returns>An <see cref="IEnumerable"/> that iterates through the given enumerator.</returns>
        public static IEnumerable ToEnumerable(this IEnumerator enumerator) => new EnumeratorWrapper(enumerator);

        /// <summary>
        /// Converts a generic <see cref="IEnumerator{T}"/> to an <see cref="IEnumerable{T}"/>. 
        /// </summary>
        /// <param name="enumerator">The enumerator to wrap.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that iterates through the given enumerator.</returns>
        public static IEnumerable<T> ToEnumerable<T>(this IEnumerator<T> enumerator) => new GenericEnumeratorWrapper<T>(enumerator);

        /// <summary>
        /// Further encapsulates the given <see cref="IEnumerable"/> by wrapping the <see cref="IEnumerator"/> returned by it into an <see cref="EnumeratorWrapper"/>.
        /// </summary>
        /// <param name="enumerable">The enumerable to wrap.</param>
        /// <returns>An <see cref="IEnumerable"/> that iterates through the enumerator of the given enumerable.</returns>
        /// <seealso cref="ToEnumerable"/>
        public static IEnumerable WrapEnumerable(this IEnumerable enumerable) => ToEnumerable(enumerable.GetEnumerator());


        /// <summary>
        /// Further encapsulates the given <see cref="IEnumerable{T}"/> by wrapping the <see cref="IEnumerator{T}"/> returned by it into a <see cref="GenericEnumeratorWrapper{T}"/>.
        /// </summary>
        /// <param name="enumerable">The enumerable to wrap.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> that iterates through the enumerator of the given enumerable.</returns>
        /// <seealso cref="ToEnumerable{T}"/>
        public static IEnumerable<T> WrapEnumerable<T>(this IEnumerable<T> enumerable) => ToEnumerable(enumerable.GetEnumerator());

    }

}
