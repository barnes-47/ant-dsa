using Ds.Operation;
using System;
using Xunit;

namespace Ds.Test.Operation
{
    public class OnStackUnitTest
    {
        private void Assert_BeforePopping(char[] expected, char expectedTop, Stack actual)
        {
            Assert.False(actual.IsUnderflow);
            Assert.True(actual.IsOverflow);
            Assert.True(expected.Length == actual.Count + 1);
            Assert.True(expectedTop == actual.Peek());
        }
        private void Assert_WhilePopping(char[] expected, Stack actual)
        {
            for (var i = expected.Length - 1; i > -1 && actual.Count > -1; --i)
            {
                Assert.True(expected[i] == actual.Pop());
            }
        }
        private void Assert_AfterPopping(Stack actual)
        {
            Assert.False(actual.IsOverflow);

            Assert.True(-1 == actual.Count);
            Assert.True(actual.IsUnderflow);
            Assert.True(default(char) == actual.Peek());
        }

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        public void InsertAtBottom_InsertsTheDataAtTheBottomOfTheStack_WhenStackIsNotEmpty(
            string actualStr)
        {
            var expectedElements = actualStr.ToCharArray();
            var expectedTop = expectedElements[0];
            var actualOnStack = new OnStack(expectedElements.Length);

            for (var i = 0; i < expectedElements.Length; ++i)
            {
                actualOnStack.InsertAtBottom(expectedElements[i]);
            }

            Assert_BeforePopping(expectedElements, expectedTop, actualOnStack.Stack);

            Array.Reverse(expectedElements, 0, expectedElements.Length);
            Assert_WhilePopping(expectedElements, actualOnStack.Stack);
            Assert_AfterPopping(actualOnStack.Stack);
        }

        [Theory]
        [InlineData("1")]
        [InlineData("12")]
        [InlineData("123")]
        [InlineData("1234")]
        [InlineData("12345")]
        [InlineData("123456")]
        public void Reverse_RearrangeElementsOfStackInReverseOrder_WhenStackHasAtleastTwoElements(
            string actualStr)
        {
            var expectedElements = actualStr.ToCharArray();
            var expectedTop = expectedElements[0];
            var actualOnStack = new OnStack(expectedElements);

            actualOnStack.Reverse();

            Array.Reverse(expectedElements);
            Assert_BeforePopping(expectedElements, expectedTop, actualOnStack.Stack);
            Assert_WhilePopping(expectedElements, actualOnStack.Stack);
            Assert_AfterPopping(actualOnStack.Stack);
        }

        [Theory]
        [InlineData("12", "2")]
        [InlineData("123", "13")]
        [InlineData("1234", "134")]
        [InlineData("12345", "1245")]
        [InlineData("123456", "12456")]
        public void DeleteMiddleElement_DeletesTheMiddleElement(
            string actualStr
            , string expectedStr)
        {
            var expectedElement = expectedStr.ToCharArray();
            var expectedTop = expectedElement[^1];
            var actualOnStack = new OnStack(actualStr.ToCharArray());

            actualOnStack.DeleteMiddleElement();

            Assert.False(actualOnStack.Stack.IsOverflow);
            Assert.False(actualOnStack.Stack.IsUnderflow);

            Assert.True(expectedElement.Length == actualOnStack.Stack.Count + 1);
            Assert.True(expectedTop == actualOnStack.Stack.Peek());

            Assert_WhilePopping(expectedElement, actualOnStack.Stack);
            Assert_AfterPopping(actualOnStack.Stack);
        }
    }
}
