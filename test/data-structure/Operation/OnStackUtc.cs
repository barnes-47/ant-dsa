namespace Ds.Test.Operation
{
    using DsGeneric = Ds.Generic;
    using Ds.Operation;
    using Ds.Helper;
    using System.Collections.Generic;
    using Xunit;
    using System;

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

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void InfixToPostfix_ThrowsArgumentNullException_WhenInfixExpressionIsNullOrEmpty(
            string inputExp)
        {
            var expectedEx = new ArgumentNullException(nameof(inputExp));
            var actualOnStack = new OnStack<char>();


            var actualEx = Assert.Throws<ArgumentNullException>(() => actualOnStack.InfixToPostfix(inputExp));

            Assert.True(expectedEx.ParamName == actualEx.ParamName);
            Assert.True(expectedEx.Message == actualEx.Message);
        }

        [Theory]
        [InlineData("abc")]
        [InlineData("abcdef")]
        [InlineData("abc def")]
        public void InfixToPostfix_ThrowsArgumentNullException_WhenInfixExpressionDoestNotContainsAnyArithmeticOperator(
            string inputExp)
        {
            var expectedEx = new ArgumentException(Message.OnStack.ArithmeticOperatorNotFound);
            var actualOnStack = new OnStack<char>();

            var actualEx = Assert.Throws<ArgumentException>(() => actualOnStack.InfixToPostfix(inputExp));

            Assert.True(expectedEx.ParamName == actualEx.ParamName);
            Assert.True(expectedEx.Message == actualEx.Message);
        }

        [Theory]
        [InlineData("a+b*c+d", "abc*+d+")]
        [InlineData("(a+b)*(c+d)", "ab+cd+*")]
        [InlineData("((((a+b)*c)-d)/e)", "ab+c*d-e/")]
        [InlineData("(((a+b)*c)/d+e*f/g)", "ab+c*d/ef*g/+")]        
        [InlineData("k+l-m*n+(o+p)*w/u/v*t+q", "kl+mn*-op+w*u/v/t*+q+")]
        public void InfixToPostfix_ConvertsAnInfixExpressionToPostfixExpression(
            string inputExp
            , string expectedExp)
        {
            var actualOnStack = new OnStack<char>();

            var actualExp = actualOnStack.InfixToPostfix(inputExp);

            Assert.True(expectedExp == actualExp);
        }

        [Theory]
        [InlineData("a+b*c+d", "++a*bcd")]
        [InlineData("(a+b)*(c+d)", "*+ab+cd")]
        [InlineData("(((a+b)*c)/d+e*f/g)", "+/*+abcd/*efg")]
        [InlineData("k+l-m*n+(o+p)*w/u/v*t+q", "++-+kl*mn*//*+opwuvtq")]
        public void InfixToPrefix_ConvertsAnInfixExpressionToPrefixExpression(
            string infixExp
            , string expectedExp)
        {
            var actualOnStack = new OnStack<char>();

            var actualExp = actualOnStack.InfixToPrefix(infixExp);

            Assert.True(expectedExp == actualExp);
        }
    }
}
