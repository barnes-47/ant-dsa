using Ds.Helper;
using System.Collections.Generic;
using System.Text;
using static Ds.Helper.Constant;

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

        /// <summary>
        /// Converts Infix expression to Postfix expression.
        /// </summary>
        /// <param name="infixStr">The infix string.</param>
        /// <returns>The prefix string.</returns>
        public string InfixToPostfix(string infixStr)
        {
            if (infixStr.IsNullOrEmpty())
                return StringEmpty;

            var postfixArray = InternalInfixToPostfix(infixStr.ToCharArray());

            return new string(postfixArray);
        }

        /// <summary>
        /// Converts Infix expression to Prefix expression.
        /// </summary>
        /// <param name="infixStr">The infix string.</param>
        /// <returns>The prefix string.</returns>
        public string InfixToPrefix(string infixStr)
        {
            if (infixStr.IsNullOrEmpty())
                return StringEmpty;

            var array = infixStr.ToCharArray();
            for(var i = 0; i < array.Length; ++i)
            {
                if (array[i] == OpenParantheses)
                {
                    array[i] = ClosedParantheses;
                    continue;
                }
                if (array[i] == ClosedParantheses)
                    array[i] = OpenParantheses;
            }

            var prefixArray = InternalInfixToPostfix(array);
            prefixArray.Reverse();

            return new string(prefixArray);
        }

        #region Private Methods
        private char[] InternalInfixToPostfix(char[] infixArray)
        {
            var stack = new Generic.Stack<char>();
            var postfixArray = new char[infixArray.Length];
            var i = 0;
            for (; i < infixArray.Length; i++)
            {
                var c = infixArray[i];
                if (char.IsLetterOrDigit(c))
                {
                    postfixArray[i] = c;
                    continue;
                }
                if (c.IsOpenParantheses())
                {
                    stack.Push(c);
                    continue;
                }
                if (c.IsClosedParantheses())
                {
                    while (stack.IsNotEmpty && stack.Peek() != OpenParantheses)
                    {
                        postfixArray[i] = stack.Pop();
                    }
                    if (stack.IsNotEmpty && stack.Peek() != OpenParantheses)
                        Throw.InvalidOperationException(Message.OnStack.InvalidExpression);

                    stack.Pop();
                }

                while (stack.IsNotEmpty && c.Op1HasLowerOrEqualPrecedence(stack.Peek()))
                {
                    postfixArray[i] = stack.Pop();
                }
                stack.Push(c);
            }

            while (i < postfixArray.Length && stack.IsNotEmpty)
            {
                postfixArray[i++] = stack.Pop();
            }

            return postfixArray;
        }
        #endregion
    }
}
