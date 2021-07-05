using System;
using Xunit;

namespace MyTools.Extensions
{
    public class SubstrExtensionTests
    {
        [Theory,
         InlineData("abc", "c", "ab"), InlineData("abc", "d", ""), InlineData("abc", null, "abc"),
         InlineData("abc", "", "abc"), InlineData(null, "abc", ""), InlineData("hello", "ll", "he"),
         InlineData("AbC", "c", "Ab", StringComparison.OrdinalIgnoreCase)]
        public void BeforeShouldBeRight(string text, string value, string result,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var before = text.Before(value, comparisonType);
            Assert.Equal(result, before);
        }

        [Theory,
         InlineData("abc", new[] {"c", "b"}, "a"), InlineData("abc", null, "abc"),
         InlineData("abc", new[] {"a"}, ""), InlineData("hello world", new[] {"ld", "ll"}, "he")]
        public void BeforeArrayShouldBeRight(string text, string[] values, string result)
        {
            var before = text.Before(values);
            Assert.Equal(result, before);
        }

        [Theory,
         InlineData("abc", "a", "bc"), InlineData("abc", "d", ""), InlineData("abc", null, "abc"),
         InlineData("abc", "", "abc"), InlineData(null, "abc", ""), InlineData("hello", "ll", "o"),
         InlineData("AbC", "a", "bC", StringComparison.OrdinalIgnoreCase)]
        public void AfterShouldBeRight(string text, string value, string result,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var after = text.After(value, comparisonType);
            Assert.Equal(result, after);
        }

        [Theory,
         InlineData("abc", new[] {"a", "b"}, "c"), InlineData("abc", null, "abc"),
         InlineData("abc", new[] {"c"}, ""), InlineData("hello world", new[] {"ll", "or"}, "ld")]
        public void AfterArrayShouldBeRight(string text, string[] values, string result)
        {
            var before = text.After(values);
            Assert.Equal(result, before);
        }

        [Theory, InlineData("abc", "a", "c", "b"), InlineData("abc", "d", "e", ""), InlineData("abc", "", "", "abc"),
         InlineData(null, "abc", "def", ""), InlineData("hello world", "ll", "rl", "o wo"),
         InlineData("AbCeF", "a", "Ef", "bC", StringComparison.OrdinalIgnoreCase)]
        public void BetweenShouldBeRight(string text, string a, string b, string result,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var after = text.Between(a, b, comparisonType);
            Assert.Equal(result, after);
        }
    }
}