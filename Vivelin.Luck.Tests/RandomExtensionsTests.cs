using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vivelin.Luck.Tests
{
    [TestClass]
    public class RandomExtensionsTests
    {
        [DataTestMethod]
        [DataRow(5000, -5, +5)]
        public void RandomDoublesAreWithingRange(int iterations, double minValue, double maxValue)
        {
            var rng = new Random();

            for (var i = 0; i < iterations; i++)
            {
                var value = rng.Next(minValue, maxValue);
                if (value < minValue)
                    Assert.Fail("The value was outside of the expected range. Minimum value: <{0}>. Actual: <{1}>.", minValue, value);
                if (value >= maxValue)
                    Assert.Fail("The value was outside of the expected range. Maximum value: <{0}>. Actual: <{1}>.", maxValue, value);
            }
        }

        [DataTestMethod]
        [DataRow(5000, 1.5d)]
        public void RandomPositiveDoublesAreWithinRange(int iterations, double maxValue)
        {
            var rng = new Random();

            for (var i = 0; i < iterations; i++)
            {
                var value = rng.Next(maxValue);
                if (value >= maxValue)
                    Assert.Fail("The value was outside of the expected range. Maximum value: <{0}>. Actual: <{1}>.", maxValue, value);
            }
        }
    }
}
