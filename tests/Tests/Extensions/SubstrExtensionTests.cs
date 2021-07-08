using System;
using System.Text.RegularExpressions;
using Xunit;

namespace MyTools.Extensions
{
    public class SubstrExtensionTests
    {
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
         InlineData("abc", "c", "ab"), InlineData("abc", "d", ""), InlineData("abc", null, "abc"),
         InlineData("abc", "", "abc"), InlineData(null, "abc", ""), InlineData("hello", "ll", "he"),
         InlineData("AbC", "c", "Ab", StringComparison.OrdinalIgnoreCase)]
        public void BeforeShouldBeRight(string text, string value, string result,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var before = text.Before(value, comparisonType);
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

        [Theory,
         InlineData("abc", new[] {"a", "b"}, "c"), InlineData("abc", null, "abc"),
         InlineData("abc", new[] {"c"}, ""), InlineData("hello world", new[] {"ll", "or"}, "ld")]
        public void AfterArrayShouldBeRight(string text, string[] values, string result)
        {
            var before = text.After(values);
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
         InlineData("abc123xyz", @"\d+", "xyz"), InlineData("123456", @"\w+", "")]
        public void AfterRegexShouldBeRight(string text, string exp, string result)
        {
            var after = text.After(new Regex(exp));
            Assert.Equal(result, after);
        }

        [Theory,
         InlineData("abc123xyz", @"\d+", "abc"), InlineData("123456", @"\w+", "")]
        public void BeforeRegexShouldBeRight(string text, string exp, string result)
        {
            var before = text.Before(new Regex(exp));
            Assert.Equal(result, before);
        }

        [Theory,
         InlineData("123abc123", @"\d+", @"\d+", "abc"), InlineData("123abc123", @"\d+", @"\s+", "")]
        public void BetweenRegexShouldBeRight(string text, string exp1, string exp2, string result)
        {
            var after = text.Between(new Regex(exp1), new Regex(exp2));
            Assert.Equal(result, after);
        }

        [Theory,
         InlineData("abc", "a", "abc"), InlineData("abc", "d", ""), InlineData("abc", null, "abc"),
         InlineData("abc", "", "abc"), InlineData(null, "abc", ""), InlineData("hello", "ll", "llo"),
         InlineData("AbC", "a", "AbC", StringComparison.OrdinalIgnoreCase)]
        public void AfterWithShouldBeRight(string text, string value, string result,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var after = text.AfterWith(value, comparisonType);
            Assert.Equal(result, after);
        }


        [Theory,
         InlineData("abc", "c", "abc"), InlineData("abc", "d", ""), InlineData("abc", null, "abc"),
         InlineData("abc", "", "abc"), InlineData(null, "abc", ""), InlineData("hello", "ll", "hell"),
         InlineData("AbC", "c", "AbC", StringComparison.OrdinalIgnoreCase)]
        public void BeforeWithShouldBeRight(string text, string value, string result,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var before = text.BeforeWith(value, comparisonType);
            Assert.Equal(result, before);
        }

        [Theory,
         InlineData("a1b2", "1", "b", "1b"), InlineData("abc", "d", "e", ""), InlineData("abc", "", "", "abc"),
         InlineData(null, "abc", "def", ""), InlineData("hello world", "ll", "or", "llo wor"),
         InlineData("AbCeF", "B", "e", "bCe", StringComparison.OrdinalIgnoreCase)]
        public void BetweenWithShouldBeRight(string text, string a, string b, string result,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            var after = text.BetweenWith(a, b, comparisonType);
            Assert.Equal(result, after);
        }

        [Theory,
         InlineData("abc", new[] {"a", "b"}, "bc"), InlineData("abc", null, "abc"),
         InlineData("abc", new[] {"c"}, "c"), InlineData("hello world", new[] {"ll", "rl"}, "rld")]
        public void AfterArrayArrayShouldBeRight(string text, string[] values, string result)
        {
            var before = text.AfterWith(values);
            Assert.Equal(result, before);
        }

        [Theory,
         InlineData("abc", new[] {"c", "b"}, "ab"), InlineData("abc", null, "abc"),
         InlineData("abc", new[] {"a"}, "a"), InlineData("hello world", new[] {"ld", "ll"}, "hell")]
        public void BeforeWithArrayShouldBeRight(string text, string[] values, string result)
        {
            var before = text.BeforeWith(values);
            Assert.Equal(result, before);
        }

        [Theory,
         InlineData("abc123xyz", @"\d+", "123xyz"), InlineData("123456", @"\s+", "")]
        public void AfterWithRegexShouldBeRight(string text, string exp, string result)
        {
            var after = text.AfterWith(new Regex(exp));
            Assert.Equal(result, after);
        }

        [Theory,
         InlineData("abc123xyz", @"\d+", "abc123"), InlineData("123456", @"\s+", "")]
        public void BeforeWithRegexShouldBeRight(string text, string exp, string result)
        {
            var before = text.BeforeWith(new Regex(exp));
            Assert.Equal(result, before);
        }

        [Theory,
         InlineData("abc123xyz", @"\d+", @"\d+", "123"), InlineData("123abc123", @"\d+", @"\s+", "")]
        public void BetweenWithRegexShouldBeRight(string text, string exp1, string exp2, string result)
        {
            var after = text.BetweenWith(new Regex(exp1), new Regex(exp2));
            Assert.Equal(result, after);
        }
    }
}