using System.Collections;
using System.Collections.Generic;

namespace Algo.Lists.SortingAlgorithms.Models
{
    public class SortResult
    {
        public IList<int> Elements { get; set; } = new List<int>();
        public long Ticks { get; set; }
    }
}
