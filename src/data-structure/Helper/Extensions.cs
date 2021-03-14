namespace Ds.Helper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Ds.Helper.Constant;

    public static class Extensions
    {

        #region Char Extension Methods
        public static bool ContainsClosingParantheses(this char[] array)
        {
            if (array == null)
                Throw.ArgumentNullException(nameof(array));
            if (array.Length <= 0)
                return false;

            return Array.IndexOf(array, ClosingParantheses) > -1;
        }
        public static bool ContainsOpeningAndClosingParantheses(this char[] array)
            => ContainsOpeningParantheses(array) && ContainsClosingParantheses(array);
        public static bool ContainsOpeningParantheses(this char[] array)
        {
            if (array == null)
                Throw.ArgumentNullException(nameof(array));
            if (array.Length <= 0)
                return false;

            return Array.IndexOf(array, OpeningParantheses) > -1;
        }
        /// <summary>
        /// Returns true when operator1 and operator2 has equal precedence, false otherwise.
        /// </summary>
        /// <param name="op1">Must be an arithmetic operator.</param>
        /// <param name="op2">Must be an arithmetic operator.</param>
        /// <returns></returns>
        public static bool HasEqualPrecedenceTo(this char op1, char op2)
        {
            if (!IsArithmeticOperator(op1) || !IsArithmeticOperator(op1))
                Throw.ArgumentException(Message.OnStack.InvalidArithmeticOperator);

            return OperatorPrecedence(op1) == OperatorPrecedence(op2);
        }
        /// <summary>
        /// Returns true when operator1 has higher precedence than operator2, false otherwise.
        /// </summary>
        /// <param name="op1">Must be an arithmetic operator.</param>
        /// <param name="op2">Must be an arithmetic operator.</param>
        /// <returns></returns>
        public static bool HasHigherPrecedenceThan(this char op1, char op2)
        {
            if (!IsArithmeticOperator(op1) || !IsArithmeticOperator(op1))
                Throw.ArgumentException(Message.OnStack.InvalidArithmeticOperator);

            return OperatorPrecedence(op1) > OperatorPrecedence(op2);
        }
        /// <summary>
        /// Returns true when operator1 has lower precedence than operator2, false otherwise.
        /// </summary>
        /// <param name="op1">Must be an arithmetic operator.</param>
        /// <param name="op2">Must be an arithmetic operator.</param>
        /// <returns></returns>
        public static bool HasLowerPrecedenceThan(this char op1, char op2)
        {
            if (!IsArithmeticOperator(op1) || !IsArithmeticOperator(op1))
                Throw.ArgumentException(Message.OnStack.InvalidArithmeticOperator);

            return OperatorPrecedence(op1) < OperatorPrecedence(op2);
        }
        public static bool IsAdditionOperator(this char c)
            => c == ArithmeticOperator_Addition;
        public static bool IsArithmeticOperator(this char c)
            => Array.IndexOf(ArithmeticOperators, c) > -1;
        public static bool IsClosingParantheses(this char c)
            => c == ClosingParantheses;
        public static bool IsDivisionOperator(this char c)
            => c == ArithmeticOperator_Division;
        /// <summary>
        /// Returns true if the operator is left-associative, false otherwise.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static bool IsLeftAssociative(this char op)
            => Array.IndexOf(LeftAssociativeOperators, op) > -1;
        public static bool IsLetterOrDigit(this char c)
            => char.IsLetterOrDigit(c);
        public static bool IsMultiplicationOperator(this char c)
            => c == ArithmeticOperator_Multiplication;
        public static bool IsOpeningParantheses(this char c)
            => c == OpeningParantheses;
        /// <summary>
        /// Returns true if the operator is right-associative, false otherwise.
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        public static bool IsRightAssociative(this char op)
            => Array.IndexOf(RightAssociativeOperators, op) > -1;
        public static bool IsSubtractionOperator(this char c)
            => c == ArithmeticOperator_Subtraction;
        public static short OperatorPrecedence(this char op)
        {
            switch (op)
            {
                case ArithmeticOperator_Addition:
                case ArithmeticOperator_Subtraction:
                    return 1;
                case '%':
                case ArithmeticOperator_Multiplication:
                case ArithmeticOperator_Division:
                    return 2;
                default:
                    return -1;
            }
        }
        public static void Reverse(this char[] array)
        {
            if (array == null)
                Throw.ArgumentNullException(nameof(array));
            if (array.Length < 2)
                return;
            Array.Reverse(array);
        }
        #endregion

        #region String Extension Methods
        public static bool ContainsAnyArithmeticOperator(this string str)
        {
            if (str.IsNullOrEmpty())
                Throw.ArgumentNullException(nameof(str));

            return str.IndexOfAny(ArithmeticOperators) > -1;
        }
        public static bool ContainsClosingParantheses(this string str)
        {
            if (str.IsNullOrEmpty())
                Throw.ArgumentNullException(nameof(str));
            if (str.IsEmpty())
                return false;

            return str.IndexOf(ClosingParantheses) > -1;
        }
        public static bool ContainsOpeningOrClosingParantheses(this string str)
        {
            if (str.IsNullOrEmpty())
                Throw.ArgumentNullException(nameof(str));
            if (str.IsEmpty())
                return false;

            return str.IndexOf(OpeningParantheses) > -1 || str.IndexOf(ClosingParantheses) > -1;
        }
        public static bool ContainsOpeningParantheses(this string str)
        {
            if (str.IsNullOrEmpty())
                Throw.ArgumentNullException(nameof(str));
            if (str.IsEmpty())
                return false;

            return str.IndexOf(OpeningParantheses) > -1;
        }
        public static IEnumerable<int> ConvertToInts(this string str)
        {
            if (str.IsNullOrEmpty())
                Throw.ArgumentNullException(nameof(str));

            IEnumerable<int> ints = null;
            try
            {
                ints = str.Split(',').ConvertToInts();
            }
            catch (Exception ex)
            {
                Throw.InvalidOperationException(ex.Message, ex);
            }

            return ints;
        }
        public static IEnumerable<int> ConvertToInts(this IEnumerable<string> strings)
        {
            if (strings == null)
                Throw.ArgumentNullException(nameof(strings));
            if (!strings.Any())
                return null;

            IEnumerable<int> ints = null;
            try
            {
                ints = Array.ConvertAll(strings.ToArray(), int.Parse);
            }
            catch(Exception ex)
            {
                Throw.InvalidOperationException(ex.Message, ex);
            }

            return ints;
        }
        public static bool IsEmpty(this string str)
            => str.Length <= 0;
        public static bool IsNull(this string str)
            => (str == null) || (str is null);
        public static bool IsNullOrEmpty(this string str)
            => string.IsNullOrEmpty(str);
        public static string Reverse(this string str)
        {
            if (str.IsNull())
                Throw.ArgumentNullException(nameof(str));
            if (str.IsEmpty())
                return str;

            var array = str.ToCharArray();
            if (array.Length < 2)
                return str;

            Array.Reverse(array);

            return new string(array);
        }
        #endregion
    }
}
