namespace Practice.Test.HackerEarth
{
    using Practice.HackerEarth;
    using Xunit;

    public class DsArrayTest
    {
        private readonly DsArray _dsArray;

        public DsArrayTest()
        {
            _dsArray = new DsArray();
        }
    
        [Theory]
        [InlineData("00001")]
        public void IsDigitsOfTheNumberSequential_ReturnsFalse_WhenNumberIsNotSequential(
            string testInput)
        {
            var actual = _dsArray.IsDigitsOfTheNumberSequential(testInput);

            Assert.False(actual);
        }

        [Theory]
        [InlineData("65")]
        [InlineData("6543")]
        [InlineData("1023")]
        [InlineData("13254")]
        [InlineData("8976")]
        [InlineData("465879")]
        public void IsDigitsOfTheNumberSequential_ReturnsTrue_WhenNumberIsSequential(
            string testInput)
        {
            var actual = _dsArray.IsDigitsOfTheNumberSequential(testInput);

            Assert.True(actual);
        }
    }
}
