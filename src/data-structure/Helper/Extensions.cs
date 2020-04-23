using System;

namespace Ds.Helper
{
    public static class Extensions
    {
        #region Char Extension Methods
        public static bool IsOpenParantheses(this char c) => c == '(';
        public static bool IsClosedParantheses(this char c) => c == ')';
        public static bool Op1HasLowerOrEqualPrecedence(this char op1, char op2) => op1.OperatorPrecedence() <= op2.OperatorPrecedence();
        public static short OperatorPrecedence(this char op)
        {
            switch (op)
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
        public static bool IsNull(this string str) => (str == null) || (str is null);
        public static bool IsEmpty(this string str) => str == string.Empty || str == "";
        public static bool IsNullOrEmpty(this string str) => string.IsNullOrEmpty(str);
        public static string Reverse(this string str)
        {
            if (str.IsNull())
                Throw.ArgumentNullException(nameof(str));
            if (str.IsEmpty())
                return string.Empty;

            var array = str.ToCharArray();
            array.Reverse();

            return new string(array);
        }
        #endregion
    }
}
