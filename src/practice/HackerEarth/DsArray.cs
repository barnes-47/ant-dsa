namespace Practice.HackerEarth
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DsArray
    {
        #region Practice Problems
        /// <summary>
        /// Problem:
        ///     Given an array with integers(in string format) as its element.
        ///     For each integer, check if it's digits are sequential/continous sequence (digits doesn't neccessarily have to be in order).
        ///     The differnce between consecutive digits is |1|, when they are arranged in order.
        ///     For example -
        ///         4231 is sequential, but not in order.
        ///         1234 is sequential, and also in order.
        ///         1235 is non-sequential.
        /// </summary>
        /// <param name="integers">Collection of integers in string format.</param>
        /// <returns>Collection of boolean indicating which element in the array has sequential digits.</returns>
        public IEnumerable<bool> DigitsOfTheNumbersInArraySequential(IEnumerable<string> integers)
        {
            var numbers = integers.ToArray();
            var arrayIsDigitsSequential = new bool[numbers.Length];
            for(var i = 0; i < numbers.Length; ++i)
            {
                arrayIsDigitsSequential[i] = InternalIsDigitsOfTheNumberSequential(numbers[i]);
            }

            return arrayIsDigitsSequential;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="number">Number in string format.</param>
        /// <returns></returns>
        public bool IsDigitsOfTheNumberSequential(string number)
            => InternalIsDigitsOfTheNumberSequential(number);
        #endregion Practice Problems

        #region Private Helper Methods
        private bool InternalIsDigitsOfTheNumberSequential(string number)
        {
            var digitArray = number.Select(character => int.Parse(character.ToString())).ToArray();
            var first = digitArray.Min();
            Array.Sort(digitArray);
            for (var i = 0; i < digitArray.Length - 1; i++)
            {
                if (digitArray[i] != digitArray[i + 1] - 1)
                    return false;
            }

            return true;
        }

        private int GetArithmeticSeries(int numberOfElements, int firstElement, int commonDifference)
            => (numberOfElements * ((2 * firstElement) + ((numberOfElements - 1) * commonDifference))) / 2;
        #endregion Private Helper Methods
    }
}
