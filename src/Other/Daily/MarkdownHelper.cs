using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using MyTools.Helpers;

namespace MyTools.Daily
{
    public static class MarkdownHelper
    {
        private static readonly Lazy<HttpClient> _lazyClient = new Lazy<HttpClient>(() => new HttpClient(), true);
        private static HttpClient DownloadClient => _lazyClient.Value;

        private static readonly Regex MatchHttpImageExp =
            new Regex(@"!\[(?'alt'.*?)\]\((?'url'https?://.+?)\)", RegexOptions.IgnoreCase);

        private static readonly Regex MatchImageFileExp =
            new Regex(@"!\[(?'alt'.*?)\]\((?'url'.+?)\)", RegexOptions.IgnoreCase);

        private static readonly Regex MatchMdFileExp = new Regex(@"\.(ma?r?k?do?w?n?(te?xt)?|workbook)$",
            RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture);

        /// <summary>
        /// 下载 Markdown 文件中网络图片
        /// </summary>
        /// <param name="postPath">文章所在文件夹</param>
        /// <param name="imagePath">图片文件保存文件夹</param>
        /// <param name="updateUrl">更新文件中文件路径，false 仅下载图片</param>
        /// <param name="imagePrefix">图片路径前缀，用于更新图片路径指定路径前缀</param>
        /// <param name="newPostPath">更新后文章保存目录</param>
        /// <param name="cancellationToken"></param>
        public static async Task DownloadImagesAsync(string postPath, string imagePath, bool updateUrl,
            string imagePrefix, string newPostPath, CancellationToken cancellationToken = default)
        {
            // 检查入参
            if (string.IsNullOrWhiteSpace(postPath))
                throw new ArgumentException("请选择Markdown文件所在目录！", nameof(postPath));
            if (string.IsNullOrWhiteSpace(imagePath)) throw new ArgumentException("请选择存储图片1目录！", nameof(postPath));
            if (!Directory.Exists(postPath)) throw new DirectoryNotFoundException("所选Markdown文件夹不存在！");

            // 创建存储图片文件夹
            if (!Directory.Exists(imagePath)) Directory.CreateDirectory(imagePath);

            // 创建保存文件目录
            if (!Directory.Exists(newPostPath)) Directory.CreateDirectory(newPostPath);

            var mdFiles = Directory.GetFiles(postPath).Where(n => MatchMdFileExp.IsMatch(n)).ToArray();
            for (var i = 0; i < mdFiles.Length; i++)
            {
                var mdFile = mdFiles[i];
                var fileContent = File.ReadAllText(mdFile, Encoding.UTF8);
                var matches = MatchHttpImageExp.Matches(fileContent);
                if (matches.Count == 0) continue;
                var imgUrlList = (from Match match in matches select match.Groups["url"].Value).Distinct().ToList();
                var imgPathDict = new Dictionary<string, string>();
                var postName = Path.GetFileNameWithoutExtension(mdFile);
                for (var j = 0; j < imgUrlList.Count; j++)
                {
                    var imgUrl = imgUrlList[j];
                    using var responseMessage = await DownloadClient.GetAsync(imgUrl, cancellationToken);
                    responseMessage.EnsureSuccessStatusCode();

                    using var memoryStream = new MemoryStream();
                    await responseMessage.Content.CopyToAsync(memoryStream);
                    
                    // 获取图片名
                    var imgName = GetImageName(imgUrl, memoryStream, $"{postName}-{j + 1:00}");

                    // 写入文件
                    var savePath = Path.Combine(imagePath, imgName);
                    if (!File.Exists(savePath))
                    {
                        File.WriteAllBytes(savePath, memoryStream.ToArray());
                    }

                    // 转换后的文件名
                    imgPathDict.Add(imgUrl, Path.Combine(imagePrefix ?? string.Empty, imgName));
                }

                // 替换文本内容
                if (!updateUrl) continue;
                foreach (Match match in matches)
                {
                    var altText = match.Groups["alt"].Value;
                    var urlText = match.Groups["url"].Value;
                    fileContent = fileContent.Replace(match.Value, $"![{altText}]({imgPathDict[urlText]})");
                }

                // 更新文件中图片链接并写入到文件
                File.WriteAllText(Path.Combine(newPostPath, Path.GetFileName(mdFile)), fileContent, Encoding.UTF8);
            }
        }

        /// <summary>
        /// 获取 Markdown 文件中的图片
        /// </summary>
        /// <param name="postPath"></param>
        /// <returns></returns>
        public static Dictionary<string, string[]> CheckImages(string postPath)
        {
            var mdFiles = Directory.GetFiles(postPath).Where(n => MatchMdFileExp.IsMatch(n)).ToArray();
            var dict = new Dictionary<string, string[]>();
            for (var i = 0; i < mdFiles.Length; i++)
            {
                var mdFile = mdFiles[i];
                var fileContent = File.ReadAllText(mdFile, Encoding.UTF8);
                var images =
                    (from Match match in MatchImageFileExp.Matches(fileContent) select match.Groups["url"].Value)
                    .Distinct().ToArray();
                dict.Add(mdFile, images);
            }

            return dict;
        }

        private static string GetImageName(string url, Stream stream, string name)
        {
            var imgUri = new Uri(url);
            string format = null;
            var fileName = imgUri.Segments.Length > 1 && imgUri.Segments[imgUri.Segments.Length - 1] != "/"
                ? imgUri.Segments[imgUri.Segments.Length - 1]
                : string.Empty;
            if (!string.IsNullOrEmpty(fileName) && fileName.Contains("."))
            {
                format = fileName.Substring(fileName.LastIndexOf(".") + 1);
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                var imgFormat = ImageHelper.GetImageFormat(stream);
                if (imgFormat == ImageFormat.Unknown) throw new Exception("当前图片格式未知！");
                format = imgFormat.ToString().ToLower();
            }

            return $"{name}.{format}";
        }
    }
}