using Ds.LinkedList;
using System;
using Xunit;

namespace Ds.Test.LinkedList
{
    public class SingleUnitTest
    {
        #region Private Constants
        private const string ExpectedMessageWhenEmpty = "The singly linked list is empty.";
        #endregion

        #region Private Common Methods
        private long[] ConvertAllToLong(string commaSeparatedStr) => Array.ConvertAll(commaSeparatedStr.Split(','), long.Parse);
        private Node GetNode(Singly singly, long data)
        {
            var current = singly.Head;
            while(current != null)
            {
                if (current.Data == data)
                    return current;
                current = current.Next;
            }

            return null;
        }
        private void MakeItCircular(Singly singly, long data)
        {
            var node = GetNode(singly, data);
            if (node == null)
                return;
            singly.Tail.Next = node;
        }
        #endregion

        [Theory]
        [InlineData(1)]
        [InlineData(-111)]
        [InlineData(0)]
        [InlineData(5678)]
        public void AddFirst_ThrowsNullReferenceException_WhenNull(
            long testData)
        {
            Singly singly = null;
            var expectedEx = new NullReferenceException();

            var actualEx = Assert.Throws<NullReferenceException>(() => singly.AddFirst(testData));

            Assert.True(expectedEx.Message == actualEx.Message);
            Assert.True(expectedEx.GetType().FullName == actualEx.GetType().FullName);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-111)]
        [InlineData(0)]
        [InlineData(5678)]
        public void AddFirst_AddsOneNode_WhenEmpty(
            long testData)
        {
            var singly = new Singly();
            var length = 1UL;
            singly.AddFirst(testData);

            Assert.NotNull(singly);
            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(singly.Head.Equals(singly.Tail));
            Assert.True(singly.Head.Data == testData);
            Assert.True(singly.Tail.Data == testData);
            Assert.True(singly.Length == length);
        }

