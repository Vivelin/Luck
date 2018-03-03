using System;

namespace Vivelin.Luck
{
    /// <summary>
    /// Provides additional ways for generating pseudo-random numbers.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        /// Returns a non-negative random floating-point number that is less than
        /// the specified maximum.
        /// </summary>
        /// <param name="random">A pseudo-random number generator.</param>
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="maxValue"/> is less than zero.
        /// </exception>
        public static double Next(this Random random, double maxValue)
        {
            if (maxValue < 0)
                throw ArgumentLessThanZero(nameof(maxValue));

            return random.NextDouble() * maxValue;
        }

        /// <summary>
        /// Returns a random floating-point number that is within a specified range.
        /// </summary>
        /// <param name="random">A pseudo-random number generator.</param>
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
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="minValue"/> is greater than <paramref name="maxValue"/>.
        /// </exception>
        public static double Next(this Random random, double minValue, double maxValue)
        {
            if (minValue > maxValue)
                throw ArgumentOutOfRange(nameof(minValue), nameof(maxValue));

            var range = maxValue - minValue;
            return (random.NextDouble() * range) + minValue;
        }

        private static ArgumentOutOfRangeException ArgumentLessThanZero(string parameterName)
        {
            return new ArgumentOutOfRangeException(parameterName, string.Format(Exceptions.ArgumentMustBePositive, parameterName));
        }

        private static ArgumentOutOfRangeException ArgumentOutOfRange(string parameterName, string maxValueParameterName)
        {
            return new ArgumentOutOfRangeException(parameterName, string.Format(Exceptions.ArgumentMinMaxValue, parameterName, maxValueParameterName));
        }
    }
}