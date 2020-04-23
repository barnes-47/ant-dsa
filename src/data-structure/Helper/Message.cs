namespace Ds.Helper
{
    internal static class Message
    {
        internal static class Stack
        {
            internal const string Empty = "Stack is empty.";
            internal const string NonNegativeCapacity = "Capacity must be non-negative.";
            internal const string NonNegativeArrayIndex = "Array index must be non-negative.";
            internal const string ArrayIndexNotGreaterThanArraySize = "Array index must not be greater than array size.";
            internal const string ArraySizeLess = "The difference of array length and arrayIndex must not be less than Count";
        }

        internal static class OnStack
        {
            internal const string InvalidExpression = "Invalid expression.";
        }

        internal static class Enumerator
        {
            internal const string OperationNotStarted = "Operation not yet started.";
            internal const string OperationEnded = "Operation already ended.";
        }

        internal static class Extensions
        {
            internal const string ArrayCannotBeNull = "Array cannot be null.";
        }
    }
}
