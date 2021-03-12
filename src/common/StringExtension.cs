namespace Common
{
    public static class StringExtension
    {
        public static bool IsNull(this string str)
            => str == null || str is null;

        public static bool IsNullOrEmpty(this string str)
            => string.IsNullOrEmpty(str);
    }
}
