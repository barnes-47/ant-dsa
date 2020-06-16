using System.Collections.Generic;
using System.Linq;

namespace Practice
{
    public class Problem
    {
        #region Public Methods
        /// <summary>
        /// Finds the two numbers in the list that adds to the number passed.
        /// </summary>
        /// <param name="num">The number.</param>
        public bool FindTwoNumbersInTheArrayThatAddsTo(IList<int> unorderedList, int num)
            => FindTwoIntsInTheListThatAddsTo(unorderedList, num).Any();
        #endregion

        #region Private Methods
        private IList<int> FindTwoIntsInTheListThatAddsTo(IList<int> unordered, int num)
        {
            //Sort the list.
            var ordered = unordered.OrderBy(x => x).ToList();

            //Gets the 2 numbers.
            var list = new List<int>();
            var middle = ordered.Count / 2;
            var sum = 0;
            for (int i = 0, j = ordered.Count - 1; i <= middle && j >= middle;)
            {
                sum = ordered[i] + ordered[j];
                if (sum < num)
                {
                    ++i;
                    continue;
                }
                if (sum > num)
                {
                    --j;
                    continue;
                }

                list.Add(ordered[i]);
                list.Add(ordered[j]);
                break;
            }

            return list;
        }
        #endregion
    }
}
