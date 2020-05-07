using Ds.Helper;
using System.Collections;
using System.Collections.Generic;

namespace Ds.Generic.LinkedList
{
    public sealed class SinglyNode<T>
    {
        #region Private Variables
        private EqualityComparer<T> _comparer;
        #endregion
        public T Item { get; }
        public SinglyNode<T> Next { get; internal set; }

        public SinglyNode(T item)
        {
            _comparer = EqualityComparer<T>.Default;
            Item = item;
            Next = null;
        }

        internal void Invalidate()
            => Next = null;
    }

    public class Singly<T> : ICollection<T>
    {
        #region Public Properties
        public SinglyNode<T> Head { get; internal set; }
        public SinglyNode<T> Tail { get; internal set; }
        public int Count { get; internal set; }
        public bool IsEmpty => Head == null && Tail == null && Count == 0;
        public bool IsReadOnly => false;
        #endregion

        #region Ctors
        public Singly()
        {

        }
        public Singly(IEnumerable<T> collection)
        {
            if (collection == null)
                Throw.ArgumentNullException(nameof(collection));

            foreach (var item in collection)
            {
                AddLast(item);
            }
        }
        #endregion

        public void Add(T item)
            => AddLast(item);

        public void AddFirst(T item)
        {
            var node = new SinglyNode<T>(item);
            if (IsEmpty)
            {
                InternalAddFirstNode(node);
                return;
            }

            node.Next = Head;
            Head = node;
            ++Count;
        }

        public void AddLast(T item)
        {
            var node = new SinglyNode<T>(item);
            if (IsEmpty)
            {
                InternalAddFirstNode(node);
                return;
            }

            Tail.Next = node;
            Tail = node;
            ++Count;
        }

        public void Clear()
        {
            var current = Head;
            while (current != null)
            {
                var _ = current;
                current = current.Next;
                _.Invalidate();
            }

            InternalReset();
        }

        public bool Contains(T item)
            => Find(item) != null;

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
            if (IsEmpty)
                return;

            var current = Head;
            while (current != null)
            {
                array[arrayIndex++] = current.Item;
                current = current.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
            => new Enumerator(this);

        public bool Remove(T item)
        {
            if (IsEmpty)
                return false;

            var comparer = EqualityComparer<T>.Default;
            if (Head.Next == null)
            {
                if (!comparer.Equals(Head.Item, item))
                    return false;

                InternalReset();
                return true;
            }

            var prev = Head;
            var current = Head.Next;
            while (current != null)
            {
                if (comparer.Equals(current.Item, item))
                {
                    prev.Next = current.Next;
                    if (Tail == current)
                        Tail = prev;
                    current.Invalidate();
                    --Count;
                    return true;
                }

                prev = current;
                current = current.Next;
            }

            return false;
        }

        public void RemoveFirst()
        {
            if (IsEmpty)
                Throw.InvalidOperationException(Message.SinglyLinkedList.Empty);
            if (Head.Next == null)
            {
                InternalReset();
                return;
            }

            var _ = Head;
            Head = Head.Next;
            _.Invalidate();
            --Count;
        }

        public void RemoveLast()
        {
            if (IsEmpty)
                Throw.InvalidOperationException(Message.SinglyLinkedList.Empty);
            if (Head.Next == null)
            {
                InternalReset();
                return;
            }

            var prev = Head;
            var current = Head.Next;
            while (current.Next != null)
            {
                prev = current;
                current = current.Next;
            }

            Tail = prev;
            Tail.Invalidate();
            --Count;
        }

        IEnumerator IEnumerable.GetEnumerator()
            => new Enumerator(this);

        #region Private Methods
        private SinglyNode<T> Find(T item)
        {
            var comparer = EqualityComparer<T>.Default;
            var current = Head;
            while (current != null)
            {
                if (comparer.Equals(current.Item, item))
                    return current;
                current = current.Next;
            }

            return null;
        }
        private void InternalAddFirstNode(SinglyNode<T> node)
        {
            if (node == null)
                Throw.ArgumentNullException(nameof(node));

            Head = Tail = node;
            ++Count;
        }
        private void InternalReset()
        {
            Head = Tail = null;
            Count = 0;
        }
        #endregion

        public struct Enumerator : IEnumerator<T>, IEnumerator
        {
            private Singly<T> _list;
            private SinglyNode<T> _node;
            private T _current;
            private int _index;

            private bool IsFirstCall => _index < 1;
            private bool IsEndOfEnumeration => _index > _list.Count;

            internal Enumerator(Singly<T> singlyList)
            {
                _list = singlyList;
                _node = singlyList.Head;
                _current = default;
                _index = 0;
            }

            object IEnumerator.Current => Current;

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

            public void Dispose()
            {
                _index = 0;
            }

            public bool MoveNext()
            {
                if (_node == null)
                {
                    _index = _list.Count + 1;
                    return false;
                }

                ++_index;
                _current = _node.Item;
                _node = _node.Next;
                return true;
            }

            public void Reset()
            {
                _current = default;
                _node = _list.Head;
                _index = 0;
            }
        }
    }
}
