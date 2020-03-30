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
        /// <summary>
        /// Returns an element at the specified index, 0 otherwise.
        /// Follows a zero-based index.
        /// </summary>
        /// <param name="index">The index</param>
        /// <returns></returns>
        public long ElementAt(ulong index)
        {
            if (index >= Count)
                return 0;
            if (index == 0)
                return Head.Data;
            if (index == Count - 1)
                return Tail.Data;

            var current = Head.Next;
            var internalIndex = 1UL;
            while(current != null)
            {
                if (internalIndex++ == index)
                    return current.Data;
                current = current.Next;
            }

            return 0;
        }

        /// <summary>
        /// Returns true if a cycle is detected in the list, false otherwise.
        /// Uses "The Tortoise and the Hare Algorithm".
        /// </summary>
        /// <returns></returns>
        public bool LoopExists()
        {
            if (IsEmpty)
                return false;
            if (Tail.Next == null)  // No loop detected when Tail points to --> null.
                return false;
            if (Tail.Next.Equals(Head) && Tail.Next.Data == Head.Data)  // Loop detected when Tail points to --> Head.
                return true;
            if (Tail.Next.Equals(Tail) && Tail.Next.Data == Tail.Data)  // Loop detected when Tail points to --> Tail.
                return true;

            var tortoise = Head;
            var hare = Head.Next;
            while(hare != null)
            {
                if (hare.Next == null)
                    return false;
                if (hare.Equals(tortoise) && hare.Data == tortoise.Data)
                    return true;
                hare = hare.Next.Next;
                tortoise = tortoise.Next;
            }

            return false;
        }

        /// <summary>
        /// Gets the number of elements forming the loop in the list.
        /// </summary>
        /// <returns></returns>
        public ulong LoopSize()
        {
            if (IsEmpty)
                return 0;

            var tortoise = Head;
            var hare = Head.Next;
            while(tortoise != null && hare != null)
            {
                if (hare.Next == null)
                    return 0;
                if (hare.Equals(tortoise) && hare.Data == tortoise.Data)
                {
                    var count = 1UL;
                    var temp = tortoise;
                    while(temp.Next != tortoise)
                    {
                        ++count;
                        temp = temp.Next;
                    }

                    return count;
                }
                hare = hare.Next.Next;
                tortoise = tortoise.Next;
            }

            return 0;
        }

        /// <summary>
        /// Gets the first node of the loop, null if no loop is detected.
        /// Uses The Tortoise and Hare Algorithm.
        /// </summary>
        /// <returns></returns>
        public Node GetsLoopStartUsingTortoiseHare()
        {
            if (IsEmpty)
                return null;

            var tortoise = Head;
            var hare = Head.Next;
            while(tortoise != null && hare != null)
            {
                if (hare.Next == null)
                    return null;
                if (hare.Equals(tortoise) && hare.Data == tortoise.Data)
                {
                    tortoise = Head;
                    while(tortoise != hare.Next && tortoise.Data != hare.Next.Data)
                    {
                        tortoise = tortoise.Next;
                        hare = hare.Next;
                    }

                    return tortoise;
                }
                hare = hare.Next.Next;
                tortoise = tortoise.Next;
            }

            return null;
        }

        /// <summary>
        /// Gets the first node of the loop, null if no loop is detected.
        /// Using the HashSet to store the nodes and returning the one that already exists.
        /// </summary>
        /// <returns></returns>
        public Node GetsLoopStartUsingHashSet()
        {
            var nodeHashSet = new HashSet<Node>();
            var current = Head;
            while(current != null)
            {
                if (nodeHashSet.Contains(current))
                    return current;
                nodeHashSet.Add(current);
                current = current.Next;
            }

            return null;
        }

        /// <summary>
        /// Reverses a the list without creating a new list.
        /// </summary>
        public void Reverse()
        {
            if (IsEmpty)
                return;

            Node current = Head, previous = null;
            while(current != null)
            {
                var next = current.Next;
                current.Next = previous;
                previous = current;
                current = next;
            }

            current = Head;
            Head = Tail;
            Tail = current;
        }

        /// <summary>
        /// Checks if the list is a palindrome or not.
        /// </summary>
        /// <returns>true if the list is palindrome, false otherwise.</returns>
        public bool IsPalindrome()
        {
            var reverseSingly = ReverseClone();
            if (reverseSingly == null)
                return false;

            var reverseCurrent = reverseSingly.Head;
            var actualCurrent = Head;
            while(actualCurrent != null && reverseCurrent != null)
            {
                if (actualCurrent.Data != reverseCurrent.Data)
                    return false;
                actualCurrent = actualCurrent.Next;
                reverseCurrent = reverseCurrent.Next;
            }

            return true;
        }

        /// <summary>
        /// Creates a new list with the data of current list.
        /// List must have atleast one element to perform clone operation.
        /// </summary>
        /// <returns>Head node of the newly created list. Null if the list is empty.</returns>
        public Singly Clone()
        {
            if (IsEmpty)
                return null;

            var newSingly = new Singly();
            var current = Head;
            while(current != null)
            {
                newSingly.AddLast(current.Data);
                current = current.Next;
            }

            return newSingly;
        }

        /// <summary>
        /// Creates a new list in reverse order with the data of current list.
        /// List must have atleast one element to perform reverse-clone operation.
        /// </summary>
        /// <returns>Head node of the newly created list. Null if the list is empty.</returns>
        public Singly ReverseClone()
        {
            if (IsEmpty)
                return null;

            var newSingly = new Singly();
            var current = Head;
            while(current != null)
            {
                newSingly.AddFirst(current.Data);
                current = current.Next;
            }

            return newSingly;
        }
        #endregion
    }
}
