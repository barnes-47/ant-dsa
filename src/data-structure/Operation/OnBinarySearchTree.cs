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

        public static bool IsBst(BinarySearchTreeNode<int> node)
        {
            if (!IsValidBstNode(node))
                return false;
            return IsBst(node.Left) && IsBst(node.Right);
        }

        public static bool IsValidBstNode(BinarySearchTreeNode<int> node)
        {
            if (node == null)
                return true;
            if (node.Left == null && node.Right == null)
                return true;
            if (node.Left != null && node.Left.Item > node.Item)
                return false;
            if (node.Right != null && node.Right.Item < node.Item)
                return false;

            return true;
        }
    }
}
