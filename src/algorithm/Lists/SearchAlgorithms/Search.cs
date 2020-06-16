using Algo.Lists.SearchAlgorithms.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Algo.Lists.SearchAlgorithms
{
    public static class Search
    {
        /// <summary>
        /// Time complexity:    O(log n)
        /// Space complexity:   O(1)
        /// Peforms binary search on a sorted collection of integers.
        /// </summary>
        /// <param name="elements">The sorted collection of integers.</param>
        /// <param name="item">The item to be searched.</param>
        /// <returns>Instance of <see cref="SearchResult"/>.</returns>
        public static SearchResult BinarySearch(this IList<int> elements, int item)
        {
            var result = new SearchResult();
            var sw = new Stopwatch();
            sw.Start();

            if (!elements.Any())    //When the list is empty.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;

                return result;
            }
            if (item < elements[0] || item > elements[^1])    //When the item does not exists in the sorted list.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;

                return result;
            }
            if (item == elements[0] || item == elements[^1])   //When the item is first or last element of the sorted list.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;
                result.Index = elements.Count - 1;

                return result;
            }

            var start = 0;
            var end = elements.Count - 1;
            var middle = (end - start) / 2;     //floor
            while (start < end)
            {
                if (elements[middle] == item)
                    break;
                if (item > elements[middle])
                    start = middle + 1;
                else
                    end = middle - 1;

                middle = start + ((end - start) / 2);
            }

            sw.Stop();
            result.Ticks = sw.ElapsedTicks;

            if (elements[middle] == item)
                result.Index = middle;


            return result;
        }

        /// <summary>
        /// Time complexity:    O(log(log n))
        /// Space complexity:   O(1)
        /// Peforms interpolation search on a sorted and uniformly distributed collection of integers.</summary>
        /// <param name="elements">The sorted and uniformly distributed collection of integers.</param>
        /// <param name="item">The item to be searched.</param>
        /// <returns>Instance of <see cref="SearchResult"/>.</returns>
        public static int InterpolationSearch(this IList<int> elements, int item)
        {
            var start = 0;
            var end = elements.Count - 1;
            var pos = -1;
            while (start <= end)
            {
                pos = (int)(start + (((decimal)(end - start) / elements[end] - elements[start]) * (item - elements[start])));
                if (pos < 0 || pos > elements.Count - 1)
                    return -1;
                if (elements[pos] == item)
                    return pos;
                if (item > elements[pos])
                    start = pos + 1;
                else
                    end = pos - 1;
            }

            return -1;
        }

        /// <summary>
        /// Time complexity:    O(log(sqrt n))
        /// Space complexity:   o(1)
        /// Performs jump search to check if the requested item exists in a particular block.
        /// Once found, binary search is performed on that block.
        /// </summary>
        /// <param name="elements">The sorted collection of integers.</param>
        /// <param name="item">The item to be searched.</param>
        /// <returns>Instance of <see cref="SearchResult"/>.</returns>
        public static SearchResult JumpinarySearch(this IList<int> elements, int item)
        {
            var result = new SearchResult();
            var sw = new Stopwatch();
            sw.Start();

            if (!elements.Any())    //When the list is empty.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;

                return result;
            }
            if (item < elements[0] || item > elements[^1])    //When the item does not exists in the sorted list.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;

                return result;
            }
            if (item == elements[0] || item == elements[^1])   //When the item is first or last element ofs the sorted list.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;
                result.Index = elements.Count - 1;

                return result;
            }

            //Find the block where the item, may exist.
            var blockSize = (int)Math.Sqrt(elements.Count); //floor
            var blockStart = 0;
            var blockEnd = blockSize - 1;
            var end = elements.Count - 1;
            while ((blockStart <= end) && (item > elements[blockEnd]))
            {
                blockStart += blockSize;
                blockEnd += blockSize;
                if (end < blockEnd)
                    blockEnd = end;
            }

            //Perform binary search on the block.
            var start = blockStart;
            end = blockEnd;
            var middle = start + ((end - start) / 2);
            while (start < end)
            {
                if (elements[middle] == item)
                    break;
                if (item > elements[middle])
                    start = middle + 1;
                else
                    end = middle - 1;

                middle = start + ((end - start) / 2);
            }

            sw.Stop();
            result.Ticks = sw.ElapsedTicks;

            if (elements[middle] == item)
                result.Index = middle;

            return result;
        }

        /// <summary>
        /// Time complexity:    O(sqrt n)
        /// Space complexity:   O(1)
        /// Peforms jumps search on a sorted collection of integers.</summary>
        /// <param name="elements">The sorted collection of integers.</param>
        /// <param name="item">The item to be searched.</param>
        /// <returns>Instance of <see cref="SearchResult"/>.</returns>
        public static SearchResult JumpSearch(this IList<int> elements, int item)
        {
            var result = new SearchResult();
            var sw = new Stopwatch();
            sw.Start();

            if (!elements.Any())    //When the list is empty.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;

                return result;
            }
            if (item < elements[0] || item > elements[^1])    //When the item does not exists in the sorted list.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;

                return result;
            }
            if (item == elements[0] || item == elements[^1])   //When the item is first or last element of the sorted list.
            {
                sw.Stop();
                result.Ticks = sw.ElapsedTicks;
                result.Index = elements.Count - 1;

                return result;
            }

            //First perform jump/block search to find the block in which the requested item exists.
            var blockSize = (int)Math.Sqrt(elements.Count); //floor
            var blockStart = 0;
            var blockEnd = blockSize - 1;
            var end = elements.Count - 1;
            while((blockStart <= end) && (item > elements[blockEnd]))
            {
                blockStart += blockSize;
                blockEnd += blockSize;
                if (end < blockEnd)     //if blockEnd is greater than end of list, set blockEnd to end.
                    blockEnd = end;
            }

            //Then perform linear search in the block.
            while(blockStart <= blockEnd)
            {
                if (elements[blockStart] == item)
                {
                    result.Index = blockStart;
                    break;
                }
                ++blockStart;
            }

            sw.Stop();
            result.Ticks = sw.ElapsedTicks;

            return result;
        }

        /// <summary>
        /// Time complexity:    O(n)
        /// Space complexity:   O(1)
        /// Performs linear search on an (un)sorted collection of integers.
        /// </summary>
        /// <param name="elements">The (un)sorted collection of integers.</param>
        /// <param name="item">The item to be searched.</param>
        /// <returns>Index of the item if found, -1 otherwise.</returns>
        public static SearchResult LinearSearch(this IList<int> elements, int item)
        {
            var result = new SearchResult();
            var sw = new Stopwatch();
            sw.Start();

            for (var i = 0; i < elements.Count; ++i)
            {
                if (elements[i] == item)
                {
                    result.Index = i;
                    break;
                }
            }

            sw.Stop();
            result.Ticks = sw.ElapsedTicks;

            return result;
        }
    }
}
