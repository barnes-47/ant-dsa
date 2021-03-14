namespace Ds.Helper
{
    using System;

    internal static class Throw
    {
        #region Exception: Invalid Operation
        internal static void InvalidOperationException(string message)
            => throw new InvalidOperationException(message);

        internal static void InvalidOperationException(string message, Exception innerEx)
            => throw new InvalidOperationException(message, innerEx);
        #endregion Exception: Invalid Operation

        internal static void ArgumentException(string message)
            => throw new ArgumentException(message);

        #region Exception: Argument Null
        internal static void ArgumentNullException(string paramName)
            => throw new ArgumentNullException(paramName);
        #endregion Exception: Argument Null

        #region Exception: Argument Out Of Range
        internal static void ArgumentOutOfRangeException(string message, Exception innerException)
            => throw new ArgumentOutOfRangeException(message, innerException);

        internal static void ArgumentOutOfRangeException(string paramName)
            => throw new ArgumentOutOfRangeException(paramName);

        internal static void ArgumentOutOfRangeException(string paramName, string message)
            => throw new ArgumentOutOfRangeException(paramName, message);

        internal static void ArgumentOutOfRangeException(string paramName, object actualValue, string message)
            => throw new ArgumentOutOfRangeException(paramName, actualValue, message);
        #endregion Exception: Argument Out Of Range
    }
}
