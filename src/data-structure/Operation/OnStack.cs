using System;
using System.Text;

namespace Ds.Operation
{
    public class OnStack
    {
        #region Private Constants
        private const string NullOrEmptyExpression = "Null or empty expression.";
        private const string InvalidExpression = "Invalid expression.";
        #endregion

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

        #region Standard Problems based on Stack
        public string InfixToPostfix(string infixExp)
        {
            if (string.IsNullOrEmpty(infixExp))
                return NullOrEmptyExpression;

            var expBuilder = new StringBuilder();
            for (var i = 0; i < infixExp.Length; i++)
            {
                var c = infixExp[i];
                if (char.IsLetterOrDigit(c))
                {
                    expBuilder.Append(c);
                    continue;
                }
                if (c == '(')
                {
                    Stack.Push(c);
                    continue;
                }
                if (c == ')')
                {
                    while (!Stack.IsUnderflow && Stack.Peek() != '(')
                    {
                        expBuilder.Append(Stack.Pop());
                    }
                    if (!Stack.IsUnderflow && Stack.Peek() != '(')
                    {
                        return InvalidExpression;
                    }
                    Stack.Pop();
                    continue;
                }

                while (!Stack.IsUnderflow && OpPrecedence(c) <= OpPrecedence(Stack.Peek()))
                {
                    expBuilder.Append(Stack.Pop());
                }
                Stack.Push(c);
            }

            while (!Stack.IsUnderflow)
            {
                expBuilder.Append(Stack.Pop());
            }

            return expBuilder.ToString();
        }

        public string InfixToPrefix(string infixExp)
        {
            if (string.IsNullOrEmpty(infixExp))
                return NullOrEmptyExpression;

            var charArray = Reverse(infixExp);
            for (var i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] == ')')
                {
                    charArray[i] = '(';
                    continue;
                }
                if (charArray[i] == '(')
                    charArray[i] = ')';
            }

            var postfixExp = InfixToPostfix(new string(charArray));
            if (postfixExp == NullOrEmptyExpression)
                return NullOrEmptyExpression;
            if (postfixExp == InvalidExpression)
                return InvalidExpression;

            return new string(Reverse(postfixExp));
        }

        public string PrefixToPostfix(string prefixExp)
        {
            if (string.IsNullOrEmpty(prefixExp))
                return NullOrEmptyExpression;

            var reverseCharArray = Reverse(prefixExp);
            var stack = new Stack(reverseCharArray.Length);
            for (var i = 0; i < reverseCharArray.Length; i++)
            {
                var c = reverseCharArray[i];
                if (char.IsLetterOrDigit(c))
                {
                    stack.Push(c);
                    continue;
                }
                if (IsOperator(c))
                {
                    var str = $"{stack.Pop()}{stack.Pop()}{c}";
                    var j = 0;
                    while (stack.Count < str.Length)
                    {
                        stack.Push(str[j++]);
                    }
                }
            }
        }
        #endregion

        #region Private Methods
        private char[] Reverse(string str)
        {
            if (string.IsNullOrEmpty(str))
                return new char[0];

            var array = str.ToCharArray();
            Array.Reverse(array);

            return array;
        }
        private bool IsOperator(char op)
        {
            switch (op)
            {
                case '*':
                case '/':
                case '+':
                case '-':
                    return true;
                default:
                    return false;
            }
        }
        private int OpPrecedence(char c)
        {
            switch (c)
            {
                case '+':
                case '-':
                    return 1;
                case '*':
                case '/':
                    return 2;
                default:
                    return -1;
            }
        }
        #endregion
    }
}
