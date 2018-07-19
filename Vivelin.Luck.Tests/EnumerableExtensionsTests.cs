using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vivelin.Luck.Tests
{
    [TestClass]
    public class EnumerableExtensionsTests
    {
        [TestMethod]
        public void WeightedSampleSelectsHigherWeightedObjectsMoreFrequently()
        {
            var iterations = 1_000_000;
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
                var value = collection.WeightedSample(random);
                results[value.Weight]++;
            }

            // SortedDictionary sorts on key. Values are in the same order, so we
            // only need to check if values are sorted too.
            Assert.IsTrue(IsSorted(results.Values), string.Join(", ", results.Values));
        }

        [TestMethod]
        public void SampleReturnsDefaultValueOnEmptyCollections()
        {
            var collection = new WeightedObject[0];
            Assert.IsNull(collection.Sample());
        }

        [TestMethod]
        public void SampleReturnsDifferentElementsWhenCalledRepeatedly()
        {
            var collection = new[]
            {
                new WeightedValue(0.01),
                new WeightedValue(0.1),
                new WeightedValue(10),
                new WeightedValue(100)
            };

            var initial = collection.Sample();
            for (var i = 0; i < 1000; i++)
            {
                if (initial.Weight != collection.Sample().Weight)
                    return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void SampleReturnsDifferentElementsWhenCalledRepeatedlyInSequence()
        {
            var collection = new[]
            {
                new WeightedValue(0.01),
                new WeightedValue(0.1),
                new WeightedValue(10),
                new WeightedValue(100)
            };

            var initial = ((IEnumerable<WeightedValue>)collection).Sample();
            for (var i = 0; i < 1000; i++)
            {
                if (initial.Weight != ((IEnumerable<WeightedValue>)collection).Sample().Weight)
                    return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void SampleReturnsDefaultValueOnEmptySequences()
        {
            var collection = new WeightedObject[0];
            Assert.IsNull(((IEnumerable<WeightedObject>)collection).Sample());
        }

        [TestMethod]
        public void WeightedSampleReturnsDefaultValueOnEmptyCollections()
        {
            var collection = new WeightedObject[0];
            Assert.IsNull(collection.WeightedSample());
        }

        [TestMethod]
        public void WeightedSampleSkipsOverNullReferences()
        {
            const int expectedWeight = 100;
            var collection = new[]
            {
                new WeightedObject(0),
                null,
                new WeightedObject(expectedWeight)
            };

            var random = new Random(0);
            var value = collection.WeightedSample(random);
            Assert.AreEqual(expectedWeight, value.Weight);
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

        private class WeightedObject : IWeighted
        {
            public WeightedObject(double weight)
            {
                Weight = weight;
            }

            public double Weight { get; }
        }
    }
}