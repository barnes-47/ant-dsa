using System.Collections.Generic;
using Xunit;

namespace Practice.Test
{
    public class DailyCodingProblemsTests
    {
        [Fact]
        public void FindTwoNumbersInTheArrayThatAddsTo_Returns_True()
        {
            var num = 17;
            var unorderedList = new List<int> { 10, 2, 11, 12, 7 };
            var problem = new DailyCodingProblems();

            var actual = problem.GetTwoNumbersInTheArrayThatAddsTo(unorderedList, num);

            Assert.True(actual);
        }

        [Fact]
        public void FindTwoNumbersInTheArrayThatAddsTo_Returns_False()
        {
            var num = 121;
            var unorderedList = new List<int> { 10, 2, 11, 12, 7 };
            var problem = new DailyCodingProblems();

            var actual = problem.GetTwoNumbersInTheArrayThatAddsTo(unorderedList, num);

            Assert.False(actual);
        }

        [Fact]
        public void GetTheMultiplicationArray_ReturnsTheMultiplicationArray()
        {
            var unorderedList = new List<int> { 1, 2, 3, 4, 5 };
            var expected = new List<int> { 120, 60, 40, 30, 24 };
            var problem = new DailyCodingProblems();

            var actual = problem.GetTheMultiplicationArray(unorderedList);

            for (var i = 0; i < actual.Count; ++i)
            {
                Assert.True(expected[i] == actual[i]);
            }
        }

        [Fact]
        public void Test()
        {
            //var unorderedList = new List<int> { 1, 2, 3, 4, 5 };
            //var expected = new List<int> { 120, 60, 40, 30, 24 };

            //var d = 120 >> 1;
            //var c = 120 >> 2;
            //var e = 120 >> 3;
            //var f = 120 >> 4;
            //var g = 120 >> 5;
        }
    }
}
