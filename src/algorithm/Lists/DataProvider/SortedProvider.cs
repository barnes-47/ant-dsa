using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Lists.DataProvider
{
    public class SortedProvider
    {
        public int Avg { get; }
        public int Max { get; }
        public int Min { get; }
        public int Random { get; }
        public int RandomIndex { get; }
        public int NotFound { get; }
        public IList<int> Elements { get; }

        public SortedProvider(int size)
        {
            Elements = new List<int>();
            for (var i = 0; i < size; ++i)
            {
                if (i < (size / 2))
                    Elements.Add(i);
                else
                    Elements.Add((short.MaxValue * 100) + i);
            }

            RandomIndex = new Random((int)DateTime.Now.Ticks).Next(0, Elements.Count - 1);
            Max = Elements.Max();
            Min = Elements.Min();
            Elements = Elements.OrderBy(x => x).ToList();
            Avg = Elements.First(x => x > (Min + Max) / 2);
            Random = Elements[RandomIndex];
            NotFound = Elements.Max() + 1;
        }
    }
}
