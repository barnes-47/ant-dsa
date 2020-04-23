using DsGeneric = Ds.Generic;
using Ds.Operation;
using System.Collections.Generic;
using Xunit;

namespace Ds.Test.Operation
{
    public class OnStackUtc
    {
        #region Private Assert Methods
        private void AssertTrueZeroAndActualStackCount<T>(DsGeneric.Stack<T> actual)
            => Assert.True(0 == actual.Count);
        private void AssertTrueCountOfExpectedStackAndActualStack<T>(DsGeneric.Stack<T> expected, DsGeneric.Stack<T> actual)
            => Assert.True(expected.Count == actual.Count);
        private void AssertTrueElementsOfExpectedStackAndActualStack<T>(DsGeneric.Stack<T> expected, DsGeneric.Stack<T> actual)
        {
            var comparer = EqualityComparer<T>.Default;
            while(expected.Count > 0 && actual.Count > 0)
            {
                Assert.True(expected.Count == actual.Count);
                Assert.True(comparer.Equals(expected.Pop(), actual.Pop()));
            }
        }
        private void AssertTrueExpectedOnStackAndActualOnStack<T>(OnStack<T> expected, OnStack<T> actual)
        {
            AssertTrueElementsOfExpectedStackAndActualStack<T>(expected.Stack, actual.Stack);
            AssertTrueCountOfExpectedStackAndActualStack<T>(expected.Stack, actual.Stack);
        }
        #endregion

        [Theory]
        [InlineData("a", "a")]
        [InlineData("ab", "ba")]
        [InlineData("abc", "cba")]
        [InlineData("abcd", "dcba")]
        [InlineData("abcde", "edcba")]
        public void InsertAtBottomUsingRecursion_InsertsItemAtTheBottomOfTheStack_ForCharStack(
            string actualStr
            , string expectedStr)
        {
            var actualOnStack = new OnStack<char>();
            var expectedOnStack = new OnStack<char>(expectedStr.ToCharArray());

            for (var i = 0; i < actualStr.Length; i++)
            {
                actualOnStack.InsertAtBottomUsingRecursion(actualStr[i]);
            }

            AssertTrueElementsOfExpectedStackAndActualStack<char>(expectedOnStack.Stack, actualOnStack.Stack);
            AssertTrueZeroAndActualStackCount(actualOnStack.Stack);
        }

        [Theory]
        [InlineData("aa", "aa")]
        [InlineData("aa,bb", "bb,aa")]
        [InlineData("aa,bb,cc", "cc,bb,aa")]
        [InlineData("aa,bb,cc,dd", "dd,cc,bb,aa")]
        public void InsertAtBottomUsingRecursion_InsertsItemAtTheBottomOfTheStack_ForStringStack(
            string actualStr
            , string expectedStr)
        {
            var actualStrArray = actualStr.Split(',');
            var actualOnStack = new OnStack<string>();
            var expectedOnStack = new OnStack<string>(expectedStr.Split(','));

            for (var i = 0; i < actualStrArray.Length; i++)
            {
                actualOnStack.InsertAtBottomUsingRecursion(actualStrArray[i]);
            }

            AssertTrueElementsOfExpectedStackAndActualStack<string>(expectedOnStack.Stack, actualOnStack.Stack);
            AssertTrueZeroAndActualStackCount(actualOnStack.Stack);
        }

        [Theory]
        [InlineData("aa", "aa")]
        [InlineData("aa,bb", "bb,aa")]
        [InlineData("aa,bb,cc", "cc,bb,aa")]
        [InlineData("aa,bb,cc,dd", "dd,cc,bb,aa")]
        public void ReverseUsingRecursion_ReversesStackUsingRecursion(
            string actualStr
            , string expectedStr)
        {
            var actualOnStack = new OnStack<string>(actualStr.Split(','));
            var expectedOnStack = new OnStack<string>(expectedStr.Split(','));

            actualOnStack.ReverseUsingRecursion();

            AssertTrueExpectedOnStackAndActualOnStack<string>(expectedOnStack, actualOnStack);
        }

        [Theory]
        [InlineData("aa", "aa")]
        [InlineData("aa,bb", "bb,aa")]
        [InlineData("aa,bb,cc", "cc,bb,aa")]
        [InlineData("aa,bb,cc,dd", "dd,cc,bb,aa")]
        public void ReverseUsingExtraSpace_ReversesStackUsingExtraSpace(
            string actualStr
            , string expectedStr)
        {
            var actualOnStack = new OnStack<string>(actualStr.Split(','));
            var expectedOnStack = new OnStack<string>(expectedStr.Split(','));

            actualOnStack.ReverseUsingExtraSpace();

            AssertTrueExpectedOnStackAndActualOnStack<string>(expectedOnStack, actualOnStack);
        }
    }
}
