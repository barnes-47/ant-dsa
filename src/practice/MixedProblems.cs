namespace Practice
{
    using System.Collections.Generic;

    public class MixedProblems
    {
        #region Public Methods
        public int GetFibonacci(int num)
        {
            var map = new Dictionary<int, int>();
            var sequence = _Fibonacci(map, num);

            return sequence;
        }
        #endregion

        #region Private Methods
        private int _Fibonacci(IDictionary<int, int> map, int n)
        {
            if (map.ContainsKey(n))
                return map[n];
            if (n <= 2)
                return 1;

            var seq = _Fibonacci(map, n - 1) + _Fibonacci(map, n - 2);
            return seq;
        }
        #endregion
    }
}
