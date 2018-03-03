using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vivelin.Luck.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void SampleSelectsHigherWeightedObjectsMoreFrequently()
        {
            var iterations = 5000;
            var collection = new[]
            {
                new WeightedValue(0.01),
                new WeightedValue(0.1),
                new WeightedValue(10),
                new WeightedValue(100)
            };
            var results = new SortedDictionary<double, int>();
            foreach (var item in collection)
            {
                results.Add(item.Weight, 0);
            }

            var random = new Random();
            for (var i = 0; i < iterations; i++)
            {
                var value = collection.Sample(random);
                results[value.Weight]++;
            }

            // SortedDictionary sorts on key. Values are in the same order, so we
            // only need to check if values are sorted too.
            Assert.IsTrue(IsSorted(results.Values), string.Join(", ", results.Values));
        }

        private static bool IsSorted<T>(IEnumerable<T> enumerable) where T : IComparable<T>
        {
            var prev = default(T);
            var prevSet = false;
            foreach (var item in enumerable)
            {
                if (prevSet && prev.CompareTo(item) > 0)
                    return false;
                prev = item;
                prevSet = true;
            }
            return true;
        }

        /// <summary>
        /// Represents a weighted integer.
        /// </summary>
        private struct WeightedValue : IWeighted
        {
            private double weight;

            public WeightedValue(double weight)
            {
                this.weight = weight;
            }

            public double Weight => weight;
        }
    }
}