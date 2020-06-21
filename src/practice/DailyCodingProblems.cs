using System.Collections.Generic;
using System.Linq;

namespace Practice
{
    public class DailyCodingProblems
    {
        #region Public Methods
        /// <summary>
        /// This problem was recently asked by Google.
        /// 
        /// Given a list of numbers and a number k,
        /// return whether any two numbers from the list add up to k.
        /// 
        /// For example, given[10, 15, 3, 7] and k of 17, return true since 10 + 7 is 17.
        /// 
        /// Bonus: Can you do this in one pass?
        /// </summary>
        /// <param name="num">The number.</param>
        public bool GetTwoNumbersInTheArrayThatAddsTo(IList<int> unorderedList, int num)
            => FindTwoIntsInTheListThatAddsTo(unorderedList, num).Any();

        /// <summary>
        /// This problem was asked by Uber.
        /// 
        /// Given an array of integers, return a new array such that
        /// each element at index i of the new array is the product of
        /// all the numbers in the original array except the one at i.
        /// 
        /// For example, if our input was [1, 2, 3, 4, 5], the expected output would be [120, 60, 40, 30, 24].
        /// If our input was [3, 2, 1], the expected output would be [2, 3, 6].
        /// 
        /// Follow-up: what if you can't use division?
        /// </summary>
        /// <returns></returns>
        public IList<int> GetTheMultiplicationArray(IList<int> unorderedList)
        {
            #region First Idea
            var product = 1;
            var count = 0;
            while (count < unorderedList.Count)
            {
                product *= unorderedList[count++];
            }

            var productList = new List<int>();
            count = 0;
            while (count < unorderedList.Count)
            {
                productList.Add(product / unorderedList[count++]);
            }

            return productList;
            #endregion

            #region Follow-up

            #endregion
        }

        /// <summary>
        /// This problem was asked by Google.
        /// 
        /// Given the root to a binary tree, implement serialize(root), which serializes the tree into a string,
        /// and deserialize(s), which deserializes the string back into the tree.
        /// </summary>
        /// <returns></returns>
        public string Serialize()
        {
            return "";
        }
        #endregion

        #region Private Methods        
        /// <summary>Gets the sum of n terms in an arithmetic progression.</summary>
        /// <param name="first">The first element.</param>
        /// <param name="diff">The difference between two consecutive elements.</param>
        /// <param name="n">The number of elements.</param>
        /// <returns></returns>
        private int GetSumOfNTermsInArithmeticProgression(int first, int diff, int n)
        {
            if ((n & 1) == 1)   //When n is ODD.
                return n * (((2 * first) + ((n - 1) * diff)) / 2);

            return n / 2 * ((2 * first) + ((n - 1) * diff));
        }

        /// <summary>Gets the factorial of the number passed.</summary>
        /// <param name="n">The number.</param>
        /// <returns></returns>
        public int GetFactorial(int n)
        {
            var product = 1;
            while (n > 1)
            {
                product *= n--;
            }

            return product;
        }

        /// <summary>Gets the factorial of the number passed using recursion.</summary>
        /// <param name="n">The number.</param>
        /// <returns></returns>
        public int GetFactorial_Recursion(int n)
        {
            if (n < 2)
                return 1;
            return n * GetFactorial_Recursion(n - 1);
        }

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
