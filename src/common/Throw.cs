namespace Common
{
    using System;

    public static class Throw
    {
        #region Argument Exception

        #endregion Argument Exception

        #region Argument Null Exception
        public static void ArgumentNullException(string paramName)
            => throw new ArgumentNullException(paramName);

        public static void ArgumentNullException(string paramName, string message)
            => throw new ArgumentNullException(paramName, message);

        public static void ArgumentNullException(string message, Exception innerException)
            => throw new ArgumentNullException(message, innerException);
        #endregion Argument Null Exception

        #region Argument Out Of Range Exception

        #endregion Argument Out Of Range Exception
    }
}
