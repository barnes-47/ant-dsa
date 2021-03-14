/*
 * Red-Black Tree:
 * 
 * Some sources:
 * 1. https://cs.gmu.edu/~kosecka/cs583/lecture08.pdf
 * 2. http://staff.ustc.edu.cn/~csli/graduate/algorithms/book6/chap14.htm
 * 3. http://software.ucv.ro/~mburicea/lab8ASD.pdf
 * 
 * It is a self balancing BST with one extra bit of storage per node: its color, which can be either RED or BLACK.
 * A BST is a RB-Tree if it satisfies the following properties:
 * 1. The ROOT is always BLACK.
 * 2. Every node is either RED or BLACK.
 * 3. Every leaf (NULL) is BLACK.
 * 4. If a node is RED than both its children are BLACK.
 * 5. For each node, all paths from the node to descendant leaves contain the same number of BLACK nodes.
 * 
 * Each node of the tree contains the fields:
 * 1. Color.
 * 2. Key/Item/Value.
 * 3. Left.
 * 4. Right.
 * 5. Parent.
 * 
 * If a child or the parent of a node does not exist, the corresponding pointer field of the node contains the value NULL.
 * We shall regard these NULL’s as being pointers to external nodes (leaves) of the binary tree.
 * The number of BLACK nodes on any path from, but not including, a node 'x' to a leaf is called the black-height of the node,
 * denoted bh(x).
 * 
 * 
 * Both RB-Tree and AVL-Tree support insertion, deletion and look-up guaranteed O(logN) time.
 * However, there are following points of comparison between the two:
 * 1. AVL-Tree is more rigidly balanced and hence provide faster look-up as compared to RB-Tree.
 * 2. Insert intensive tasks uses RB-Tree over AVL-Tree.
 * 
 * Application of Red-Black Tree:
 * 1. SortedSet in C# language.
 * 2. Building blocks in other data structures which provides worst case guarantees; such as--
 *      a. Data structures used in computational geometry can be based on RB-Tree
 *      b. The Completely Fair Scheduler (CFS) used in current linux kernels.
 *      c. The epoll system call implementation.
 * 3. Several set operations such as Union, Intersection and Set difference have been defined on RB-Tree.
 */

namespace Ds.Generic.Tree
{
    using Ds.Helper;
    using System.Collections.Generic;

    //public static class RedBlackTreeExtension
    //{
    //    /// <summary>Determines whether parent of the specified node is a left child.</summary>
    //    /// <param name="node">The node.</param>
    //    /// <returns>
    //    ///   <c>true</c> if parent of the specified node is a left child; otherwise, <c>false</c>.
    //    /// </returns>
    //    private static bool IsParentLeftChild<T>(this RedBlackTreeNode<T> node)
    //        => node.Parent == node.Parent.Parent.Left;

    //    private static bool IsLeftChild<T>(this RedBlackTreeNode<T> node)
    //        => node == node.Parent.Left;

    //    private static bool IsRightChild<T>(this RedBlackTreeNode<T> node)
    //        => node == node.Parent.Right;

    //    private static RedBlackTreeNode<T> GrandParent<T>(this RedBlackTreeNode<T> node)
    //        => node.Parent.Parent;

    //    private static RedBlackTreeNode<T> GreatGrandParent<T>(this RedBlackTreeNode<T> node)
    //        => node.Parent.Parent.Parent;

    //    private static RedBlackTreeNode<T> Parent<T>(this RedBlackTreeNode<T> node)
    //        => node.Parent;
    //}

    public class RedBlackTreeNode<T>
    {
        #region Public Properties
        public bool IsRed { get; internal set; }
        public T Item { get; internal set; }
        public RedBlackTreeNode<T> Parent { get; internal set; }
        public RedBlackTreeNode<T> Left { get; internal set; }
        public RedBlackTreeNode<T> Right { get; internal set; }
        #endregion

        #region Ctors
        public RedBlackTreeNode(T item)
            : this(item, true, null)
        {

        }
        public RedBlackTreeNode(T item, bool isRed)
            : this(item, isRed, null)
        {

        }
        public RedBlackTreeNode(T item, bool isRed, RedBlackTreeNode<T> parent)
        {
            Item = item;
            IsRed = isRed;
            Parent = parent;
            Left = Right = null;
        }
        #endregion
    }

    public class RedBlackTree<T>
    {
        #region Private Properties
        private IComparer<T> _comparer;
        #endregion

        #region Public Properties
        public int Count { get; private set; }
        public bool IsEmpty => Count < 1 && Root == null;
        public T Max
        {
            get
            {
                if (IsEmpty)
                    return default;
                return GetMax(Root).Item;
            }
        }
        public T Min
        {
            get
            {
                if (IsEmpty)
                    return default;
                return GetMin(Root).Item;
            }
        }
        public RedBlackTreeNode<T> Root { get; private set; }
        #endregion

