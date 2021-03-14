namespace Ds.Test.Operation
{
    using Ds.Generic.LinkedList;
    using Ds.Operation;
    using Xunit;
    using Ds.Helper;
    using System.Collections.Generic;

    internal class Expected<T>
    {
        internal SinglyNode<T> Head { get; set; }
        internal SinglyNode<T> Tail { get; set; }
        internal int Count { get; set; }
    }

    internal static class SinglyLinkedListUtcExtension
    {
        internal static Expected<T> GetExpected<T>(this Singly<T> singly)
            => new Expected<T>
            {
                Count = singly.Count,
                Head = singly.Head,
                Tail = singly.Tail
            };
        internal static Expected<T> GetExpected<T>(this Singly<T> singly
            , T expectedHeadItem
            , T expectedTailItem)
            => new Expected<T>
            {
                Count = singly.Count,
                Head = singly.GetNode(expectedHeadItem),
                Tail = singly.GetNode(expectedTailItem)
            };
    }

    public class OnSinglyLinkedListUtc
    {
        #region Private Assert Methods
        private void ClearSingly<T>(params Singly<T>[] singlies)
        {
            foreach (var singly in singlies)
            {
                singly.Clear();
            }
        }
        private void AssertHeadTailReferencesAndCount<T>(
            Expected<T> expected
            , Singly<T> singly)
        {
            Assert.Equal(expected.Head, singly.Head);
            Assert.Equal(expected.Tail, singly.Tail);
            Assert.True(expected.Count == singly.Count);
        }
        private void AssertExpectedAndActualListContents<T>(
            Singly<T> expected
            , Singly<T> actual)
        {
            Assert.True(expected.Count == actual.Count);

            var comparer = EqualityComparer<T>.Default;
            var expectedCurrent = expected.Head;
            var actualCurrent = actual.Head;
            while (expectedCurrent != null && actualCurrent != null)
            {
                Assert.True(comparer.Equals(expectedCurrent.Item, actualCurrent.Item));
                expectedCurrent = expectedCurrent.Next;
                actualCurrent = actualCurrent.Next;
            }
        }
        #endregion

        [Theory]
        [InlineData("1")]
        [InlineData("1,1")]
        [InlineData("1,2,1")]
        [InlineData("1,2,3,4,3,2,1")]
        public void IsPalindrome_ReturnsTrue_WhenListIsPalindrome(
            string strList)
        {
            var singly = new Singly<int>(strList.ConvertToInts());
            var expected = singly.GetExpected();

            var actual = singly.IsPalindrome();

            Assert.True(actual);
            AssertHeadTailReferencesAndCount(expected, singly);
        }

        [Fact]
        public void IsPalindrome_ReturnsFalse_WhenListIsEmpty()
        {
            var singly = new Singly<string>();

            var actual = singly.IsPalindrome();

            Assert.False(actual);
        }

        [Theory]
        [InlineData("1,2")]
        [InlineData("1,2,2")]
        [InlineData("1,2,3,4,1,2,1")]
        public void IsPalindrome_ReturnsFalse_WhenListIsNotPalindrome(
            string strList)
        {
            var singly = new Singly<int>(strList.ConvertToInts());
            var expected = singly.GetExpected();

            var actual = singly.IsPalindrome();

            Assert.False(actual);
            AssertHeadTailReferencesAndCount(expected, singly);
        }

        [Theory]
        [InlineData("11", 11)]
        [InlineData("10,1", 10)]
        [InlineData("10,1,4,3,9,78,45,23,36,5", 9)]
        [InlineData("10,1,4,3,9,78,45,23,36,5", 10)]
        [InlineData("10,1,4,3,9,78,45,23,36,5", 5)]
        public void LoopExists_ReturnsTrue_WhenLoopExistsInTheList(
            string strList
            , int startOfLoop)
        {
            var singly = new Singly<int>(strList.ConvertToInts());
            singly.SetupLoop(startOfLoop);

            var actual = singly.LoopExists();

            Assert.True(actual);
        }

        [Fact]
        public void LoopExists_ReturnsFalse_WhenListIsEmpty()
        {
            var singly = new Singly<string>();

            var actual = singly.IsPalindrome();

            Assert.False(actual);
            Assert.True(singly.IsEmpty);
        }

        [Theory]
        [InlineData("10,1,4,3,9,78,45,23,36,5", 11)]
        public void LoopExists_ReturnsFalse_WhenLoopDoesNotExistsInTheList(
            string strList
            , int startOfLoop)
        {
            var singly = new Singly<int>(strList.ConvertToInts());
            singly.SetupLoop(startOfLoop);

            var actual = singly.LoopExists();

            Assert.False(actual);
        }

        [Fact]
        public void LoopLength_ReturnsZero_WhenListIsEmpty()
        {
            var singly = new Singly<int>();

            var actual = singly.LoopLength();

            Assert.True(0 == actual);
            Assert.True(singly.IsEmpty);
        }

        [Theory]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12")]
        public void LoopLength_ReturnsZero_WhenLoopDoesNotExistsInTheList(
            string actualStr)
        {
            var singly = new Singly<int>(actualStr.ConvertToInts());

            var actualLoopLength = singly.LoopLength();

            Assert.True(0 == actualLoopLength);
            Assert.False(singly.IsEmpty);
        }

        [Theory]
        [InlineData("1", 1, 1)]
        [InlineData("1,1,1", 1, 3)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11", 5, 7)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11", 4, 8)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 9, 4)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 8, 5)]
        public void LoopLength_ReturnsLengthOfLoop_WhenLoopExistsInTheList(
            string strList
            , int startOfLoop
            , int expectedLoopLength)
        {
            var singly = new Singly<int>(strList.ConvertToInts());
            var expected = singly.GetExpected();
            singly.SetupLoop(startOfLoop);

            var actualLoopLength = singly.LoopLength();

            Assert.True(expectedLoopLength == actualLoopLength);
            AssertHeadTailReferencesAndCount(expected, singly);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("1,1,1", 1)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11", 5)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11", 4)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 9)]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12", 8)]
        public void LoopStartNode_ReturnsStartNodeOfLoop_WhenLoopExists(
            string strList
            , int startOfLoop)
        {
            var singly = new Singly<int>(strList.ConvertToInts());
            var expected = singly.GetExpected();
            var expectedNode = singly.GetNode(startOfLoop);
            singly.SetupLoop(startOfLoop);

            var actualNode = singly.LoopStartNode();

            Assert.Equal(expectedNode, actualNode);
            Assert.True(expectedNode.Item == actualNode.Item);
            AssertHeadTailReferencesAndCount(expected, singly);
        }

        [Fact]
        public void LoopStartNode_ReturnsNull_WhenListIsEmpty()
        {
            var singly = new Singly<int>();

            var actualNode = singly.LoopStartNode();

            Assert.Null(actualNode);
        }

        [Theory]
        [InlineData("1,2,3,4,5,6,7,8,9,10,11,12")]
        public void LoopStartNode_ReturnsNull_WhenLoopDoesNotExists(
            string strList)
        {
            var singly = new Singly<int>(strList.ConvertToInts());
            var expected = singly.GetExpected();

            var actualNode = singly.LoopStartNode();

            Assert.Null(actualNode);
            AssertHeadTailReferencesAndCount(expected, singly);
        }

        [Theory]
        [InlineData("4,9,7,5", "4,5,7,9")]
        [InlineData("4,9,3,6,8,1,2,7,5", "1,2,3,4,5,6,7,8,9")]
        public void MergeSort_SortsTheNodesInAscendingOrder_WhenListIsNotSorted(
            string actualStr
            , string expectedStr)
        {
            var actualSingly = new Singly<int>(actualStr.ConvertToInts());
            var expectedSingly = new Singly<int>(expectedStr.ConvertToInts());

            actualSingly.MergeSort();

            AssertExpectedAndActualListContents(expectedSingly, actualSingly);
            expectedSingly.Clear();
            actualSingly.Clear();
        }

        [Theory]
        [InlineData("4,5,7,9", "1,2,3", "1,2,3,4,5,7,9")]   // Both are sorted.
        [InlineData("1,2,3,4", "6,7,8,9,10", "1,2,3,4,6,7,8,9,10")]
        public void MergeSort_ReturnsNewSortedList_WhenBothListsAreAlreadySorted(
            string actualStr1
            , string actualStr2
            , string expectedStr)
        {
            var actualSinglyOne = new Singly<int>(actualStr1.ConvertToInts());
            var actualSinglyTwo = new Singly<int>(actualStr2.ConvertToInts());
            var expectedSingly = new Singly<int>(expectedStr.ConvertToInts());

            var actualSingly = actualSinglyOne.MergeSort(actualSinglyTwo);

            AssertExpectedAndActualListContents(expectedSingly, actualSingly);
            ClearSingly(actualSingly, actualSinglyOne, actualSinglyTwo, expectedSingly);
        }

        [Theory]
        [InlineData("2,3,9,7", "1,5,4", "1,2,3,5,4,9,7")]   // None is sorted.
        public void MergeSort_ReturnsNewUnsortedList_WhenBothListsAreNotSorted(
            string actualStr1
            , string actualStr2
            , string expectedStr)
        {
            var actualSinglyOne = new Singly<int>(actualStr1.ConvertToInts());
            var actualSinglyTwo = new Singly<int>(actualStr2.ConvertToInts());
            var expectedSingly = new Singly<int>(expectedStr.ConvertToInts());

            var actualSingly = actualSinglyOne.MergeSort(actualSinglyTwo);

            AssertExpectedAndActualListContents(expectedSingly, actualSingly);
            ClearSingly(actualSingly, actualSinglyOne, actualSinglyTwo, expectedSingly);
        }

        [Fact]
        public void MergeSort_ReturnsNull_WhenBothListsAreEmpty()
        {
            var actualSinglyOne = new Singly<int>();
            var actualSinglyTwo = new Singly<int>();

            var actualSingly = actualSinglyOne.MergeSort(actualSinglyTwo);

            Assert.Null(actualSingly);
        }

        [Theory]
        [InlineData(null, "1,2,3,4,5", "1,2,3,4,5")]
        [InlineData("1,2,3,4,5", null, "1,2,3,4,5")]
        public void MergeSort_ReturnsNonEmptySortedList_WhenOneOfTheListIsEmpty(
            string actualStr1
            , string actualStr2
            , string expectedStr)
        {
            var actualSinglyOne = actualStr1.IsNullOrEmpty() ? new Singly<int>() : new Singly<int>(actualStr1.ConvertToInts());
            var actualSinglyTwo = actualStr2.IsNullOrEmpty() ? new Singly<int>() : new Singly<int>(actualStr2.ConvertToInts());
            var expectedSingly = new Singly<int>(expectedStr.ConvertToInts());

            var actualSingly = actualSinglyOne.MergeSort(actualSinglyTwo);

            AssertExpectedAndActualListContents(expectedSingly, actualSingly);
            if (!actualSinglyOne.IsEmpty)
                actualSinglyOne.Clear();
            if (!actualSinglyTwo.IsEmpty)
                actualSinglyTwo.Clear();

            ClearSingly(actualSingly, expectedSingly);
        }

        [Fact]
        public void MiddleNode_ReturnsNull_WhenListIsEmpty()
        {
            var singly = new Singly<int>();

            var actual = singly.MiddleNode();

            Assert.Null(actual);
            Assert.True(singly.IsEmpty);
        }

        [Theory]
        [InlineData("1", 1)]
        [InlineData("1,2", 1)]
        [InlineData("1,2,3", 2)]
        [InlineData("1,2,3,4", 2)]
        [InlineData("1,2,3,4,5", 3)]
        [InlineData("1,2,3,4,5,6", 3)]
        [InlineData("1,2,3,4,5,6,7", 4)]
        [InlineData("1,2,3,4,5,6,7,8", 4)]
        [InlineData("1,2,3,4,5,6,7,8,9", 5)]
        [InlineData("1,2,3,4,5,6,7,8,9,10", 5)]
        public void MiddleNode_ReturnsTheMiddleNode_WhenListIsNotEmpty(
            string actualStr
            , int expectedItem)
        {
            var singly = new Singly<int>(actualStr.ConvertToInts());
            var expected = singly.GetExpected();

            var actual = singly.MiddleNode();

            Assert.True(expectedItem == actual.Item);
            AssertHeadTailReferencesAndCount(expected, singly);
        }
    }
}
