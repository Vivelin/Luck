using System;
using System.Collections.Generic;
using System.Linq;

namespace Vivelin.Luck
{
    /// <summary>
    /// Provides a set of static methods for randomly selecting elements from a list.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Returns a random element from a sequence of weighted values.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <returns>A random element from <paramref name="source"/>.</returns>
        public static T WeightedSample<T>(this IEnumerable<T> source) where T : IWeighted
        {
            return WeightedSample(source, Rng.Current);
        }

        /// <summary>
        /// Returns a random element from a sequence of values.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <returns>A random element from <paramref name="source"/>.</returns>
        public static T Sample<T>(this IEnumerable<T> source)
        {
            return Sample(source, Rng.Current);
        }

        /// <summary>
        /// Returns a random element from a sequence of values.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <returns>A random element from <paramref name="source"/>.</returns>
        public static T Sample<T>(this IList<T> source)
        {
            return Sample(source, Rng.Current);
        }

        /// <summary>
        /// Returns a random element from a sequence of weighted values using the
        /// specified random number generator.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="random">
        /// A pseudo-random number generator used to select an element.
        /// </param>
        /// <returns>A random element from <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="random"/> is <c>null</c>.</exception>
        public static T WeightedSample<T>(this IEnumerable<T> source, Random random) where T : IWeighted
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (random == null)
                throw new ArgumentNullException(nameof(random));

            if (!source.Any())
                return default(T);

            var totalWeight = source.Sum(x => x?.Weight ?? 0);
            var targetWeight = random.Next(totalWeight);

            var runningTotal = 0d;
            foreach (var element in source)
            {
                runningTotal += element?.Weight ?? 0;
                if (runningTotal > targetWeight)
                    return element;
            }

            throw new UnreachableException();
        }

        /// <summary>
        /// Returns a random element from a sequence of values using the
        /// specified random number generator.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="random">
        /// A pseudo-random number generator used to select an element.
        /// </param>
        /// <returns>A random element from <paramref name="source"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="random"/> is <c>null</c>.</exception>
        public static T Sample<T>(this IEnumerable<T> source, Random random)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (random == null)
                throw new ArgumentNullException(nameof(random));

            var count = source.Count();
            if (count == 0)
                return default(T);

            var target = random.Next(count);

            var runningTotal = 0;
            foreach (var element in source)
            {
                runningTotal++;
                if (runningTotal > target)
                    return element;
            }

            throw new UnreachableException();
        }

        /// <summary>
        /// Returns a random element from a sequence of values using the
        /// specified random number generator.
        /// </summary>
        /// <typeparam name="T">The type of elements in the sequence.</typeparam>
        /// <param name="source">A sequence of values.</param>
        /// <param name="random">
        /// A pseudo-random number generator used to select an element.
        /// </param>
        /// <returns>A random element from <paramref name="source"/>, or a default value if <paramref name="source"/> is empty.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="source"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException"><paramref name="random"/> is <c>null</c>.</exception>
        public static T Sample<T>(this IList<T> source, Random random)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (random == null)
                throw new ArgumentNullException(nameof(random));

            if (source.Count == 0)
                return default(T);

            var target = random.Next(source.Count);
            return source[target];
        }
    }
}