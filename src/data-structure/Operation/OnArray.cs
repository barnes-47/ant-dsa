using Ds.Helper;

namespace Ds.Operation
{
    public class OnArray
    {
        public void ToReverseIt<T>(T[] array)
        {
            T temp;
            for (var i = 0; i < array.Length; i++)
            {
                temp = array[array.Length - i - 1];
                array[array.Length - i - 1] = array[i];
                array[i] = temp;
            }
        }

        /// <summary>
        /// Rotates an array passed.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array">Array to be rotated.</param>
        /// <param name="index">Index of the element from where the rotation must start.</param>
        /// <param name="numberOfElements">Number of elements to be rotated.</param>
        public void ToRotateIt<T>(T[] array, int index, int numberOfElements)
        {
            if (index < 0)
                Throw.ArgumentOutOfRangeException(nameof(index));
            //var currentIndexOfNewFirstElement = index > 0 ? index - 1 : ;
            var newIndexOfCurrentFirstElement = array.Length - numberOfElements + index;
            //var tempElement = 
        }
    }
}
