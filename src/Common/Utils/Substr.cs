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
        /// 获取 value 之前的文本
        /// </summary>
        /// <param name="value"></param>
        /// <param name="comparisonType"></param>
        /// <returns></returns>
        public Substr Before(string value, StringComparison comparisonType = StringComparison.CurrentCulture)
        {
            if (Length == 0 || string.IsNullOrEmpty(value)) return this;
            var index = Text.IndexOf(value, Index, comparisonType);
            if (index == -1 || index >= Index + Length)
            {
                Length = 0;
            }
            else
            {
                Length = index - Index;
            }

            return this;
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
        /// 获取正则 exp 匹配之前的文本
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Substr Before(Regex exp)
        {
            if (exp == null || Length == 0) return this;
            var match = exp.Match(Text, Index);
            if (!match.Success || match.Index >= Index + Length)
            {
                Length = 0;
            }
            else
            {
                Length = match.Index - Index;
            }

            return this;
        }

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
            if (index == -1 || index + value.Length >= Index + Length)
            {
                Length = 0;
            }
            else
            {
                Length -= index + value.Length - Index;
                Index = index + value.Length;
            }

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
        /// 获取正则 exp 匹配之后的文本
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        public Substr After(Regex exp)
        {
            if (exp == null || Length == 0) return this;
            var match = exp.Match(Text, Index);
            if (!match.Success || match.Index + match.Value.Length >= Index + Length)
            {
                Length = 0;
            }
            else
            {
                Length -= match.Index + match.Value.Length - Index;
                Index = match.Index + match.Value.Length;
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