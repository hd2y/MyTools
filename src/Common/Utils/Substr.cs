using System;
using System.Text.RegularExpressions;

namespace MyTools.Utils
{
    /// <summary>
    /// 剪切处理字符串
    /// </summary>
    public struct Substr
    {
        /// <summary>
        /// 用于处理文本信息
        /// </summary>
        /// <param name="text"></param>
        public Substr(string text)
        {
            Text = text ?? string.Empty;
            Index = 0;
            Length = Text.Length;
        }

        private string Text { get; set; }

        private int Index { get; set; }

        private int Length { get; set; }

        /// <summary>
        /// 获取 value 之后的文本
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public Substr After(string value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (Length == 0 || string.IsNullOrEmpty(value)) return this;
            var index = Text.IndexOf(value, Index, comparisonType);
            return AfterOrBefore(false, false, index, value.Length);
        }

        /// <summary>
        /// 获取 value 之前的文本
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public Substr Before(string value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (Length == 0 || string.IsNullOrEmpty(value)) return this;
            var index = Text.IndexOf(value, Index, comparisonType);
            return AfterOrBefore(true, false, index, value.Length);
        }

        /// <summary>
        /// 获取 a 与 b 之间的文本
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public Substr Between(string a, string b, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            After(a, comparisonType);
            Before(b, comparisonType);
            return this;
        }

        /// <summary>
        /// 获取 values 之后的文本
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Substr After(params string[] values)
        {
            if (values == null || values.Length == 0) return this;
            foreach (var value in values)
            {
                After(value);
            }

            return this;
        }

        /// <summary>
        /// 获取 values 之前的文本
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Substr Before(params string[] values)
        {
            if (values == null || values.Length == 0) return this;
            foreach (var value in values)
            {
                Before(value);
            }

            return this;
        }

        /// <summary>
        /// 获取正则 exp 匹配之后的文本
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Substr After(Regex exp)
        {
            if (exp == null || Length == 0) return this;
            var match = exp.Match(Text, Index);
            return AfterOrBefore(false, false, match.Success ? match.Index : -1, match.Value.Length);
        }

        /// <summary>
        /// 获取正则 exp 匹配之前的文本
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Substr Before(Regex exp)
        {
            if (exp == null || Length == 0) return this;
            var match = exp.Match(Text, Index);
            return AfterOrBefore(true, false, match.Success ? match.Index : -1, match.Value.Length);
        }

        /// <summary>
        /// 获取正则 exp1 与 exp2 之间内容
        /// </summary>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public Substr Between(Regex exp1, Regex exp2)
        {
            After(exp1);
            Before(exp2);
            return this;
        }

        /// <summary>
        /// 获取 value 之后的文本，包含匹配内容
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public Substr AfterWith(string value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (Length == 0 || string.IsNullOrEmpty(value)) return this;
            var index = Text.IndexOf(value, Index, comparisonType);
            return AfterOrBefore(false, true, index, value.Length);
        }

        /// <summary>
        /// 获取 value 之前的文本，包含匹配内容
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public Substr BeforeWith(string value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (Length == 0 || string.IsNullOrEmpty(value)) return this;
            var index = Text.IndexOf(value, Index, comparisonType);
            return AfterOrBefore(true, true, index, value.Length);
        }

        /// <summary>
        /// 获取 a 与 b 之间的文本，包含匹配内容
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public Substr BetweenWith(string a, string b, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            AfterWith(a, comparisonType);
            BeforeWith(b, comparisonType);
            return this;
        }

        /// <summary>
        /// 获取 values 之后的文本，包含匹配内容
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Substr AfterWith(params string[] values)
        {
            if (values == null || values.Length == 0) return this;
            foreach (var value in values)
            {
                AfterWith(value);
            }

            return this;
        }

        /// <summary>
        /// 获取 values 之前的文本，包含匹配内容
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public Substr BeforeWith(params string[] values)
        {
            if (values == null || values.Length == 0) return this;
            foreach (var value in values)
            {
                BeforeWith(value);
            }

            return this;
        }

        /// <summary>
        /// 获取正则 exp 匹配之后的文本，包含匹配内容
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Substr AfterWith(Regex exp)
        {
            if (exp == null || Length == 0) return this;
            var match = exp.Match(Text, Index);
            return AfterOrBefore(false, true, match.Success ? match.Index : -1, match.Value.Length);
        }

        /// <summary>
        /// 获取正则 exp 匹配之前的文本，包含匹配内容
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Substr BeforeWith(Regex exp)
        {
            if (exp == null || Length == 0) return this;
            var match = exp.Match(Text, Index);
            return AfterOrBefore(true, true, match.Success ? match.Index : -1, match.Value.Length);
        }

        /// <summary>
        /// 获取正则 exp1 与 exp2 之间内容，包含匹配内容
        /// </summary>
        /// <param name="exp1"></param>
        /// <param name="exp2"></param>
        /// <returns></returns>
        public Substr BetweenWith(Regex exp1, Regex exp2)
        {
            AfterWith(exp1);
            BeforeWith(exp2);
            return this;
        }

        /// <summary>
        /// 统一处理起始索引与长度
        /// </summary>
        /// <param name="flag">匹配类型：true 为 Before, false 为 After</param>
        /// <param name="isWith">是否包含匹配的内容，true 包含</param>
        /// <param name="index">匹配到内容的索引</param>
        /// <param name="length">匹配到内容的长度</param>
        /// <returns></returns>
        private Substr AfterOrBefore(bool flag, bool isWith, int index, int length)
        {
            if (index == -1 || index + length > Index + Length)
            {
                Length = 0;
            }
            else
            {
                if (flag)
                {
                    // Before
                    Length = isWith ? index - Index + length : index - Index;
                }
                else
                {
                    // After
                    Length = isWith ? Length - index + Index : Length - index + Index - length;
                    Index = isWith ? index : index + length;
                }
            }

            return this;
        }

        /// <summary>
        /// 获取处理后的文本
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Length == 0 ? string.Empty : Text.Substring(Index, Length);
        }

        public static implicit operator string(Substr substring)
        {
            return substring.ToString();
        }

        public static implicit operator Substr(string text)
        {
            return new Substr(text);
        }
    }
}