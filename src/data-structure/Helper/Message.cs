namespace Ds.Helper
{
    public static class Message
    {
        public static class Stack
        {
            public const string Empty = "Stack is empty.";
            public const string NonNegativeCapacity = "Capacity must be non-negative.";
            public const string NonNegativeArrayIndex = "Array index must be non-negative.";
            public const string ArrayIndexNotGreaterThanArraySize = "Array index must not be greater than array size.";
            public const string ArraySizeLess = "The difference of array length and arrayIndex must not be less than Count";
        }

        public static class OnStack
        {
            public const string InvalidExpression = "Invalid expression.";
            public const string InvalidArithmeticOperator = "Either of the character is not a valid arithmetic operator.";
            public const string ArithmeticOperatorNotFound = "No arithmetic operator was found in the expression.";
            public const string ParanthesesInPostfixExpression = "Postfix expression cannot contain either parantheses.";
            public const string ParanthesesInPrefixExpression = "Prefix expression cannot contain either parantheses.";
            public const string NumberOfPopOpAreMoreThanElements = "Cannot perform this operation. Number of Pop operations are more than the elements in stack.";
        }

        public static class Enumerator
        {
            public const string OperationNotStarted = "Operation not yet started.";
            public const string OperationEnded = "Operation already ended.";
        }

        public static class Extensions
        {
            public const string ArrayCannotBeNull = "Array cannot be null.";
        }
    }
}
