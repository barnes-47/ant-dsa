namespace Ds.Operation
{
    public class OnStack
    {
        public Stack Stack { get; }

        #region Public Ctors
        public OnStack(int capacity)
            : this(new Stack(capacity))
        {

        }
        public OnStack(string str)
            : this(new Stack(str))
        {

        }
        public OnStack(char[] array)
            : this(new Stack(array))
        {

        }
        public OnStack(Stack stack)
        {
            Stack = stack;
        }
        #endregion

        #region Other Operations on Stack
        /// <summary>
        /// Inserts the element at the bottom of stack.
        /// Uses recursion.
        /// </summary>
        /// <param name="data"></param>
        public void InsertAtBottom(char data)
        {
            if (Stack.IsUnderflow)
            {
                Stack.Push(data);
                return;
            }

            var top = Stack.Pop();
            InsertAtBottom(data);
            Stack.Push(top);
        }

        /// <summary>
        /// Reverses the stack. Uses InsertAtBottom internally.
        /// </summary>
        public void Reverse()
        {
            if (Stack.IsUnderflow)
                return;
            if (Stack.HasSingleElement)
                return;

            var top = Stack.Pop();
            Reverse();
            InsertAtBottom(top);
        }

        /// <summary>
        /// Deletes the middle element on the stack.
        /// </summary>
        public void DeleteMiddleElement()
        {
            if (Stack.IsUnderflow)
                return;

            var middleIndex = Stack.Count / 2;
            var size = Stack.Count - middleIndex;
            var array = new char[size];
            for (var i = 0; i < size; ++i)
            {
                array[i] = Stack.Pop();
            }
            Stack.Pop();            // Popping the middle element.

            for (var i = array.Length - 1; i >= 0; --i)
            {
                Stack.Push(array[i]);
            }
        }
        #endregion
    }
}
