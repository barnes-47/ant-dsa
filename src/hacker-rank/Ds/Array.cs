namespace HackerRank.Ds
{
    public class Array
    {
        public T[] Reverse<T>(T[] array)
        {
            if (array == null)
                Throw

            var length = array.Length % 2 == 0 ? array.Length / 2 : (array.Length - 1) / 2;
            T temp;
            for (var i = 0; i < length; ++i)
            {
                temp = array[array.Length - i - 1];
                array[array.Length - i - 1] = array[i];
                array[i] = temp;
            }

            return array;
        }
    }
}
