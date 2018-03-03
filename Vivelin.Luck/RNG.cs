using System;
using System.Threading;

namespace Vivelin.Luck
{
    /// <summary>
    /// Provides a static, thread-safe way to generate pseudo-random numbers.
    /// </summary>
    public static class Rng
    {
        private static readonly ThreadLocal<Random> localRandom = new ThreadLocal<Random>(Create);
        private static readonly Random seedGenerator = new Random();

        /// <summary>
        /// Gets a pseudo-random number generator that is unique for the current thread.
        /// </summary>
        public static Random Current => localRandom.Value;

        /// <summary>
        /// Creates a new instance of the <see cref="Random"/> class using a
        /// random seed.
        /// </summary>
        /// <returns>A new <see cref="Random"/> instance with a random seed.</returns>
        public static Random Create()
        {
            lock (seedGenerator)
            {
                var seed = seedGenerator.Next();
                return new Random(seed);
            }
        }

        /// <summary>
        /// Returns a non-negative random integer.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to zero and less than
        /// <see cref="int.MaxValue"/>.
        /// </returns>
        public static int Next() => Current.Next();

        /// <summary>
        /// Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated.
        /// <paramref name="maxValue"/> must be greater than or equal to 0.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer that is greater than or equal to 0, and less
        /// than <paramref name="maxValue"/>; that is, the range of return values
        /// ordinarily includes 0 but not <paramref name="maxValue"/>. However, if
        /// <paramref name="maxValue"/> equals 0, <paramref name="maxValue"/> is returned.
        /// </returns>
        public static int Next(int maxValue) => Current.Next(maxValue);

        /// <summary>
        /// Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">
        /// The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. <paramref
        /// name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.
        /// </param>
        /// <returns>
        /// A 32-bit signed integer greater than or equal to <paramref
        /// name="minValue"/> and less than <paramref name="maxValue"/>.
        /// </returns>
        public static int Next(int minValue, int maxValue) => Current.Next(minValue, maxValue);

        /// <summary>
        /// Returns a non-negative random floating-point number that is less than
        /// the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number to be generated.
        /// <paramref name="maxValue"/> must be greater than or equal to 0.
        /// </param>
        /// <returns>
        /// A double-precision floating-point number that is greater than or equal
        /// to 0, and less than <paramref name="maxValue"/>; that is, the range of
        /// return values ordinarily includes 0 but not <paramref
        /// name="maxValue"/>. However, if <paramref name="maxValue"/> equals 0,
        /// <paramref name="maxValue"/> is returned.
        /// </returns>
        public static double Next(double maxValue) => Current.Next(maxValue);

        /// <summary>
        /// Returns a random floating-point number that is within a specified range.
        /// </summary>
        /// <param name="minValue">
        /// The inclusive lower bound of the random number returned.
        /// </param>
        /// <param name="maxValue">
        /// The exclusive upper bound of the random number returned. <paramref
        /// name="maxValue"/> must be greater than or equal to <paramref name="minValue"/>.
        /// </param>
        /// <returns>
        /// A double-precision floating-point number greater than or equal to
        /// <paramref name="minValue"/> and less than <paramref name="maxValue"/>.
        /// </returns>
        public static double Next(double minValue, double maxValue) => Current.Next(minValue, maxValue);

        /// <summary>
        /// Returns a random floating-point number that is greater than or equal
        /// to 0.0, and less than 1.0.
        /// </summary>
        /// <returns>
        /// A double-precision floating point number that is greater than or equal
        /// to 0.0, and less than 1.0.
        /// </returns>
        public static double NextDouble() => Current.NextDouble();
    }
}