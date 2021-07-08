using System;
using System.Text.RegularExpressions;
using MyTools.Utils;

namespace MyTools.Extensions
{
    public static class SubstrExtension
    {
        /// <summary>
        /// 获取 value 之后的内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static Substr After(this string text, string value,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new Substr(text).After(value, comparisonType);
        }
        
        /// <summary>
        /// 获取 value 之前的内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static Substr Before(this string text, string value,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new Substr(text).Before(value, comparisonType);
        }

        /// <summary>
        /// 获取 a 与 b 之间的内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static Substr Between(this string text, string a, string b,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new Substr(text).Between(a, b, comparisonType);
        }

        /// <summary>
        /// 获取 value 之后的内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Substr After(this string text, params string[] values)
        {
            return new Substr(text).After(values);
        }

        /// <summary>
        /// 获取 value 之前的内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Substr Before(this string text, params string[] values)
        {
            return new Substr(text).Before(values);
        }

        /// <summary>
        /// 获取正则 exp 匹配之后的文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Substr After(this string text, Regex exp)
        {
            return new Substr(text).After(exp);
        }

        /// <summary>
        /// 获取正则 exp 匹配之前的文本
        /// </summary>
        /// <param name="text"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Substr Before(this string text, Regex exp)
        {
            return new Substr(text).Before(exp);
        }

        /// <summary>
        /// 获取正则 exp1 与 exp2 之间内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public static Substr Between(this string text, Regex exp1, Regex exp2)
        {
            return new Substr(text).Between(exp1, exp2);
        }
        
        /// <summary>
        /// 获取 value 之后的内容，包含匹配内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static Substr AfterWith(this string text, string value,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new Substr(text).AfterWith(value, comparisonType);
        }
        
        /// <summary>
        /// 获取 value 之前的内容，包含匹配内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static Substr BeforeWith(this string text, string value,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new Substr(text).BeforeWith(value, comparisonType);
        }

        /// <summary>
        /// 获取 a 与 b 之间的内容，包含匹配内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public static Substr BetweenWith(this string text, string a, string b,
            StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            return new Substr(text).BetweenWith(a, b, comparisonType);
        }

        /// <summary>
        /// 获取 value 之后的内容，包含匹配内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Substr AfterWith(this string text, params string[] values)
        {
            return new Substr(text).AfterWith(values);
        }

        /// <summary>
        /// 获取 value 之前的内容，包含匹配内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static Substr BeforeWith(this string text, params string[] values)
        {
            return new Substr(text).BeforeWith(values);
        }

        /// <summary>
        /// 获取正则 exp 匹配之后的文本，包含匹配内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Substr AfterWith(this string text, Regex exp)
        {
            return new Substr(text).AfterWith(exp);
        }

        /// <summary>
        /// 获取正则 exp 匹配之前的文本，包含匹配内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static Substr BeforeWith(this string text, Regex exp)
        {
            return new Substr(text).BeforeWith(exp);
        }

        /// <summary>
        /// 获取正则 exp1 与 exp2 之间内容，包含匹配内容
        /// </summary>
        /// <param name="text"></param>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public static Substr BetweenWith(this string text, Regex exp1, Regex exp2)
        {
            return new Substr(text).BetweenWith(exp1, exp2);
        }
    }
}