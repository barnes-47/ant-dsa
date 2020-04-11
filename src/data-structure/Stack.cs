using System;

namespace Ds
{
    public class Stack
    {
        private char[] _array;
        private int _size;

        public int Count => _size;
        public bool IsOverflow => _size == _array.Length - 1;
        public bool IsUnderflow => _size == -1;
        public bool HasSingleElement => _size == 0;

        public Stack()
        {
            _size = -1;
            _array = Array.Empty<char>();
        }

        public Stack(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Invalid capacity value.");

            _size = -1;
            _array = new char[capacity];
        }

        public Stack(string str)
            : this(str.ToCharArray())
        {

        }

        public Stack(char[] array)
        {
            _size = array.Length - 1;
            _array = new char[array.Length];
            Array.Copy(array, 0, _array, 0, _array.Length);
        }

        #region Basic Operations
        public void Push(char data)
        {
            if (IsOverflow)
                return;

            _array[++_size] = data;
        }

        public char Pop() => IsUnderflow ? (default) : _array[_size--];

        public char Peek() => IsUnderflow ? (default) : _array[_size];

        public void Reverse()
        {
            if (IsUnderflow)
                return;
            if (HasSingleElement)
                return;
            var size = (_size & 1) == 1 ? _size / 2 : (_size / 2) - 1;
            for (int i = 0, j = _size; i <= size && size < j; i++, j--)
            {
                var temp = _array[i];
                _array[i] = _array[j];
                _array[j] = temp;
            }
        }

        public void DeleteAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Invalid index.");
            if (index > _size)
                throw new ArgumentOutOfRangeException(nameof(index), index, "Invalid index.");
            for (var i = index; i + 1 <= _size; ++i)
            {
                _array[i] = _array[i + 1];
            }
            --_size;
        }

        public void PrintToConsole()
        {
            for (var i = 0; i <= _size; ++i)
            {
                Console.WriteLine(_array[i]);
            }
        }
        #endregion
    }
}
