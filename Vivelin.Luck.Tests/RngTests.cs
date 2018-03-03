using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Vivelin.Luck.Tests
{
    [TestClass]
    public class RngTests
    {
        [DataTestMethod]
        [DataRow(15, 30)]
        public void MultipleParellelThreadsGenerateUniqueSequences(int threadCount, int sequenceLength)
        {
            var results = new int[threadCount][];

            Parallel.For(0, results.Length, i => results[i] = GenerateArray(sequenceLength, Rng.Next));

            for (var i = 0; i < results.Length - 1; i++)
            {
                for (var j = i + 1; j < results.Length - 1; j++)
                {
                    CollectionAssert.AreNotEqual(results[i], results[j]);
                }
            }
        }

        private static T[] GenerateArray<T>(int size, Func<T> generator)
        {
            var result = new T[size];
            for (var i = 0; i < size; i++)
                result[i] = generator();
            return result;
        }
    }
}