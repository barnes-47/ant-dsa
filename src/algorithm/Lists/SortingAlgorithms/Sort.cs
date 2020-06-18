namespace Algo.Lists.SortingAlgorithms
{
    using Algo.Lists.SortingAlgorithms.Models;
    using System.Collections.Generic;
    using System.Diagnostics;

    public static class Sort
    {
        /// <summary>
        /// Bubble sort algorithm.
        /// Time complexity:    O(n*n)
        /// Space complexity:   O(n)
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
        public static SortResult BubbleSort(this IList<int> elements)
        {
            var result = new SortResult();
            var sw = Stopwatch.StartNew();

            //Bubble Sort Algorithm
            int i, j;
            var swapped = true;
            while (swapped)
            {
                i = -1; j = 0;
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

            sw.Stop();
            result.Ticks = sw.ElapsedTicks;
            result.Elements = elements;

            return result;
        }

        /// <summary>
        /// Insertion sort algorithm.
        /// Time complexity:    O(n*n)
        /// Space complexity:   O(1)
        /// 
        /// It divides(logically) the list into 2 parts - sorted and unsorted.
        /// The first element of the list is considered the sorted and
        /// remaining list is considered unsorted.
        /// At a time it picks one element from unsorted list and puts it in it's place in the sorted list.
        /// 
        /// It is much less efficient on large lists than more advanced algorithms,
        /// such as QuickSort, HeadSort and MergeSort.
        /// </summary>
        /// <param name="elements">The elements.</param>
        public static SortResult InsertionSort(this IList<int> elements)
        {
            var result = new SortResult();
            var sw = Stopwatch.StartNew();

            //Insertion Sort Algorithm
            var sortedIndex = 0;
            var unsortedIndex = 0;
            var firstUnsortedElement = elements[1];
            while (++unsortedIndex < elements.Count)
            {
                sortedIndex = unsortedIndex - 1;
                firstUnsortedElement = elements[unsortedIndex];

                if (firstUnsortedElement < elements[sortedIndex]) //when unsorted first is less than sorted last.
                {
                    while ((0 <= sortedIndex) && (firstUnsortedElement < elements[sortedIndex]))
                    {
                        //swap the elements
                        elements[sortedIndex] = elements[sortedIndex] + elements[sortedIndex + 1];
                        elements[sortedIndex + 1] = elements[sortedIndex] - elements[sortedIndex + 1];
                        elements[sortedIndex] = elements[sortedIndex] - elements[sortedIndex + 1];
                        --sortedIndex;
                    }
                }
            }

            sw.Stop();
            result.Ticks = sw.ElapsedTicks;
            result.Elements = elements;

            return result;
        }

        public static SortResult MergeSort(this IList<int> elements)
        {
            var result = new SortResult();
            var sw = Stopwatch.StartNew();

            InternalSort(elements, 0, elements.Count - 1);

            sw.Stop();
            result.Ticks = sw.ElapsedTicks;
            result.Elements = elements;

            return result;
        }

        #region Private Methods
        private static void InternalListCopy(IList<int> actual, IList<int> mirror)
        {
            var i = -1;
            while (++i < actual.Count)
                mirror.Add(actual[i]);
        }
        private static void InternalMerge(IList<int> elements, int begin, int middle, int end)
        {
            //Create 2 lists
            var listX = new List<int>();
            var listY = new List<int>();
            for (var i = begin; i <= middle; ++i)
                listX.Add(elements[i]);
            for (var i = middle + 1; i <= end; ++i)
                listY.Add(elements[i]);

            //Loop through both lists to compare and replace the elements
            var iX = 0;
            var iY = 0;
            var iElement = begin;
            while (iX < listX.Count && iY < listY.Count)
            {
                if (listX[iX] <= listY[iY])
                    elements[iElement] = listX[iX++];
                else
                    elements[iElement] = listY[iY++];
                ++iElement;
            }

            //Copy remaining elements
            while (iX < listX.Count)
                elements[iElement++] = listX[iX++];
            while (iY < listY.Count)
                elements[iElement++] = listY[iY++];
        }
        private static void InternalSort(IList<int> elements, int begin, int end)
        {
            if (end <= begin)
                return;

            var middle = (end + begin) / 2;
            InternalSort(elements, begin, middle);
            InternalSort(elements, middle + 1, end);
            InternalMerge(elements, begin, middle, end);
        }
        #endregion
    }
}
