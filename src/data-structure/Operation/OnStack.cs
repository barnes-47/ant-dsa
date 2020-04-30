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
        /// Converts an infix-expression to postfix-expression.
        /// 
        /// Note:- Current character while looping over the input-expression will be described as 'c'.
        /// 1. Loop over the input-expression
        ///     1. If c is an opening parantheses.
        ///         1. Push c to stack.
        ///         2. continue;
        ///     2. If c is a letter or digit.
        ///         1. Append it to expression-builder.
        ///         2. continue;
        ///     3. If c is an arithmetic-operator
        ///         1. Loop over stack untill it is not-empty and c has an equal or lower precedence than the one in the stack
        ///             1. If c has equal precedence and is right associative
        ///                 1. Push the c to stack.
        ///                 2. continue;
        ///             2. If c has equal precedence and is left associative or c has lower precedence than the one in the stack
        ///                 1. Pop the stack and append its output to expression-builder.
        ///         2. Push c on stack.
        ///         3. continue;
        ///     4. If c is a closing parantheses
        ///         1. Loop over stack untill it is not empty and c is not equal to an opening parantheses
        ///             1. Pop the stack and append the result to expression-builder.
        ///         2. If stack is not empty and stack top is not an opening-parantheses.
        ///             1. Throw an exception.
        ///         3. Pop the stack.
        /// 
        /// 2. Loop over the entire stack untill it is empty
        ///     1. Pop the stack and append the result to the expression-builder.
        /// </summary>
        /// <param name="inputExp">The infix expression.</param>
        /// <returns></returns>
        public string InfixToPostfix(string inputExp)
        {
            InternalValidateInfixExpresion(inputExp);

            var operators = new Generic.Stack<char>();
            var expBuilder = new StringBuilder();
            foreach (var c in inputExp)
            {
                if (c.IsOpeningParantheses())
                {
                    operators.Push(c);
                    continue;
                }
                if (c.IsLetterOrDigit())
                {
                    expBuilder.Append(c);
                    continue;
                }
                if (c.IsArithmeticOperator())
                {
                    while(operators.IsNotEmpty && (c.HasEqualPrecedenceTo(operators.Peek()) || c.HasLowerPrecedenceThan(operators.Peek())))
                    {
                        if (c.HasEqualPrecedenceTo(operators.Peek()) && c.IsRightAssociative())
                            break;
                        if ((c.HasEqualPrecedenceTo(operators.Peek()) && c.IsLeftAssociative()) || c.HasLowerPrecedenceThan(operators.Peek()))
                            expBuilder.Append(operators.Pop());                        
                    }

                    operators.Push(c);
                    continue;
                }
                if (c.IsClosingParantheses())
                {
                    while (operators.IsNotEmpty && !operators.Peek().IsOpeningParantheses())
                    {
                        expBuilder.Append(operators.Pop());
                    }
                    if (operators.IsNotEmpty && !operators.Peek().IsOpeningParantheses())
                    {
                        Throw.InvalidOperationException(Message.OnStack.InvalidExpression);
                    }

                    operators.Pop();
                }
            }

            while (operators.IsNotEmpty)
            {
                expBuilder.Append(operators.Pop());
            }
            operators.Clear();

            return expBuilder.ToString(0, expBuilder.Length);
        }

        /// <summary>
        /// Converts an infix-expression to prefix-expression.
        /// </summary>
        /// <param name="inputExp"></param>
        /// <returns></returns>
        public string InfixToPrefix(string inputExp)
        {
            InternalValidateInfixExpresion(inputExp);
            inputExp = inputExp.Reverse();            

            var operators = new Generic.Stack<char>();
            var reverseExpBuilder = new StringBuilder();
            foreach (var c in inputExp)
            {
                if (c.IsClosingParantheses())
                {
                    operators.Push(c);
                    continue;
                }
                if (c.IsLetterOrDigit())
                {
                    reverseExpBuilder.Append(c);
                    continue;
                }
                if (c.IsArithmeticOperator())
                {
                    while(operators.IsNotEmpty && (c.HasEqualPrecedenceTo(operators.Peek()) || c.HasLowerPrecedenceThan(operators.Peek())))
                    {
                        if (c.HasEqualPrecedenceTo(operators.Peek()) && c.IsLeftAssociative())
                            break;
                        if ((c.HasEqualPrecedenceTo(operators.Peek()) && c.IsRightAssociative()) || c.HasLowerPrecedenceThan(operators.Peek()))
                            reverseExpBuilder.Append(operators.Pop());
                    }

                    operators.Push(c);
                    continue;
                }
                if (c.IsOpeningParantheses())
                {
                    while(operators.IsNotEmpty && !operators.Peek().IsClosingParantheses())
                    {
                        reverseExpBuilder.Append(operators.Pop());
                    }
                    if (operators.IsNotEmpty && !operators.Peek().IsClosingParantheses())
                        Throw.InvalidOperationException(Message.OnStack.InvalidExpression);

                    operators.Pop();    // remove the closing-parantheses.
                }
            }

            while (operators.IsNotEmpty)
            {
                reverseExpBuilder.Append(operators.Pop());
            }
            operators.Clear();

            var expression = reverseExpBuilder.ToString(0, reverseExpBuilder.Length).Reverse();

            return expression;
        }

        public string PostfixToPrefix(string postfixExp)
        {
            InternalValidateInfixExpresion(postfixExp);

            var operands = new Generic.Stack<string>();
            var array = postfixExp.ToCharArray();
            for(var i = 0; i < array.Length; ++i)
            {
                var c = array[i];
                if (c.IsLetterOrDigit())
                {
                    operands.Push(c.ToString());
                    continue;
                }
                if (operands.Count < 2)
                    Throw.InvalidOperationException(Message.OnStack.NumberOfPopOpAreMoreThanElements);

                var operand_1 = operands.Pop();
                operands.Push($"{c}{operands.Pop()}{operand_1}");
            }

            var prefix = operands.Pop();
            operands.Clear();

            return prefix;
        }

        #region Private Methods
        private void InternalValidateInfixExpresion(string inputExp)
        {
            if (inputExp.IsNullOrEmpty())
                Throw.ArgumentNullException(nameof(inputExp));
            if (!inputExp.ContainsAnyArithmeticOperator())
                Throw.ArgumentException(Message.OnStack.ArithmeticOperatorNotFound);
        }
        #endregion
    }
}
