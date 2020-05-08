using System;

namespace Ds.Helper
{
    internal static class Throw
    {
        internal static void InvalidOperationException(string message)
            => throw new InvalidOperationException(message);

        internal static void InvalidOperationException(string message, Exception innerEx)
            => throw new InvalidOperationException(message, innerEx);

        internal static void ArgumentException(string message)
            => throw new ArgumentException(message);

        internal static void ArgumentNullException(string paramName)
            => throw new ArgumentNullException(paramName);

        internal static void ArgumentOutOfRangeException(string message, Exception innerException)
            => throw new ArgumentOutOfRangeException(message, innerException);

        internal static void ArgumentOutOfRangeException(string paramName, string message)
            => throw new ArgumentOutOfRangeException(paramName, message);

        internal static void ArgumentOutOfRangeException(string paramName, object actualValue, string message)
            => throw new ArgumentOutOfRangeException(paramName, actualValue, message);
    }
}
