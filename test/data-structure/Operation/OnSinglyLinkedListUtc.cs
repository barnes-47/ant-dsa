using Castle.Core.Internal;
using Ds.Generic.LinkedList;
using Ds.Operation;
using Moq;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Xunit;

namespace Ds.Test.Operation
{
    public class OnSinglyLinkedListUtc
    {
        #region Private Class
        internal class Expected<T>
        {
            internal SinglyNode<T> Head { get; set; }
            internal SinglyNode<T> Tail { get; set; }
            internal int Count { get; set; }
        }
        #endregion

        #region Private Helper Methods
        private static Expected<T> GetExpected<T>(Singly<T> singly) => new Expected<T>
        {
            Count = singly.Count,
            Head = singly.Head,
            Tail = singly.Tail
        };
        private static IEnumerable<int> ConvertToInts(string str)
        {
            if (str.IsNullOrEmpty())
                return new List<int>();
            return Array.ConvertAll(str.Split(','), int.Parse);
        }
        #endregion

        #region Private Assert Methods
        private void AssertHeadTailAndCount<T>(
            Expected<T> expected
            , Singly<T> actualSingly)
        {
            Assert.Equal(expected.Head, actualSingly.Head);
            Assert.Equal(expected.Tail, actualSingly.Tail);
            Assert.True(expected.Count == actualSingly.Count);
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
            var singly = new Singly<int>(ConvertToInts(strList));
            var expected = GetExpected(singly);

            var actual = singly.IsPalindrome();

            Assert.True(actual);
            AssertHeadTailAndCount(expected, singly);
        }

        [Theory]
        [InlineData("")]
        [InlineData("1,2")]
        [InlineData("1,2,2")]
        [InlineData("1,2,3,4,1,2,1")]
        public void IsPalindrome_ReturnsFalse_WhenListIsNotPalindrome(
            string strList)
        {
            var singly = new Singly<int>(ConvertToInts(strList));
            var expected = GetExpected(singly);

            var actual = singly.IsPalindrome();

            Assert.False(actual);
            AssertHeadTailAndCount(expected, singly);
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
            var singly = new Singly<int>(ConvertToInts(strList));
            singly.SetupLoop(startOfLoop);

            var actual = singly.LoopExists();

            Assert.True(actual);
        }

        [Theory]
        [InlineData("", 2)]
        [InlineData("10,1,4,3,9,78,45,23,36,5", 11)]
        public void LoopExists_ReturnsFalse_WhenLoopDoesNotExistsInTheList(
            string strList
            , int startOfLoop)
        {
            var singly = new Singly<int>(ConvertToInts(strList));
            singly.SetupLoop(startOfLoop);

            var actual = singly.LoopExists();

            Assert.False(actual);
        }
    }
}
