namespace Ds.Helper
{
    public static class Constant
    {
        #region String Constants
        internal const string StringEmpty = "";
        #endregion

        #region Character Constants
        internal const char OpeningParantheses = '(';
        internal const char ClosingParantheses = ')';
        internal const char ArithmeticOperator_Addition = '+';
        internal const char ArithmeticOperator_Subtraction = '-';
        internal const char ArithmeticOperator_Modulus = '%';
        internal const char ArithmeticOperator_Multiplication = '*';
        internal const char ArithmeticOperator_Division = '/';
        #endregion

        #region Static Readonly Variables
        internal static readonly char[] ArithmeticOperators = new char[]
        {
            ArithmeticOperator_Addition, ArithmeticOperator_Division, ArithmeticOperator_Modulus, ArithmeticOperator_Multiplication, ArithmeticOperator_Subtraction
        };
        internal static readonly char[] LeftAssociativeOperators = new char[]
        {
            ArithmeticOperator_Addition, ArithmeticOperator_Division, ArithmeticOperator_Modulus, ArithmeticOperator_Multiplication, ArithmeticOperator_Subtraction
        };
        internal static readonly char[] RightAssociativeOperators = new char[]
        {

        };
        #endregion
    }
}
