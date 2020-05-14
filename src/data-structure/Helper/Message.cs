namespace Ds.Helper
{
    public static class Message
    {
        public static class Common
        {
            public const string NonNegativeCapacity = "Capacity must be non-negative.";
            public const string NonNegativeArrayIndex = "Array index must be non-negative.";
            public const string ArrayIndexCannotGreaterThanArraySize = "Array index must not be greater than array size.";
            public const string ArraySizeLess = "Array size must be greater than or equal to the length of the list. Array size is difference between array length and array index.";
        }

        public static class QueueUsingArray
        {
            public const string Empty = "Queue is empty.";
        }

        public static class SinglyLinkedList
        {
            public const string Empty = "Singly list is empty.";
        }

        public static class Stack
        {
            public const string Empty = "Stack is empty.";
            public const string NonNegativeCapacity = "Capacity must be non-negative.";                        
        }

        public static class OnSinglyLinkedList
        {
            public const string NthMustBePositiveNonZero = "Nth value must be a positive non-zero integer.";
            public const string NthCannotBeGreaterThanListCount = "Nth cannot be greater than list count.";
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
