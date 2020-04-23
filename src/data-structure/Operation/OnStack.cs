using System.Collections.Generic;

namespace Ds.Operation
{
    public class OnStack<T>
    {
        #region Public Properties
        public Generic.Stack<T> Stack { get; private set; }
        #endregion

        #region Public Ctors
        public OnStack()
        {
            Stack = new Generic.Stack<T>();
        }

        public OnStack(int capacity)
        {
            Stack = new Generic.Stack<T>(capacity);
        }

        public OnStack(IEnumerable<T> collection)
        {
            Stack = new Generic.Stack<T>(collection);
        }
        #endregion

        /// <summary>
        /// Inserts the item at the bottom of the stack using recursion.
        /// </summary>
        public void InsertAtBottomUsingRecursion(T item)
        {
            if (Stack.IsEmpty)
            {
                Stack.Push(item);
                return;
            }

            var top = Stack.Pop();
            InsertAtBottomUsingRecursion(item);
            Stack.Push(top);
        }

        /// <summary>
        /// Reverses the elements of the stack using recursion.
        /// </summary>
        public void ReverseUsingRecursion()
        {
            if (Stack.IsEmpty)
                return;

            var top = Stack.Pop();
            ReverseUsingRecursion();
            InsertAtBottomUsingRecursion(top);
        }

        /// <summary>
        /// Reverses a Stack using extra stack.
        /// </summary>
        public void ReverseUsingExtraSpace()
        {
            var stack = new Generic.Stack<T>(Stack.Count);
            foreach (var item in Stack)
            {
                stack.Push(item);
            }

            Stack = stack;
        }
    }
}
