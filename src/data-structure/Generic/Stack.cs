namespace Ds.Generic
{
    using Ds.Helper;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IEnumerable<T>
    {
        #region Private Constants
        private const int _defaultCapacity = 8;
        #endregion

        #region Private Variables
        private T[] _array;
        private static readonly T[] _emptyArray = new T[0];
        #endregion

        #region Public Properties
        public int Count { get; private set; }
        public bool IsEmpty => Count < 1;
        public bool IsNotEmpty => Count > 0;
        public bool IsOverflow => Count >= _array.Length;
        #endregion

        #region Public Ctors
        public Stack()
        {
            _array = _emptyArray;
            Count = 0;
        }

        public Stack(int capacity)
        {
            if (capacity < 0)
                Throw.ArgumentOutOfRangeException(nameof(capacity), capacity, Message.Stack.NonNegativeCapacity);

            _array = new T[capacity];
        }

        public Stack(IEnumerable<T> collection)
        {
            if (collection == null)
                Throw.ArgumentNullException(nameof(collection));

            if (collection is ICollection<T> coll)
            {
                var count = coll.Count;
                _array = new T[count];
                coll.CopyTo(_array, 0);
                Count = count;
            }
            else
            {
                Count = 0;
                _array = new T[_defaultCapacity];

                using var en = collection.GetEnumerator();
                while (en.MoveNext())
                {
                    Push(en.Current);
                }
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Removes all objects from the stack.
        /// </summary>
        public void Clear()
        {
            Array.Clear(_array, 0, Count);
            Count = 0;
        }

        /// <summary>
        /// Checks if the item exists in the Stack or not.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>True if the item exists, false otherwise.</returns>
        public bool Contains(T item)
        {
            var comparer = EqualityComparer<T>.Default;
            var count = Count;
            while(count-- > 0)
            {
                if ((object)item == null)
                {
                    if ((object)_array[count] == null)
                        return true;
                }
                if (_array[count] != null && comparer.Equals(_array[count], item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Copies the Stack to the array starting with the index at: arrayIndex,
        /// in the same manner as Pop would return.
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                Throw.ArgumentNullException(nameof(array));
            if (arrayIndex < 0)
                Throw.ArgumentOutOfRangeException(nameof(arrayIndex), Message.Common.NonNegativeArrayIndex);
            if (arrayIndex > array.Length)
                Throw.ArgumentOutOfRangeException(nameof(arrayIndex), Message.Common.ArrayIndexCannotGreaterThanArraySize);
            if (Count > array.Length - arrayIndex)
                Throw.ArgumentException(Message.Common.ArraySizeLess);

            Array.Copy(_array, 0, array, arrayIndex, Count);
            Array.Reverse(array, arrayIndex, Count);
        }

        /// <summary>
        /// Returns top item of the stack without removing it.
        /// If the stack is empty, Peek throws an InvalidOperationException.
        /// </summary>
        /// <returns>The top item of the stack.</returns>
        public T Peek()
        {
            if (IsEmpty)
                Throw.InvalidOperationException(Message.Stack.Empty);

            return _array[Count - 1];
        }

        /// <summary>
        /// Pops an item from the top of the stack.
        /// If the stack is empty, Pop throws an InvalidOperationException.
        /// </summary>
        /// <returns>The top item of the stack.</returns>
        public T Pop()
        {
            if (IsEmpty)
                Throw.InvalidOperationException(Message.Stack.Empty);

            var item = _array[--Count];
            _array[Count] = default;        // Free memory quicker.

            return item;
        }

        /// <summary>
        /// Pushes an item to the top of the stack.
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            if (IsOverflow)
            {
                var newArray = new T[(_array.Length == 0) ? _defaultCapacity : 2 * _array.Length];
                Array.Copy(_array, 0, newArray, 0, _array.Length);
                _array = newArray;
            }

            _array[Count++] = item;
        }

        /// <summary>
        /// Copies the stack to an array, in the same order Pop would return the items.
        /// </summary>
        /// <returns>The array.</returns>
        public T[] ToArray()
        {
            var copy = new T[Count];
            var i = 0;
            while (i < Count)
            {
                copy[i] = _array[Count - i - 1];
                i++;
            }

            return copy;
        }

        /// <summary>
        /// Frees the excess array memory, when the actual size of Stack is less than 75% of the total allocated size of the Stack.
        /// </summary>
        public void TrimExcess()
        {
            var threshold = (int)((double)_array.Length * 0.75);
            if (Count >= threshold)
                return;

            var newArray = new T[Count];
            Array.Copy(_array, 0, newArray, 0, Count);
            _array = newArray;
        }

        /// <summary>
        /// Enumerator for the Stack.
        /// </summary>
        /// <returns></returns>
        public Enumerator GetEnumerator() => new Enumerator(this);

        /// <summary>
        /// Enumerator of IEnumerable<T> implemented by Stack.
        /// </summary>
        /// <returns></returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        /// <summary>
        /// Enumerator of IEnumerable implemented by Stack.
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);
        #endregion

        #region Public Struct
        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly Stack<T> _stack;
            private int _index;
            private T _current;

            private bool IsFirstCall => _index <= -2;
            private bool IsEndOfEnumeration => _index == -1;

            internal Enumerator(Stack<T> stack)
            {
                _stack = stack;
                _index = -2;
                _current = default;
            }

            public T Current
            {
                get
                {
                    if (IsFirstCall)
                        Throw.InvalidOperationException(Message.Enumerator.OperationNotStarted);
                    if (IsEndOfEnumeration)
                        Throw.InvalidOperationException(Message.Enumerator.OperationEnded);
                    return _current;
                }
            }

            object IEnumerator.Current => Current;

            public void Dispose() => _index = -1;

            public bool MoveNext()
            {
                bool hasElement;
                if (IsFirstCall)
                {
                    _index = _stack.Count - 1;
                    hasElement = _index >= 0;
                    if (hasElement)
                        _current = _stack._array[_index];

                    return hasElement;
                }
                if (IsEndOfEnumeration)
                    return false;

                hasElement = --_index >= 0;
                _current = hasElement ? _stack._array[_index] : (default);

                return hasElement;
            }

            public void Reset()
            {
                _index = -2;
                _current = default;
            }
        }
        #endregion
    }
}
