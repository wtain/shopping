
using System.Collections.Generic;

namespace ShoppingApp.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static IEnumerable<string> ToEnumerable(this string str)
        {
            yield return str;
        }
    }
}