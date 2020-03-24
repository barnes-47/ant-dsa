using Ds.LinkedList;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Ds.Test.LinkedList
{
    public class SingleUnitTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(-111)]
        [InlineData(0)]
        [InlineData(5678)]
        public void AddFirst_ThrowsNullReferenceException_WhenNull(long testData)
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
        public void AddFirst_AddsOneNode_WhenEmpty(long testData)
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
        public void AddFirst_AddsMultipleNodes_WhenEmpty(string testStr)
        {
            var singly = new Singly();
            var mockData = Array.ConvertAll(testStr.Split(','), long.Parse);
            foreach(var d in mockData)
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
        public void AddLast_ThrowsNullReferenceException_WhenNull(long testData)
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
        public void AddLast_AddsOneNode_WhenEmpty(long testData)
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
        public void AddLast_AddsMultipleNodes_WhenEmpty(string testStr)
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
        [InlineData(1)]
        [InlineData(-111)]
        [InlineData(0)]
        [InlineData(5678)]
        public void AddAfter_ThrowsArgumentNullException_WhenNodeIsNull(long testData)
        {
            var singly = new Singly();
            var expectedEx = new ArgumentNullException("existingNode");
            Node testNode = null;

            var actualEx = Assert.Throws<ArgumentNullException>(() => singly.AddAfter(testNode, testData));

            Assert.True(expectedEx.ParamName == actualEx.ParamName);
            Assert.True(expectedEx.Message == actualEx.Message);
            Assert.True(expectedEx.GetType().FullName == actualEx.GetType().FullName);
        }

        [Theory]
        [InlineData(-44, 1)]
        [InlineData(20, -111)]
        [InlineData(55, 0)]
        [InlineData(876, 5678)]
        public void AddAfter_AddsTwoNodes_WhenEmpty(long expectedExistingData, long expectedNewData)
        {
            var singly = new Singly();
            var expectedLength = 2UL;
            var expectedExistingNode = new Node(expectedExistingData);

            singly.AddAfter(expectedExistingNode, expectedNewData);

            Assert.NotNull(singly.Head);
            Assert.NotNull(singly.Tail);
            Assert.False(singly.Head.Equals(singly.Tail));
            Assert.True(expectedLength == singly.Length);
            Assert.True(expectedExistingData == singly.Head.Data);
            Assert.True(expectedNewData == singly.Tail.Data);
        }
    }
}
