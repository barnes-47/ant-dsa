using System.Collections.Generic;
using Xunit;

namespace Practice.Test
{
    public class ProblemTests
    {
        [Fact]
        public void FindTwoNumbersInTheArrayThatAddsTo_Returns_True()
        {
            var num = 17;
            var unorderedList = new List<int> { 10, 2, 11, 12, 7 };
            var problem = new Problem();

            var actual = problem.FindTwoNumbersInTheArrayThatAddsTo(unorderedList, num);

            Assert.True(actual);
        }
    }
}
