namespace Ds.Helper
{
    internal static class Message
    {
        internal static class Stack
        {
            internal static string Empty => "Stack is empty.";
            internal static string NonNegativeCapacity => "Capacity must be non-negative.";
            internal static string NonNegativeArrayIndex => "Array index must be non-negative.";
            internal static string ArrayIndexNotGreaterThanArraySize => "Array index must not be greater than array size.";
            internal static string ArraySizeLess => "The difference of array length and arrayIndex must not be less than Count";
        }

        internal static class Enumerator
        {
            internal static string OperationNotStarted => "Operation not yet started.";
            internal static string OperationEnded => "Operation already ended.";
        }
    }
}