        #region Ctors
        public RedBlackTree()
        {
            _comparer = Comparer<T>.Default;
        }
        public RedBlackTree(IComparer<T> comparer)
        {
            if (comparer == null)
                _comparer = Comparer<T>.Default;
            else
                _comparer = comparer;
        }
        public RedBlackTree(IEnumerable<T> collection)
            : this(collection, Comparer<T>.Default)
        {

        }
        public RedBlackTree(IEnumerable<T> collection, IComparer<T> comparer)
            : this(comparer)
        {
            if (collection == null)
                Throw.ArgumentNullException(nameof(collection));

            using var en = collection.GetEnumerator();
            while (en.MoveNext())
            {
                Add(en.Current);
            }
        }
        #endregion

        #region Public Instance Methods        
        /// <summary>
        /// 1.  Adds root node if the tree is empty.
        /// 2.  Adds a child node based on the comparison to the nodes in the tree,
        ///     as one would add to any binary search tree.
        /// 3.  Then perform rotation based on the rules of RB-Tree.
        /// </summary>
        /// <param name="item">The item.</param>
        public void Add(T item)
        {
            if (IsEmpty)
            {
                Root = GetRoot(item, false);
                ++Count;

                return;
            }

            var x = Insert(item, Root);
            InsertFixUp(x);

            ++Count;
        }

        public bool Contains(T item)
            => Find(item) != null;

        public void Delete(T item)
        {
            var node = Find(item);
            if (node == null)
                return;

            var toBeDeleted = Delete(node);
            //if (!toBeDeleted.IsRed)
                //DeleteFixUp()
        }
        #endregion

        #region Private Instance Methods        
        private RedBlackTreeNode<T> Delete(RedBlackTreeNode<T> node)
        {
            RedBlackTreeNode<T> x = null, y = null;
            if (node.Left == null || node.Right == null)
                y = node;
            else
                y = InOrderSuccessor(node);

            if (y.Left != null)
                x = y.Left;
            else
                x = y.Right;

            x.Parent = y.Parent;
            if (y.Parent == null)
                Root = x;
            else if (IsLeftChild(y))
                y.Parent.Left = x;
            else
                y.Parent.Right = x;

            if (y != node)
            {
                node.Item = y.Item;
                node.IsRed = y.IsRed;
            }

            return y;
        }

        private void DeleteFixUp(RedBlackTreeNode<T> node)
        {

        }

        /// <summary>Finds the node holding the specified item.</summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private RedBlackTreeNode<T> Find(T item)
        {
            var current = Root;
            while (current != null)
            {
                if (_comparer.Compare(item, current.Item) < 0)
                {
                    current = current.Left;
                    continue;
                }
                if (_comparer.Compare(item, current.Item) > 0)
                {
                    current = current.Right;
                    continue;
                }

                return current;
            }

            return null;
        }

        /// <summary>Gets the node with maximum value. Or the right-most node.</summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private RedBlackTreeNode<T> GetMax(RedBlackTreeNode<T> node)
        {
            if (node == null)
                return null;

            while (node.Right != null)
            {
                node = node.Right;
            }

            return node;
        }

        /// <summary>Gets the node with minimum value. Or the left-most node.</summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private RedBlackTreeNode<T> GetMin(RedBlackTreeNode<T> node)
        {
            if (node == null)
                return null;

            while (node.Left != null)
            {
                node = node.Left;
            }

            return node;
        }

        /// <summary>Gets the In-Order successor of the specified node.</summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        private RedBlackTreeNode<T> InOrderSuccessor(RedBlackTreeNode<T> node)
        {
            if (node == null)
                return null;
            if (node.Right != null)
                return GetMin(node.Right);

            var parent = node.Parent;
            while (parent != null && IsRightChild(node))
            {
                node = parent;
                parent = node.Parent;
            }

            return parent;
        }

        /// <summary>Perfoms insertion as any ordinary the BST.</summary>
        /// <param name="item">The item.</param>
        /// <param name="node">The node.</param>
        /// <returns>The newly created node for the item<see cref="T"/>.</returns>
        private RedBlackTreeNode<T> Insert(T item, RedBlackTreeNode<T> node)
        {
            while (true)
            {
                if (_comparer.Compare(item, node.Item) < 0)
                {
                    if (node.Left == null)
                    {
                        node.Left = GetRedNode(item, node);
                        return node.Left;
                    }

                    node = node.Left;
                    continue;
                }
                if (node.Right == null)
                {
                    node.Right = GetRedNode(item, node);
                    return node.Right;
                }

                node = node.Right;
            }
        }

