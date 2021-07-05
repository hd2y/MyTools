using System;
using MyTools.Utils;

namespace MyTools.Extensions
{
    public static class SubstrExtension
    {
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
        /// <param name="values"></param>
        /// <returns></returns>
        public static Substr Before(this string text, params string[] values)
        {
            return new Substr(text).Before(values);
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
    }
}