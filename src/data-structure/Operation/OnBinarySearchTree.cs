using Ds.Generic;
using Ds.Generic.Tree;
using System;

namespace Ds.Operation
{
    public static class OnBinarySearchTree
    {
        public static void NonRecursive_InOrderTreeWalk_UsingStack<T>(
            this BinarySearchTree<T> tree
            , Action<T> action)
            where T : IComparable<T>
        {
            var stack = new Stack<BinarySearchTreeNode<T>>();
            var current = tree.Root;
            while (true)
            {
                while(current != null)
                {
                    stack.Push(current);
                    current = current.Left;
                }

                if (stack.IsEmpty)
                    return;

                current = stack.Pop();
                action(current.Item);
                current = current.Right;
            }
        }
    }
}
