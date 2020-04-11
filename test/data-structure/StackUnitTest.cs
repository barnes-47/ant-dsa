using System;
using Xunit;

namespace Ds.Test
{
    public class StackUnitTest
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
        [InlineData("1", "1")]
        [InlineData("12", "12")]
        [InlineData("123", "123")]
        [InlineData("1234", "1234")]
        [InlineData("12345", "12345")]
        [InlineData("123456", "123456")]
        public void Push_InsertsDataOnTheStack(
            string actualStr
            , string expectedStr)
        {
            var expectedElements = expectedStr.ToCharArray();
            var expectedTop = expectedElements[^1];
            var actualStack = new Stack(actualStr.Length);

            for (var i = 0; i < expectedElements.Length; i++)
            {
                actualStack.Push(actualStr.ToCharArray()[i]);
            }

            Assert_BeforePopping(expectedElements, expectedTop, actualStack);
            Assert_WhilePopping(expectedElements, actualStack);
            Assert_AfterPopping(actualStack);
        }

        [Theory]
        [InlineData("1", "1")]
        [InlineData("12", "21")]
        [InlineData("123", "321")]
        [InlineData("1234", "4321")]
        [InlineData("12345", "54321")]
        [InlineData("123456", "654321")]
        public void Reverse_RearrangeElementsOfStackInReverseOrder(
            string actualStr
            , string expectedStr)
        {
            var expectedElements = expectedStr.ToCharArray();
            var expectedTop = expectedElements[^1];
            var actualStack = new Stack(actualStr.ToCharArray());

            actualStack.Reverse();

            Assert_BeforePopping(expectedElements, expectedTop, actualStack);
            Assert_WhilePopping(expectedElements, actualStack);
            Assert_AfterPopping(actualStack);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-99)]
        [InlineData(-99999999)]
        [InlineData(10)]
        [InlineData(90)]
        [InlineData(9877889)]
        public void DeleteAt_ThrowsArgumentOutOfRangeException_WhenIndexIsNegativeInt_Or_GreaterThanCountOfStack(
            int fakeIndex)
        {
            var expectedEx = new ArgumentOutOfRangeException("index", fakeIndex, "Invalid index.");
            var actualStack = new Stack(5);

            var actualEx = Assert.Throws<ArgumentOutOfRangeException>(() => actualStack.DeleteAt(fakeIndex));

            Assert.True(expectedEx.ParamName == actualEx.ParamName);
            Assert.True(expectedEx.Message == actualEx.Message);
        }
    }
}
