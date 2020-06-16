using System.Collections.Generic;

namespace Algo.Lists.SortingAlgorithms
{
    public static class Sort
    {
        /// <summary>
        /// Time complexity:    O(n*n)
        /// Space complexity:   O(n)
        /// Perfoms bubble sorting on a collection of integers.
        /// 
        /// The only significant advantage that bubble sort has over most other algorithms,
        /// is that the ability to detect that the list is already sorted is built into the
        /// algorithm.
        /// 
        /// If the list is already sorted (best-case), its time-complexity is only O(n),
        /// And space-complexity is O(1). Most other algorithms, even those with better
        /// average-case complexity, performs their entire sorting process on the set and thus
        /// are more complex.
        /// 
        /// It should be avoided in the case of large collections.
        /// </summary>
        /// <param name="elements">The elements.</param>
        public static void BubbleSort(this IList<int> elements)
        {
            var swapped = true;
            while (swapped)
            {
                var i = -1;
                var j = 0;
                swapped = false;
                while ((++i < (elements.Count - 1)) && (++j < elements.Count))
                {
                    if (elements[j] < elements[i])  //when element at i is greater than element at j.
                    {
                        //swap the elements.
                        elements[j] = elements[j] + elements[i];
                        elements[i] = elements[j] - elements[i];
                        elements[j] = elements[j] - elements[i];
                        swapped = true;
                    }
                }
            }
        }

        public static void InsertionSort(this IList<int> elements)
        {
            var sortedLastIndex = 0;
            var unsortedFirstIndex = 0;
            var unsortedFirst = elements[1];
            while (++unsortedFirstIndex < elements.Count)
            {
                sortedLastIndex = unsortedFirstIndex - 1;
                unsortedFirst = elements[unsortedFirstIndex];

                if (unsortedFirst < elements[sortedLastIndex]) //when unsorted first is less than sorted last.
                {
                    while ((0 <= sortedLastIndex) && (unsortedFirst < elements[sortedLastIndex]))
                    {
                        //swap the elements
                        elements[sortedLastIndex] = elements[sortedLastIndex] + elements[sortedLastIndex + 1];
                        elements[sortedLastIndex + 1] = elements[sortedLastIndex] - elements[sortedLastIndex + 1];
                        elements[sortedLastIndex] = elements[sortedLastIndex] - elements[sortedLastIndex + 1];
                        --sortedLastIndex;
                    }
                }
            }
        }
    }
}
