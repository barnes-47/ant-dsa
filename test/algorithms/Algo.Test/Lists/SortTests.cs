using Algo.Lists.DataProvider;
using Algo.Lists.SortingAlgorithms;
using System;
using System.Collections.Generic;
using Xunit;

namespace Algo.Test.Lists
{
    public class SortTests
    {
        [Theory]
        [InlineData("5 4 3 2 1")]
        [InlineData("4 5 3 2 1")]
        [InlineData("3 4 5 2 1")]
        [InlineData("2 3 4 5 1")]
        [InlineData("1 2 3 4 5")]
        public void BubbleSort_SortsTheElementaryUnsortedList(
            string str)
        {
            var ints = Array.ConvertAll(str.Split(' '), int.Parse);
            var elements = new List<int>(ints);

            elements.BubbleSort();

            Assert.True(1 == elements[0]);
            Assert.True(2 == elements[1]);
            Assert.True(3 == elements[2]);
            Assert.True(4 == elements[3]);
            Assert.True(5 == elements[4]);
        }

        [Theory]
        [InlineData("5 4 3 2 1")]
        [InlineData("4 5 3 2 1")]
        [InlineData("3 4 5 2 1")]
        [InlineData("2 3 4 5 1")]
        [InlineData("1 2 3 4 5")]
        public void InsertionSort_SortsTheElementaryUnsortedList(
            string str)
        {
            var ints = Array.ConvertAll(str.Split(' '), int.Parse);
            var elements = new List<int>(ints);

            elements.InsertionSort();

            Assert.True(1 == elements[0]);
            Assert.True(2 == elements[1]);
            Assert.True(3 == elements[2]);
            Assert.True(4 == elements[3]);
            Assert.True(5 == elements[4]);
        }
    }
}
