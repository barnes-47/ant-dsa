using System;
using System.Collections.Generic;
using System.Text;

namespace Ds.LinkedList
{
    public class Node
    {
        public long Data { get; }
        public Node Next { get; set; }

        public Node(long data)
        {
            Data = data;
            Next = null;
        }
    }

    public class Singly
    {
        #region Private Constants
        public const string ExceptionMessageWhenEmpty = "The singly linked list is empty.";
        #endregion

        #region Public Getters
        public Node Head { get; private set; }
        public Node Tail { get; private set; }
        public ulong Count { get; private set; }
        public bool IsNull => this == null;
        public bool IsEmpty => Count == 0 && Head == null && Tail == null;
        public bool HasLoop => InternalHasLoop();
        #endregion

        #region Private Properties
        
        #endregion

        #region Public Constructors
        public Singly()
        {
            
        }

        public Singly(long value)
        {
            AddLast(value);
        }

        public Singly(long[] values)
        {
            foreach (var value in values)
            {
                AddLast(value);
            }
        }
        #endregion

        #region Basic Public Methods
        /// <summary>
        /// Adds a newly created node with specified data, at the end of the list.
        /// Tail now points to this newly added node.
        /// Increases the value of Length by 1 after successful insertion of node.
        /// </summary>
        /// <param name="data"></param>
        public void Add(long data) => AddLast(data);

        /// <summary>
        /// Adds a newly created node with specified data, at the start of the list.
        /// Head now points to this newly added node.
        /// Increases the value of Length by 1 after successful insertion of node.
        /// </summary>
        /// <param name="data"></param>
        public void AddFirst(long data)
        {
            var newNode = new Node(data);
            if (IsEmpty)
            {
                Head = Tail = newNode;
                ++Count;
                return;
            }

            newNode.Next = Head;
            Head = newNode;
            ++Count;
        }

        /// <summary>
        /// Adds a newly created node with specified data, at the end of the list.
        /// Tail now points to this newly added node.
        /// Increases the value of Length by 1 after successful insertion of node.
        /// </summary>
        /// <param name="data"></param>
        public void AddLast(long data)
        {
            var newNode = new Node(data);
            if (IsEmpty)
            {
                Head = Tail = newNode;
                ++Count;
                return;
            }

            Tail.Next = newNode;
            Tail = newNode;
            ++Count;
        }

        /// <summary>
        /// Gets the node for the specified data, if it exists in the list, null otherwise.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public Node Find(long data)
        {
            var current = Head;
            while(current != null)
            {
                if (current.Data == data)
                    return current;
                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// Removes first occurrence of the node with specified data from the list.
        /// Decreases the value of Length by 1.
        /// </summary>
        /// <param name="data">The data to be deleted.</param>
        public bool Remove(long data)
        {
            if (IsEmpty)
                throw new Exception(ExceptionMessageWhenEmpty);
            if (IsHead(data))
            {
                Head = Head.Next;
                if (--Count == 0)
                    Tail = null;

                return true;
            }

            var current = Head.Next;
            var previous = Head;
            while (current != null)
            {
                if (current.Data == data)
                {
                    previous.Next = current.Next;
                    if (previous.Next == null)
                        Tail = previous;
                    current.Next = null;
                    --Count;

                    return true;
                }
                previous = current;
                current = current.Next;
            }

            return false;
        }
        #endregion

        #region Custom Public Methods
        /// <summary>
        /// Adds a new node containing newData after a node containing existingData.
        /// Increases the value of Length by 1 after successful insertion of node.
        /// </summary>
        /// <param name="existingData"></param>
        /// <param name="newData"></param>
        public void AddAfter(long existingData, long newData)
        {
            if (IsEmpty)
                throw new Exception(ExceptionMessageWhenEmpty);
            if (IsHead(existingData))
            {
                AddAfterHead(newData);
                return;
            }
            if (IsTail(existingData))
            {
                AddLast(newData);
                return;
            }

            var current = Head.Next;
            while(current != null)
            {
                if(current.Data == existingData)
                {
                    var newNode = new Node(newData)
                    {
                        Next = current.Next
                    };
                    current.Next = newNode;
                    ++Count;
                    break;
                }
                current = current.Next;
            }
        }

        /// <summary>
        /// Adds newly created node with the specified data, after the head of the list.
        /// Increases the value of Length by 1 after successful insertion of node.
        /// </summary>
        /// <param name="data"></param>
        public void AddAfterHead(long data)
        {
            if (IsEmpty)
                throw new Exception(ExceptionMessageWhenEmpty);
            var newNode = new Node(data)
            {
                Next = Head.Next
            };
            Head.Next = newNode;
            ++Count;
        }

        /// <summary>
        /// Checks if the node is head or not.
        /// Compares only data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsHead(long data) => Head.Data == data;

        /// <summary>
        /// Checks if the node is tail or not.
        /// Compares only data.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsTail(long data) => Tail.Data == data;

        /// <summary>
        /// Checks if the data passed, exists in the list or not.
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true if data exists in the list, false otherwise.</returns>
        public bool Contains(long data)
        {
            if (IsEmpty)
                return false;
            if (IsHead(data))
                return true;
            if (IsTail(data))
                return true;

            var current = Head.Next;
            while(current != null)
            {
                if (current.Data == data)
                    return true;
                current = current.Next;
            }

            return false;
        }
        #endregion

        #region Geeks-For-Geeks Public Methods
        
        #endregion

        #region Private Methods
        private bool InternalHasLoop()
        {
            var hashSet = new HashSet<Node>();
            var current = Head;
            while(current != null)
            {
                if (hashSet.Contains(current))
                    return true;
                hashSet.Add(current);
                current = current.Next;
            }

            return false;
        }
        #endregion
    }
}
