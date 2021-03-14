namespace Algo.Lists.SortingAlgorithms.Models
{
    using System.Collections.Generic;

    public class SortResult
    {
        public IList<int> Elements { get; set; } = new List<int>();
        public long Ticks { get; set; }
    }
}
