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
        #region Public Getters
        public Node Head { get; private set; }
        public Node Tail { get; private set; }
        public ulong Length { get; private set; }
        public bool IsNull => this == null;
        public bool IsEmpty => Length == 0 && Head == null && Tail == null;
        #endregion

        #region Public Methods
        public void AddFirst(long data)
        {
            var newNode = new Node(data);
            if (IsEmpty)
            {
                Head = Tail = newNode;
                ++Length;
                return;
            }

            newNode.Next = Head;
            Head = newNode;
            ++Length;
        }
        
        public void AddLast(long data)
        {
            var newNode = new Node(data);
            if (IsEmpty)
            {
                Head = Tail = newNode;
                ++Length;
                return;
            }

            Tail.Next = newNode;
            Tail = newNode;
            ++Length;
        }

        public void AddAfter(Node existingNode, long data) => AddAfter(existingNode, new Node(data));
        public void AddAfter(Node existingNode, Node newNode)
        {
            if (existingNode == null)
                throw new ArgumentNullException(nameof(existingNode));
            if (IsEmpty)
            {
                Head = existingNode;
                Head.Next = Tail = newNode;
                Length += 2;

                return;
            }
            if (IsHead(existingNode))
            {
                AddAfterHead(existingNode);

                return;
            }
            if (IsTail(existingNode))
            {
                AddAfterTail(existingNode);

                return;
            }

            var current = Head.Next;
            while(current != null)
            {
                if (current.Data == existingNode.Data)
                {
                    newNode.Next = existingNode.Next;
                    existingNode.Next = newNode;
                    ++Length;

                    break;
                }
                current = current.Next;
            }
        }

        public void Delete(long data)
        {

        }

        public bool IsHead(long data) => IsHead(new Node(data));

        public bool IsHead(Node node) => Head.Data == node.Data;

        public bool IsTail(long data) => IsTail(new Node(data));

        public bool IsTail(Node node) => Tail.Data == node.Data;

        public bool Exists(long data) => Exists(new Node(data));

        public bool Exists(Node node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (IsEmpty)
                return false;
            if (IsHead(node))
                return true;
            if (IsTail(node))
                return true;

            var current = Head.Next;
            while(current != null)
            {
                if (current.Data == node.Data)
                    return true;
                current = current.Next;
            }

            return false;
        }
        #endregion

        #region Private Methods
        private void AddAfterHead(long data) => AddAfterHead(new Node(data));

        private void AddAfterHead(Node node)
        {
            node.Next = Head.Next;
            Head = node;
            ++Length;
        }

        private void AddAfterTail(long data) => AddAfterTail(new Node(data));

        private void AddAfterTail(Node node)
        {
            Tail.Next = node;
            Tail = node;
            ++Length;
        }
        #endregion
    }
}
