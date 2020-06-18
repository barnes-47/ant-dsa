namespace Algo.Test.Lists
{
    using Algo.Lists.SortingAlgorithms;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class SortTests
    {
        [Theory]
        [InlineData("5 4 3 2 1")]
        [InlineData("4 5 3 2 1")]
        [InlineData("3 4 5 2 1")]
        [InlineData("2 3 4 5 1")]
        [InlineData("1 2 3 4 5")]
        public void BubbleSort_SortsTheUnsortedList(
            string str)
        {
            var actual = Array.ConvertAll(str.Split(' '), int.Parse).ToList();
            var expected = new List<int> { 1, 2, 3, 4, 5 };

            actual.BubbleSort();

            for (var i = 0; i < expected.Count; ++i)
            {
                Assert.True(expected[i] == actual[i]);
            }
        }

        [Theory]
        [InlineData("5 4 3 2 1")]
        [InlineData("4 5 3 2 1")]
        [InlineData("3 4 5 2 1")]
        [InlineData("2 3 4 5 1")]
        [InlineData("1 2 3 4 5")]
        public void InsertionSort_SortsTheUnsortedList(
            string str)
        {
            var actual = Array.ConvertAll(str.Split(' '), int.Parse).ToList();
            var expected = new List<int> { 1, 2, 3, 4, 5 };

            actual.InsertionSort();

            for (var i = 0; i < expected.Count; ++i)
            {
                Assert.True(expected[i] == actual[i]);
            }
        }

        [Theory]
        [InlineData("5 4 3 2 1")]
        [InlineData("4 5 3 2 1")]
        [InlineData("3 4 5 2 1")]
        [InlineData("2 3 4 5 1")]
        [InlineData("1 2 3 4 5")]
        public void MergeSort_SortsAnUnsortedList(string str)
        {
            var actual = Array.ConvertAll(str.Split(' '), int.Parse).ToList();
            var expected = new List<int> { 1, 2, 3, 4, 5 };

            actual.MergeSort();

            for (var i = 0; i < expected.Count; ++i)
            {
                Assert.True(expected[i] == actual[i]);
            }
        }
    }
}
