namespace Algo.Test.Lists
{
    using Algo.Lists.SearchAlgorithms;
    using System.Collections.Generic;
    using Xunit;

    public class SearchTests
    {
        /// <summary>
        /// Tests the binary search on a sorted list of ints.
        /// Method being tested returns index of the item being searched.
        /// </summary>
        [Theory]
        [InlineData(16, 15)]
        [InlineData(10, 9)]
        public void BinarySearch_Returns_Index_When_ItemExistsInTheList(
            int item,
            int expectedIndex)
        {
            IList<int> elements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedCount = elements.Count;

            var actual = elements.BinarySearch(item);

            Assert.True(expectedIndex == actual.Index);
            Assert.True(expectedCount == elements.Count);
        }

        /// <summary>
        /// Tests the binary search on a sorted list of ints.
        /// Method being tested returns -1, as the item being searched was not found.
        /// </summary>
        [Theory]
        [InlineData(20, -1)]
        [InlineData(-100, -1)]
        [InlineData(0, -1)]
        public void BinarySearch_Returns_NegativeInt_When_ItemDoesNotExistsInTheList(
            int item,
            int expectedIndex)
        {
            IList<int> elements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedCount = elements.Count;

            var actual = elements.BinarySearch(item);

            Assert.True(expectedIndex == actual.Index);
            Assert.True(expectedCount == elements.Count);
        }

        /// <summary>
        /// Tests the jumpinary search on a sorted list of ints.
        /// Method being tested returns the index of the item being searched.
        /// </summary>
        [Theory]
        [InlineData(16, 15)]
        [InlineData(10, 9)]
        public void JumpinarySearch_Returns_IndexOfTheItem_When_ItemExistsInTheList(
            int item,
            int expectedIndex)
        {
            IList<int> elements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedCount = elements.Count;

            var actual = elements.JumpinarySearch(item);

            Assert.True(expectedIndex == actual.Index);
            Assert.True(expectedCount == elements.Count);
        }

        /// <summary>
        /// Tests the jumpinary search on a sorted list of ints.
        /// Method being tested returns -1, as the item being searched was not found.
        /// </summary>
        [Theory]
        [InlineData(20, -1)]
        [InlineData(-100, -1)]
        [InlineData(0, -1)]
        public void JumpinarySearch_Returns_NegativeInt_When_ItemDoesNotExistsInTheList(
            int item,
            int expectedIndex)
        {
            IList<int> elements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedCount = elements.Count;

            var actual = elements.JumpinarySearch(item);

            Assert.True(expectedIndex == actual.Index);
            Assert.True(expectedCount == elements.Count);   //Assert that the list has not been modified.
        }

        /// <summary>
        /// Tests the jump/block search on a sorted list of ints.
        /// Method being tested returns index of the item being searched.
        /// </summary>
        [Theory]
        [InlineData(16, 15)]
        [InlineData(10, 9)]
        public void JumpSearch_Returns_Index_When_ItemExistsInTheList(
            int item,
            int expectedIndex)
        {
            IList<int> elements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedCount = elements.Count;

            var actual = elements.JumpSearch(item);

            Assert.True(expectedIndex == actual.Index);
            Assert.True(expectedCount == elements.Count);
        }

        /// <summary>
        /// Tests the jump/block search on a sorted list of ints.
        /// Method being tested returns -1, as the item being searched was not found.
        /// </summary>
        [Theory]
        [InlineData(20, -1)]
        [InlineData(-100, -1)]
        [InlineData(0, -1)]
        public void JumpSearch_Returns_NegativeInt_When_ItemDoesNotExistsInTheList(
            int item,
            int expectedIndex)
        {
            IList<int> elements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedCount = elements.Count;

            var actual = elements.JumpSearch(item);

            Assert.True(expectedIndex == actual.Index);
            Assert.True(expectedCount == elements.Count);   //Assert that the list has not been modified.
        }

        /// <summary>
        /// Tests the jump/block search on a sorted list of ints.
        /// Method being tested returns index of the item being searched.
        /// </summary>
        [Theory]
        [InlineData(16, 15)]
        [InlineData(10, 9)]
        public void LinearSearch_Returns_Index_When_ItemExistsInTheSortedList(
            int item,
            int expectedIndex)
        {
            IList<int> elements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedCount = elements.Count;

            var actual = elements.LinearSearch(item);

            Assert.True(expectedIndex == actual.Index);
            Assert.True(expectedCount == elements.Count);   //Assert that the list has not been modified.
        }

        /// <summary>
        /// Tests the linear search on a sorted list of ints.
        /// Method being tested returns -1, as the item being searched was not found.
        /// </summary>
        [Theory]
        [InlineData(20, -1)]
        [InlineData(-100, -1)]
        [InlineData(0, -1)]
        public void LinearSearch_Returns_NegativeInt_When_ItemDoesNotExistsInTheSortedList(
            int item,
            int expectedIndex)
        {
            IList<int> elements = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 };
            var expectedCount = elements.Count;

            var actual = elements.LinearSearch(item);

            Assert.True(expectedIndex == actual.Index);
            Assert.True(expectedCount == elements.Count);   //Assert that the list has not been modified.
        }
    }
}
