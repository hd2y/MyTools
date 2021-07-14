using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MyTools.Daily
{
    public static class ZDicHelper
    {
        private const string WordPath =
            "//li/a[(starts-with(@href, '/hans/') or starts-with(@href, '/hant/')) and @title]";

        private static readonly Lazy<HttpClient> _lazyClient = new Lazy<HttpClient>(() => new HttpClient(), true);
        private static HttpClient CrawlerClient => _lazyClient.Value;

        /// <summary>
        /// 下载并解析汉典常用字表等
        /// </summary>
        /// <param name="savePath">保存路径</param>
        /// <param name="urlList">链接地址</param>
        /// <param name="cancellationToken"></param>
        public static async Task ReadUniversalUsedWordTableAsync(string savePath, string[] urlList,
            CancellationToken cancellationToken = default)
        {
            var words = new List<string>();

            foreach (var url in urlList)
            {
                var html = await CrawlerClient.GetStringAsync(url);
                var document = new HtmlDocument();
                document.LoadHtml(html);
                var nodes = document.DocumentNode.SelectNodes(WordPath);
                words.AddRange(nodes.Select(node => WebUtility.HtmlDecode(node.InnerHtml).Trim()));
                Debug.WriteLine($"[{url}]解析完成，获取{nodes.Count}个汉字");
            }

            // 使用格式化的文本
            File.WriteAllText(savePath, string.Concat(words), Encoding.UTF8);
            Debug.WriteLine($"获取到{words.Count}个汉字，已保存到[{savePath}]");
        }
    }
}