        [Theory]
        [InlineData("1,2,-3,4,0,-5,6,7,8,9")]
        [InlineData("3,8,1,5,-2,-9,7,6,0,4")]
        [InlineData("12,42,57,99,87,21,56,93,36,54")]
        public void AddFirst_AddsMultipleNodes_WhenEmpty(
            string testStr)
        {
            var singly = new Singly();
            var mockData = Array.ConvertAll(testStr.Split(','), long.Parse);
            foreach (var d in mockData)
            {
                singly.AddFirst(d);
            }

            Assert.Null(singly.Tail.Next);
            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.NotNull(singly.Head.Next);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(singly.Length == (ulong)mockData.Length);

            Array.Reverse(mockData);
            var current = singly.Head;
            foreach (var d in mockData)
            {
                Assert.True(current.Data == d);
                current = current.Next;
            }
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-111)]
        [InlineData(0)]
        [InlineData(5678)]
        public void AddLast_ThrowsNullReferenceException_WhenNull(
            long testData)
        {
            Singly singly = null;
            var expectedEx = new NullReferenceException();

            var actualEx = Assert.Throws<NullReferenceException>(() => singly.AddLast(testData));

            Assert.True(expectedEx.Message == actualEx.Message);
            Assert.True(expectedEx.GetType().FullName == actualEx.GetType().FullName);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(-111)]
        [InlineData(0)]
        [InlineData(5678)]
        public void AddLast_AddsOneNode_WhenEmpty(
            long testData)
        {
            var singly = new Singly();
            var length = 1UL;
            singly.AddLast(testData);

            Assert.NotNull(singly);
            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(singly.Head.Equals(singly.Tail));
            Assert.True(singly.Head.Data == testData);
            Assert.True(singly.Tail.Data == testData);
            Assert.True(singly.Length == length);
        }

        [Theory]
        [InlineData("1,2,-3,4,0,-5,6,7,8,9")]
        [InlineData("3,8,1,5,-2,-9,7,6,0,4")]
        [InlineData("12,42,57,99,87,21,56,93,36,54")]
        public void AddLast_AddsMultipleNodes_WhenEmpty(
            string testStr)
        {
            var singly = new Singly();
            var mockData = Array.ConvertAll(testStr.Split(','), long.Parse);
            foreach (var d in mockData)
            {
                singly.AddLast(d);
            }

            Assert.Null(singly.Tail.Next);
            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.NotNull(singly.Head.Next);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(singly.Length == (ulong)mockData.Length);

            var current = singly.Head;
            foreach (var d in mockData)
            {
                Assert.True(current.Data == d);
                current = current.Next;
            }
        }

        [Theory]
        [InlineData(22, 45)]
        [InlineData(656577, 43455)]
        [InlineData(2, 7890)]
        public void AddAfter_ThrowsException_WhenEmpty(
            long expextedExistingData
            , long expectedNewData)
        {
            var singly = new Singly();
            var expectedEx = new Exception(ExpectedMessageWhenEmpty);

            var actualEx = Assert.Throws<Exception>(() => singly.AddAfter(expextedExistingData, expectedNewData));

            Assert.True(expectedEx.Message == actualEx.Message);
            Assert.True(expectedEx.GetType().FullName == actualEx.GetType().FullName);
        }

        [Theory]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 1011, 4444)]
        [InlineData("67,2,-3,4,0,-5,6,7,8,9", 67, 7888)]
        [InlineData("3993,2,-3,4,0,-5,6,7,8,9", 3993, 3888)]
        public void AddAfter_AddsNewNodeAfterHead_WhenExistingDataIsEqualToHeadData(
            string fakeStr
            , long expectedExistingData
            , long expectedNewData)
        {
            var singly = new Singly(ConvertAllToLong(fakeStr));
            var expectedLength = singly.Length + 1UL;

            singly.AddAfter(expectedExistingData, expectedNewData);

            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(expectedLength == singly.Length);
            Assert.True(expectedExistingData == singly.Head.Data);
            Assert.True(expectedNewData == singly.Head.Next.Data);
        }

        [Theory]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 9, 4444)]
        [InlineData("67,2,-3,4,0,-5,6,7,8,9090", 9090, 7888)]
        [InlineData("3993,2,-3,4,0,-5,6,7,8,2323", 2323, 3888)]
        public void AddAfter_AddsNewNodeAfterTail_WhenExistingDataIsEqualToTailData(
            string fakeStr
            , long expectedExistingData
            , long expectedNewData)
        {
            var singly = new Singly(ConvertAllToLong(fakeStr));
            var expectedLength = singly.Length + 1UL;

            singly.AddAfter(expectedExistingData, expectedNewData);

            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(expectedLength == singly.Length);
            Assert.True(expectedNewData == singly.Tail.Data);

            var current = singly.Head.Next;
            while (current != null)
            {
                if (current.Next.Next == null)
                {
                    Assert.True(expectedExistingData == current.Data);
                    break;
                }
                current = current.Next;
            }
        }

        [Theory]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 2, 4444)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", -3, 232)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 4, 7474)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 0, 842)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", -5, 159)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 6, 7823940)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 7, 676686)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 8, 97097203)]
        public void AddAfter_AddsNewNodeAfterTheNodeContainingTheExistingData(
            string fakeStr
            , long expectedExistingData
            , long expectedNewData)
        {
            var singly = new Singly(ConvertAllToLong(fakeStr));
            var expectedLength = singly.Length + 1;

            singly.AddAfter(expectedExistingData, expectedNewData);

            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(expectedLength == singly.Length);

            var current = singly.Head;
            while (current != null)
            {
                if (current.Data == expectedExistingData)
                {
                    Assert.True(expectedExistingData == current.Data);
                    Assert.True(expectedNewData == current.Next.Data);
                    break;
                }
                current = current.Next;
            }
        }

        [Theory]
        [InlineData(22)]
        [InlineData(656577)]
        [InlineData(2)]
        public void AddAfterHead_ThrowsException_WhenEmpty(
            long fakeData)
        {
            var singly = new Singly();
            var expectedEx = new Exception("The singly linked list is empty.");

            var actualEx = Assert.Throws<Exception>(() => singly.AddAfterHead(fakeData));

            Assert.True(expectedEx.Message == actualEx.Message);
            Assert.True(expectedEx.GetType().FullName == actualEx.GetType().FullName);
        }

        [Theory]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 1011, 9, 7823940)]
        [InlineData("1011,2,-3,4,0,-5,6", 1011, 6, 666)]
        [InlineData("1011,2,-3,4", 1011, 4, 879)]
        public void AddAfterHead_AddsNewNodeAfterTheHeadNode(
            string fakeStr
            , long expectedHeadData
            , long expectedTailData
            , long expectedNewData)
        {
            var singly = new Singly(ConvertAllToLong(fakeStr));
            var expectedLength = singly.Length + 1;

            singly.AddAfterHead(expectedNewData);

            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(expectedLength == singly.Length);
            Assert.True(expectedHeadData == singly.Head.Data);
            Assert.True(expectedTailData == singly.Tail.Data);
            Assert.True(expectedNewData == singly.Head.Next.Data);
        }

        [Theory]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 1011, true)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 2, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", -3, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 4, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 0, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", -5, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 6, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 7, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 8, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 9, false)]
        public void IsHead_ReturnsFalse_WhenDataIsNotEqualToHeadData_AndTrue_WhenDataIsEqualToHeadData(
            string fakeStr
            , long testData
            , bool expectedResult)
        {
            var singly = new Singly(ConvertAllToLong(fakeStr));
            var expectedLength = singly.Length;
            var expectedHeadData = singly.Head.Data;

            var actualResult = singly.IsHead(testData);

            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(expectedLength == singly.Length);
            Assert.True(expectedResult == actualResult);

            if (expectedResult)
                Assert.True(testData == singly.Head.Data);
            else
                Assert.False(testData == singly.Head.Data);
        }

        [Theory]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 1011, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 2, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", -3, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 4, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 0, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", -5, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 6, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 7, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 8, false)]
        [InlineData("1011,2,-3,4,0,-5,6,7,8,9", 9, true)]
        public void IsTail_ReturnsFalse_WhenDataIsNotEqualToTailData_AndTrue_WhenDataIsEqualToTailData(
            string fakeStr
            , long testData
            , bool expectedResult)
        {
            var singly = new Singly(ConvertAllToLong(fakeStr));
            var expectedLength = singly.Length;
            var expectedTailData = singly.Tail.Data;

            var actualResult = singly.IsTail(testData);

            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(expectedLength == singly.Length);
            Assert.True(expectedResult == actualResult);

            if (expectedResult)
                Assert.True(testData == singly.Tail.Data);
            else
                Assert.False(testData == singly.Tail.Data);
        }

        [Theory]
        [InlineData("1011,2,-3,4,6", 1011, true)]
        [InlineData("1011,2,-3,4,6", 2, true)]
        [InlineData("1011,2,-3,4,6", -3, true)]
        [InlineData("1011,2,-3,4,6", 4, true)]
        [InlineData("1011,2,-3,4,6", 6, true)]
        [InlineData("1011,2,-3,4,6", 76, false)]
        [InlineData("1011,2,-3,4,6", 99999, false)]
        [InlineData("1011,2,-3,4,6", -13453, false)]
        [InlineData("1011,2,-3,4,6", -1, false)]
        [InlineData("1011,2,-3,4,6", 0, false)]
        public void Contains_ReturnsFalse_WhenDataDoesNotExistsInTheList_AndTrue_WhenDataExistsInTheList(
            string fakeStr
            , long testData
            , bool expectedResult)
        {
            var singly = new Singly(ConvertAllToLong(fakeStr));
            var expectedLength = singly.Length;
            var expectedHead = singly.Head;
            var expectedTail = singly.Tail;

            var actualResult = singly.Contains(testData);

            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.False(singly.IsNull);
            Assert.False(singly.IsEmpty);
            Assert.True(singly.Head.Equals(expectedHead));
            Assert.True(expectedHead.Data == singly.Head.Data);
            Assert.True(singly.Tail.Equals(expectedTail));
            Assert.True(expectedTail.Data == singly.Tail.Data);
            Assert.True(expectedLength == singly.Length);
            Assert.True(expectedResult == actualResult);

            if (expectedResult)
                Assert.True(actualResult);
            else
                Assert.False(actualResult);
        }

        [Theory]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,4,-5,6,7,9", 8)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,4,-5,6,8,9", 7)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,4,-5,7,8,9", 6)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,4,6,7,8,9", -5)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,-5,6,7,8,9", 4)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,4,-5,6,7,8,9", -3)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,-3,4,-5,6,7,8,9", 2)]
        public void Delete_DeletesTheSpecifiedDataNodeWhichIsNeitherHeadNorTailAndAtleastTwoNodesLeftAfterDeletion_WhenDataIsUnique(
            string actualStr
            , string expectedStr
            , long expectedDataToBeDeleted)
        {
            var actualSingly = new Singly(ConvertAllToLong(actualStr));
            var expectedSingly = new Singly(ConvertAllToLong(expectedStr));
            var expectedHead = actualSingly.Head;
            var expectedTail = actualSingly.Tail;
            var expectedLength = expectedSingly.Length;

            var actualIsDeleted = actualSingly.Delete(expectedDataToBeDeleted);

            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(expectedSingly.Head);
            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(expectedSingly.Tail);
            Assert.False(actualSingly.Head.Equals(actualSingly.Tail));
            Assert.False(expectedSingly.Head.Equals(expectedSingly.Tail));
            Assert.False(actualSingly.IsNull);
            Assert.False(actualSingly.IsEmpty);
            Assert.False(expectedSingly.IsNull);
            Assert.False(expectedSingly.IsEmpty);
            Assert.False(actualSingly.Contains(expectedDataToBeDeleted));
            Assert.True(actualIsDeleted);
            Assert.True(expectedHead.Equals(actualSingly.Head));
            Assert.True(expectedHead.Data == actualSingly.Head.Data);
            Assert.True(expectedTail.Equals(actualSingly.Tail));
            Assert.True(expectedTail.Data == expectedSingly.Tail.Data);
            Assert.True(expectedLength == actualSingly.Length);
            Assert.True(2 <= actualSingly.Length);
        }

        [Theory]
        [InlineData("1011,5,5,2,5,3,5,9", "1011,5,2,5,3,5,9", 5)]
        public void Delete_DeletesTheSpecifiedDataNodeWhichIsNeitherHeadNorTailAndAtleastTwoNodesLeftAfterDeletion_WhenDataIsNotUnique(
            string actualStr
            , string expectedStr
            , long expectedDataToBeDeleted)
        {
            var actualSingly = new Singly(ConvertAllToLong(actualStr));
            var expectedSingly = new Singly(ConvertAllToLong(expectedStr));
            var expectedHead = actualSingly.Head;
            var expectedTail = actualSingly.Tail;
            var expectedLength = expectedSingly.Length;
            var expectedNodeToBeDeleted = actualSingly.Head.Next;

            var actualIsDeleted = actualSingly.Delete(5);

            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(expectedSingly.Head);
            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(expectedSingly.Tail);

            Assert.False(actualSingly.Head.Equals(actualSingly.Tail));
            Assert.False(expectedSingly.Head.Equals(expectedSingly.Tail));
            Assert.False(actualSingly.IsNull);
            Assert.False(actualSingly.IsEmpty);
            Assert.False(expectedSingly.IsNull);
            Assert.False(expectedSingly.IsEmpty);

            Assert.True(!expectedNodeToBeDeleted.Equals(actualSingly.Head.Next) && expectedNodeToBeDeleted.Data == actualSingly.Head.Next.Data);

            Assert.True(actualIsDeleted);
            Assert.True(actualSingly.Contains(expectedDataToBeDeleted));
            Assert.True(expectedHead.Equals(actualSingly.Head));
            Assert.True(expectedHead.Data == actualSingly.Head.Data);
            Assert.True(expectedTail.Equals(actualSingly.Tail));
            Assert.True(expectedTail.Data == expectedSingly.Tail.Data);
            Assert.True(expectedLength == actualSingly.Length);
            Assert.True(2 <= actualSingly.Length);
        }

        [Theory]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "2,-3,4,-5,6,7,8,9", 1011, 2, 9)]
        [InlineData("1011,-3,9", "-3,9", 1011, -3, 9)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,4,-5,6,7,8", 9, 1011, 8)]
        [InlineData("1011,-3,9", "1011,-3", 9, 1011, -3)]
        public void Delete_DeletesTheHeadOrTailWithAtleastTwoNodeLeftAfterDeletion_WhenTheSpecifiedDataIsInHeadOrTail(
            string actualStr
            , string expectedStr
            , long expectedDataToBeDeleted
            , long expectedHeadData
            , long expectedTailData)
        {
            var actualSingly = new Singly(ConvertAllToLong(actualStr));
            var expectedSingly = new Singly(ConvertAllToLong(expectedStr));
            var expectedHead = GetNode(actualSingly, expectedHeadData);
            var expectedTail = GetNode(actualSingly, expectedTailData);

            var actualResult = actualSingly.Delete(expectedDataToBeDeleted);

            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(expectedSingly.Head);
            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(expectedSingly.Tail);

            Assert.False(actualSingly.Head.Equals(actualSingly.Tail));
            Assert.False(expectedSingly.Head.Equals(expectedSingly.Tail));
            Assert.False(actualSingly.IsNull);
            Assert.False(actualSingly.IsEmpty);
            Assert.False(expectedSingly.IsNull);
            Assert.False(expectedSingly.IsEmpty);
            Assert.False(actualSingly.Contains(expectedDataToBeDeleted));

            Assert.True(actualResult);            
            Assert.True(expectedHead.Equals(actualSingly.Head));
            Assert.True(expectedHead.Data == actualSingly.Head.Data);
            Assert.True(expectedTail.Equals(actualSingly.Tail));
            Assert.True(expectedTail.Data == expectedSingly.Tail.Data);
            Assert.True(expectedSingly.Length == actualSingly.Length);
            Assert.True(2 <= actualSingly.Length);
        }

        [Theory]
        [InlineData("1,2", "1", 2, 1, 1)]
        [InlineData("1,2", "2", 1, 2, 2)]
        public void Delete_DeletesTheHeadOrTailWithOnlyOneNodeLeftAfterDeletion_WhenSpecifiedDataIsInHeadOrTailAndLengthIsTwo(
            string actualStr
            , string expectedStr
            , long expectedDataToBeDeleted
            , long expectedHeadData
            , long expectedTailData)
        {
            var actualSingly = new Singly(ConvertAllToLong(actualStr));
            var expectedSingly = new Singly(ConvertAllToLong(expectedStr));
            var expectedHead = GetNode(actualSingly, expectedHeadData);
            var expectedTail = GetNode(actualSingly, expectedTailData);

            var actualResult = actualSingly.Delete(expectedDataToBeDeleted);

            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(expectedSingly.Head);
            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(expectedSingly.Tail);

            Assert.False(actualSingly.IsNull);
            Assert.False(actualSingly.IsEmpty);
            Assert.False(expectedSingly.IsNull);
            Assert.False(expectedSingly.IsEmpty);
            Assert.False(actualSingly.Contains(expectedDataToBeDeleted));

            Assert.True(actualResult);
            Assert.True(actualSingly.Head.Equals(actualSingly.Tail));
            Assert.True(expectedSingly.Head.Equals(expectedSingly.Tail));
            Assert.True(expectedHead.Equals(actualSingly.Head));
            Assert.True(expectedHead.Data == actualSingly.Head.Data);
            Assert.True(expectedTail.Equals(actualSingly.Tail));
            Assert.True(expectedTail.Data == expectedSingly.Tail.Data);
            Assert.True(expectedSingly.Length == actualSingly.Length);
            Assert.True(1 == actualSingly.Length);
        }

        [Fact]
        public void Delete_DeletesTheHeadOrTailAndRendersTheListEmpty_WhenOnlyOneNodeExists()
        {
            var actualData = 100;
            var actualSingly = new Singly(actualData);

            var actualIsDeleted = actualSingly.Delete(actualData);

            Assert.Null(actualSingly.Head);
            Assert.Null(actualSingly.Tail);

            Assert.False(actualSingly.IsNull);

            Assert.True(actualIsDeleted);
            Assert.True(actualSingly.IsEmpty);
            Assert.True(0 == actualSingly.Length);
        }

        [Theory]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,4,-5,6,7,8,9", 100)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,4,-5,6,7,8,9", -200)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", "1011,2,-3,4,-5,6,7,8,9", 44)]
        public void Delete_DoesNotDeletes_WhenSpecifiedDataDoesNotExists(
            string actualStr
            , string expectedStr
            , long dataToBeDeleted)
        {
            var actualSingly = new Singly(ConvertAllToLong(actualStr));
            var expectedSingly = new Singly(ConvertAllToLong(expectedStr));
            var expectedHead = actualSingly.Head;
            var expectedTail = actualSingly.Tail;

            var actualIsDeleted = actualSingly.Delete(dataToBeDeleted);

            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(actualSingly.Tail);
            Assert.NotNull(expectedSingly.Head);
            Assert.NotNull(expectedSingly.Tail);

            Assert.False(actualIsDeleted);
            Assert.False(actualSingly.Head.Equals(actualSingly.Tail));
            Assert.False(actualSingly.IsNull);
            Assert.False(actualSingly.IsEmpty);
            Assert.False(expectedSingly.Head.Equals(expectedSingly.Tail));
            Assert.False(expectedSingly.IsNull);
            Assert.False(expectedSingly.IsEmpty);

            Assert.True(expectedHead.Equals(actualSingly.Head));
            Assert.True(expectedTail.Equals(actualSingly.Tail));
            Assert.True(expectedSingly.Length == actualSingly.Length);

            var actualCurrent = actualSingly.Head;
            var expectedCurrent = expectedSingly.Head;
            while(expectedCurrent != null && actualCurrent != null)
            {
                Assert.True(expectedCurrent.Data == actualCurrent.Data);
                expectedCurrent = expectedCurrent.Next;
                actualCurrent = actualCurrent.Next;
            }
        }

        [Theory]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", 9)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", 8)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", 7)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", 6)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", -5)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", 4)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", -3)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", 2)]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", 1011)]
        public void HasLoop_ReturnsTrue_WhenTheTailPointsToAnotherNodeInTheList(
            string actualStr
            , long nodeDataToWhichTailPointsTo)
        {
            var actualSingly = new Singly(ConvertAllToLong(actualStr));
            var expectedTailPointsToNode = GetNode(actualSingly, nodeDataToWhichTailPointsTo);
            var expectedHead = actualSingly.Head;
            var expectedTail = actualSingly.Tail;
            var expectedLength = actualSingly.Length;
            MakeItCircular(actualSingly, nodeDataToWhichTailPointsTo);

            var actualHasLoop = actualSingly.HasLoop;

            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(actualSingly.Tail);

            Assert.False(actualSingly.IsNull);
            Assert.False(actualSingly.IsEmpty);

            Assert.True(expectedHead.Equals(actualSingly.Head));
            Assert.True(expectedTail.Equals(actualSingly.Tail));
            Assert.True(expectedTailPointsToNode.Equals(actualSingly.Tail.Next));
            Assert.True(expectedHead.Data == actualSingly.Head.Data);
            Assert.True(expectedTail.Data == actualSingly.Tail.Data);
            Assert.True(expectedTailPointsToNode.Data == actualSingly.Tail.Next.Data);
            Assert.True(expectedLength == actualSingly.Length);
            Assert.True(actualHasLoop);
        }

        [Theory]
        [InlineData("1011,2,-3,4,-5,6,7,8,9", 0)]
        public void HasLoop_ReturnsFalse_WhenTheTailPointsToNull(
            string actualStr
            , long nodeDataToWhichTailPointsTo)
        {
            var actualSingly = new Singly(ConvertAllToLong(actualStr));
            var expectedTailPointsToNode = GetNode(actualSingly, nodeDataToWhichTailPointsTo);
            var expectedHead = actualSingly.Head;
            var expectedTail = actualSingly.Tail;
            var expectedLength = actualSingly.Length;
            MakeItCircular(actualSingly, nodeDataToWhichTailPointsTo);

            var actualHasLoop = actualSingly.HasLoop;

            Assert.Null(expectedTailPointsToNode);

            Assert.NotNull(actualSingly.Head);
            Assert.NotNull(actualSingly.Tail);

            Assert.False(actualSingly.IsNull);
            Assert.False(actualSingly.IsEmpty);
            Assert.False(actualHasLoop);

            Assert.True(expectedHead.Equals(actualSingly.Head));
            Assert.True(expectedTail.Equals(actualSingly.Tail));
            Assert.True(expectedHead.Data == actualSingly.Head.Data);
            Assert.True(expectedTail.Data == actualSingly.Tail.Data);
            Assert.True(expectedLength == actualSingly.Length);
        }
    }
}
