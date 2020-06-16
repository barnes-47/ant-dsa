using System;
using System.Collections.Generic;
using System.Linq;

namespace Algo.Lists.DataProvider
{
    public class UnsortedProvider
    {
        public int Avg { get; }
        public int Max { get; }
        public int Min { get; }
        public int Random { get; }
        public int RandomIndex { get; }
        public int NotFound { get; }
        public IList<int> Elements { get; }

        public UnsortedProvider(int size)
        {
            var rnd = new Random(1);
            Elements = new List<int>();
            for (var i = 0; i < size; ++i)
            {
                if ((i & 1) == 1)   //when i is odd.
                    Elements.Add(rnd.Next(0, size * 10));
                else
                    Elements.Add(rnd.Next((short.MaxValue * 100) + i, int.MaxValue - i));
            }

            RandomIndex = new Random((int)DateTime.Now.Ticks).Next(0, Elements.Count - 1);
            Max = Elements.Max();
            Min = Elements.Min();
            Avg = Elements.First(x => x > (Min + Max) / 2);
            Random = Elements[RandomIndex];
            NotFound = Elements.Max() + 1;
        }
    }
}