        /// <summary>Restructures the modified tree.</summary>
        /// <param name="node">The node.</param>
        private void InsertFixUp(RedBlackTreeNode<T> node)
        {
            node.IsRed = true;
            while (node != Root && node.Parent.IsRed)
            {
                if (IsParentLeftChild(node))
                {
                    var uncle = GetGrandParent(node).Right;
                    if (IsRed(uncle))
                    {
                        node.Parent.IsRed = false;
                        uncle.IsRed = false;
                        GetGrandParent(node).IsRed = true;
                        node = GetGrandParent(node);
                    }
                    else
                    {
                        if (IsRightChild(node))
                        {
                            node = node.Parent;
                            RotateLeft(node);
                        }

                        node.Parent.IsRed = false;
                        GetGrandParent(node).IsRed = true;
                        RotateRight(GetGrandParent(node));
                    }
                }
                else
                {
                    var uncle = GetGrandParent(node).Left;
                    if (IsRed(uncle))
                    {
                        node.Parent.IsRed = false;
                        uncle.IsRed = false;
                        GetGrandParent(node).IsRed = true;
                        node = GetGrandParent(node);
                    }
                    else
                    {
                        if (IsLeftChild(node))
                        {
                            node = node.Parent;
                            RotateRight(node);
                        }

                        node.Parent.IsRed = false;
                        GetGrandParent(node).IsRed = true;
                        RotateLeft(GetGrandParent(node));
                    }
                }
            }

            Root.IsRed = false;
        }

        /// <summary>Gets the child-node.</summary>
        /// <param name="item">The item.</param>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        private RedBlackTreeNode<T> GetRedNode(T item, RedBlackTreeNode<T> parent)
            => new RedBlackTreeNode<T>(item, true, parent);

        private RedBlackTreeNode<T> GetBlackNode(T item, RedBlackTreeNode<T> parent)
            => new RedBlackTreeNode<T>(item, false, parent);

        /// <summary>Gets the parent-node.</summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private RedBlackTreeNode<T> GetRoot(T item, bool isRed)
                    => new RedBlackTreeNode<T>(item, isRed);

        /// <summary>Performs left-rotation on the node. Assumes node[Right] != null.</summary>
        /// <param name="node">The node.</param>
        private void RotateLeft(RedBlackTreeNode<T> node)
        {
            var y = node.Right;
            node.Right = y.Left;
            if (y.Left != null)
                y.Left.Parent = node;

            y.Parent = node.Parent;
            if (node.Parent == null)
                Root = y;
            else if (IsLeftChild(node))
                node.Parent.Left = y;
            else
                node.Parent.Right = y;

            y.Left = node;
            node.Parent = y;
        }

        /// <summary>Performs right-rotation on the node. Assumes node[Left] != null.</summary>
        /// <param name="node">The node.</param>
        private void RotateRight(RedBlackTreeNode<T> node)
        {
            var y = node.Left;
            node.Left = y.Right;
            if (y.Right != null)
                y.Right.Parent = node;

            y.Parent = node.Parent;
            if (node.Parent == null)
                Root = y;
            else if (IsLeftChild(node))
                node.Parent.Left = y;
            else
                node.Parent.Right = y;

            y.Right = node;
            node.Parent = y;
        }
        #endregion

        #region Private Static Methods        
        /// <summary>Determines whether parent of the specified node is a left child.</summary>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if parent of the specified node is a left child; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsParentLeftChild(RedBlackTreeNode<T> node)
        {
            if (node == null)
                return false;
            if (node.Parent == null)
                return false;
            if (node.Parent.Parent == null)
                return false;
            if (node.Parent.Parent.Left == null)
                return false;

            return node.Parent == node.Parent.Parent.Left;
        }

        /// <summary>Determines whether the specified node is a left child.</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if the specified node is left child; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsLeftChild(RedBlackTreeNode<T> node)
        {
            if (node == null)
                return false;
            if (node.Parent == null)
                return false;
            if (node.Parent.Left == null)
                return false;

            return node == node.Parent.Left;
        }

        /// <summary>Determines whether the specified node is a right child.</summary>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if the specified node is a right child; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsRightChild(RedBlackTreeNode<T> node)
        {
            if (node == null)
                return false;
            if (node.Parent == null)
                return false;
            if (node.Parent.Right == null)
                return false;

            return node == node.Parent.Right;
        }

        /// <summary>Determines whether the specified node is red.</summary>
        /// <param name="node">The node.</param>
        /// <returns>
        ///   <c>true</c> if the specified node is red; otherwise, <c>false</c>.
        /// </returns>
        internal static bool IsRed(RedBlackTreeNode<T> node)
            => node != null && node.IsRed;

        /// <summary>Gets the grand parent of the specified node.</summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        internal static RedBlackTreeNode<T> GetGrandParent(RedBlackTreeNode<T> node)
            => node?.Parent?.Parent;

        /// <summary>Gets the great grand parent of the specified node.</summary>
        /// <param name="node">The node.</param>
        /// <returns></returns>
        internal static RedBlackTreeNode<T> GetGreatGrandParent(RedBlackTreeNode<T> node)
            => node?.Parent?.Parent?.Parent;
        #endregion
    }
}
