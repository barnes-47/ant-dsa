using System;

namespace Ds
{
    public class Stack
    {
        private readonly char[] _array;

        public int Count { get; private set; }
        public bool IsOverflow => Count == _array.Length - 1;
        public bool IsUnderflow => Count == -1;
        public bool HasSingleElement => Count == 0;

        public Stack()
        {
            Count = -1;
            _array = Array.Empty<char>();
        }

        public Stack(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Invalid capacity value.");

            Count = -1;
            _array = new char[capacity];
        }

        public Stack(string str)
            : this(str.ToCharArray())
        {

        }

        public Stack(char[] array)
        {
            Count = array.Length - 1;
            _array = new char[array.Length];
            Array.Copy(array, 0, _array, 0, _array.Length);
        }

        #region Basic Operations
        /// <summary>
        /// Pushes data on the top of the stack.
        /// </summary>
        /// <param name="data"></param>
        public void Push(char data)
        {
            if (IsOverflow)
                return;

            _array[++Count] = data;
        }

        /// <summary>
        /// Gets the top element on the stack and decreases the Count by 1.
        /// </summary>
        /// <returns></returns>
        public char Pop() => IsUnderflow ? (default) : _array[Count--];

        /// <summary>
        /// Gets the top element on the stack. Count remains intact.
        /// </summary>
        /// <returns></returns>
        public char Peek() => IsUnderflow ? (default) : _array[Count];

        /// <summary>
        /// Reverse the elements of the stack.
        /// </summary>
        public void Reverse()
        {
            if (IsUnderflow)
                return;
            if (HasSingleElement)
                return;
            var size = (Count & 1) == 1 ? Count / 2 : (Count / 2) - 1;
            for (int i = 0, j = Count; i <= size && size < j; i++, j--)
            {
                var temp = _array[i];
                _array[i] = _array[j];
                _array[j] = temp;
            }
        }

        /// <summary>
        /// Deletes the element at the specified index.
        /// </summary>
        /// <param name="index">The integer index.</param>
        public void DeleteAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Invalid index.");
            if (index > Count)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Invalid index.");
            for (var i = index; i + 1 <= Count; ++i)
            {
                _array[i] = _array[i + 1];
            }
            --Count;
        }

        /// <summary>
        /// Prints the stack on the console.
        /// </summary>
        public void PrintToConsole()
        {
            for (var i = 0; i <= Count; ++i)
            {
                Console.WriteLine(_array[i]);
            }
        }
        #endregion
    }
}
