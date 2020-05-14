using Ds.Helper;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Ds.Generic
{
    public class QueueUsingArray<T> : IEnumerable<T>, ICollection
    {
        #region Private Constants
        private const int _DefaultCapacity = 8;
        private const int _GrowFactor = 2;
        #endregion

        #region Private Static Variables
        private static T[] _emptyArray = new T[0];
        #endregion

        #region Private Variables
        private object _syncRoot;
        private T[] _array;
        private int _head;
        private int _tail;
        #endregion

        #region Private Properties        
        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is empty; otherwise, <c>false</c>.
        /// </value>
        private bool IsEmpty => Count < 1;

        /// <summary>
        /// Gets a value indicating whether this instance is full.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is full; otherwise, <c>false</c>.
        /// </value>
        private bool IsFull => Count == _array.Length;
        #endregion

        #region Ctor        
        /// <summary>
        /// Initializes a new instance of the <see cref="QueueUsingArray{T}"/> class.
        /// </summary>
        public QueueUsingArray()
        {
            _array = _emptyArray;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueUsingArray{T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public QueueUsingArray(int capacity)
        {
            if (capacity < 0)
                Throw.ArgumentOutOfRangeException(nameof(capacity), capacity, Message.Common.NonNegativeCapacity);

            _array = new T[capacity];
            Count = _head = _tail = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="QueueUsingArray{T}"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public QueueUsingArray(IEnumerable<T> collection)
        {
            if (collection == null)
                Throw.ArgumentNullException(nameof(collection));

            _array = new T[_DefaultCapacity];
            Count = 0;

            using var en = collection.GetEnumerator();
            while (en.MoveNext())
            {
                Enqueue(en.Current);
            }
        }
        #endregion

        #region Public Properties        
        /// <summary>
        /// Gets the number of elements contained in the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized (thread safe).
        /// </summary>
        public bool IsSynchronized => false;

        /// <summary>
        /// Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                    System.Threading.Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);
                return _syncRoot;
            }
        }
        #endregion

        #region Public Methods        
        /// <summary>
        /// Determines whether this instance contains the object.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>
        ///   <c>true</c> if [contains] [the specified item]; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(T item)
        {
            var index = _head;
            var count = Count;
            var comparer = EqualityComparer<T>.Default;
            while(count-- > 0)
            {
                if (item == null)
                    if (_array[index] == null)
                        return true;
                if (_array[index] != null && comparer.Equals(_array[index], item))
                    return true;
                index = (index + 1) % _array.Length;
            }

            return false;
        }

        /// <summary>
        /// Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from <see cref="T:System.Collections.ICollection" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
        /// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        public void CopyTo(Array array, int index)
        {
            if (array == null)
                Throw.ArgumentNullException(nameof(array));
            if (index < 0)
                Throw.ArgumentOutOfRangeException(nameof(index), index, Message.Common.NonNegativeArrayIndex);
            if (index > array.Length)
                Throw.ArgumentOutOfRangeException(nameof(index), index, Message.Common.ArrayIndexCannotGreaterThanArraySize);
            if (Count > array.Length - index)
                Throw.ArgumentException(Message.Common.ArraySizeLess);
            if (_head < _tail)
            {
                Array.Copy(_array, 0, array, index, Count);
                return;
            }

            Array.Copy(_array, _head, array, 0, _array.Length - _head);
            Array.Copy(_array, 0, array, _array.Length - _head, _tail);
        }

        /// <summary>
        /// Returns the item at the head of the queue.
        /// Moves the head to the next element in the queue.
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            if (IsEmpty)
                Throw.InvalidOperationException(Message.QueueUsingArray.Empty);

            var item = _array[_head];
            _array[_head] = default;
            _head = (_head + 1) % _array.Length;
            --Count;

            return item;
        }

        /// <summary>
        /// Add item to the tail of the queue.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Enqueue(T item)
        {
            if (IsFull)
            {
                var newCapacity = _array.Length * _GrowFactor;
                if (newCapacity < _DefaultCapacity)
                    newCapacity = _DefaultCapacity;
                SetCapacity(newCapacity);
            }

            _array[_tail] = item;
            _tail = (_tail + 1) % _array.Length;
            ++Count;
        }

        /// <summary>Returns an enumerator that iterates through a collection.</summary>
        /// <returns>An <see cref="T:System.Collections.IEnumerator">IEnumerator</see> object that can be used to iterate through the collection.</returns>
        public IEnumerator GetEnumerator()
            => new Enumerator(this);

        /// <summary>Returns an enumerator that iterates through the collection.</summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
            => new Enumerator(this);

        /// <summary>Gets the element.</summary>
        /// <param name="i">The index.</param>
        /// <returns></returns>
        internal T GetElement(int i)
            => _array[(_head + i) % _array.Length];

        /// <summary>Peeks this instance.</summary>
        /// <returns></returns>
        public T Peek()
        {
            if (IsEmpty)
                Throw.InvalidOperationException(Message.QueueUsingArray.Empty);

            return _array[_head];
        }
        #endregion

        #region Private Methods

        /// <summary>Sets the capacity.</summary>
        /// <param name="capacity">The capacity.</param>
        private void SetCapacity(int capacity)
        {
            var newArray = new T[capacity];
            if (Count > 0)
            {
                if (_head < _tail)
                    Array.Copy(_array, _head, newArray, 0, Count);
                else
                {
                    Array.Copy(_array, _head, newArray, 0, _array.Length - _head);
                    Array.Copy(_array, 0, newArray, _array.Length - _head, _tail);
                }
            }

            _array = newArray;
            _head = 0;
            _tail = (Count == capacity) ? 0 : Count;
        }
        #endregion

        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private readonly QueueUsingArray<T> _q;
            private T _current;
            private int _index;     // -1 = not started, -2 = ended/disposed

            /// <summary>
            /// Initializes a new instance of the <see cref="Enumerator"/> struct.
            /// </summary>
            /// <param name="q">The queue.</param>
            public Enumerator(QueueUsingArray<T> q)
            {
                _q = q;
                _current = default;
                _index = -1;
            }

            /// <summary>
            /// Gets the current.
            /// </summary>
            /// <value>The current.</value>
            public T Current
            {
                get
                {
                    if (_index <= -2)
                        Throw.InvalidOperationException(Message.Enumerator.OperationEnded);
                    if (_index == -1)
                        Throw.InvalidOperationException(Message.Enumerator.OperationNotStarted);

                    return _current;
                }
            }

            /// <summary>
            /// Gets the current.
            /// </summary>
            /// <value>The current.</value>
            object IEnumerator.Current => Current;

            /// <summary>
            /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
            /// </summary>
            public void Dispose()
            {
                _index = -2;
                _current = default;
            }

            /// <summary>
            /// Advances the enumerator to the next element of the collection.
            /// </summary>
            /// <returns>
            ///   <see langword="true" /> if the enumerator was successfully advanced to the next element; <see langword="false" /> if the enumerator has passed the end of the collection.
            /// </returns>
            public bool MoveNext()
            {
                if (_index <= -2)
                    return false;

                ++_index;
                if (_index == _q.Count)
                {
                    _index = -2;
                    _current = default;
                    return false;
                }

                _current = _q.GetElement(_index);
                return true;
            }

            /// <summary>
            /// Sets the enumerator to its initial position, which is before the first element in the collection.
            /// </summary>
            public void Reset()
            {
                _index = -1;
                _current = default;
            }
        }
    }
}
